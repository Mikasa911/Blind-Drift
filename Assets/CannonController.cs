using UnityEngine;

public class CannonController : MonoBehaviour
{
    [Header("References")]
    public Transform barrel;         // Rotating part
    public Transform firePoint;      // End of barrel

    [Header("Settings")]
    public float minAngle = 10f;
    public float maxAngle = 80f;
    public float angleSpeed = 60f;

    public float minPower = 5f;
    public float maxPower = 25f;
    public float powerIncreaseSpeed = 20f;

    private float lastPrintedPower;
    private float currentAngle = 45f;
    private float currentPower = 10f;

    private GameObject currentPlayer;
    private Rigidbody2D playerRb;

    private bool playerLoaded = false;


    void Update()
    {
        if (!playerLoaded) return;

        HandleAngle();
        HandlePower();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            LaunchPlayer();
        }
    }

    void HandleAngle()
    {
        float input = Input.GetAxis("Horizontal");

        // Reverse input when flipped
        float directionMultiplier = Mathf.Sign(transform.localScale.x);
        input *= directionMultiplier;

        currentAngle += input * angleSpeed * Time.deltaTime;
        currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);

        // Always rotate same way
        barrel.localRotation = Quaternion.Euler(0f, 0f, -currentAngle);
    }


    void HandlePower()
    {
        float previousPower = currentPower;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentPower += powerIncreaseSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            currentPower -= powerIncreaseSpeed * Time.deltaTime;
        }

        currentPower = Mathf.Clamp(currentPower, minPower, maxPower);

        // Only print if power actually changed
        if (Mathf.Abs(currentPower - previousPower) > 0.01f)
        {
            Debug.Log("Current Power: " + currentPower.ToString("F1"));
        }
    }
    public void LoadPlayer(GameObject player)
    {

        currentPlayer = player;
        currentPlayer.GetComponent<PlayerMove>().LockMovement();
        playerRb = player.GetComponent<Rigidbody2D>();
        currentPlayer.GetComponent<PlayerMove>().tipsText.text = "A,D for angle, Up,Down for Power";
        playerLoaded = true;

        // Move player to cannon tip
        player.transform.position = firePoint.position;

        // Hide player sprite
        player.GetComponent<SpriteRenderer>().enabled = false;

        // Disable player movement script
        // PlayerMove pm = player.GetComponent<PlayerMove>();
        //pm.enabled = false;

        // Stop physics movement
        playerRb.velocity = Vector2.zero;
        playerRb.angularVelocity = 0f;

        // Make player kinematic while inside cannon
        playerRb.isKinematic = true;
    }

    void LaunchPlayer()
    {
        if (currentPlayer == null) return;
               PlayerMove playermove=FindAnyObjectByType<PlayerMove>();
    FindAnyObjectByType<CameraAudioClipPlayer>().PlayClip(playermove.cannonclip);
        currentPlayer.GetComponent<PlayerMove>().tipsText.text = "";
        playerLoaded = false;

        currentPlayer.transform.position = firePoint.position;
        currentPlayer.GetComponent<SpriteRenderer>().enabled = true;

        playerRb.isKinematic = false;

        float rad = currentAngle * Mathf.Deg2Rad;
        //Vector2 direction = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad)).normalized;
        Vector2 direction = firePoint.up;

        playerRb.velocity = direction * currentPower;

        // Lock movement until player lands
        //currentPlayer.GetComponent<PlayerMove>().LockMovement();

        currentPlayer = null;
    }
}