using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float moveSpeed = 8f;

    [Header("Jump")]
    [SerializeField] public float jumpForce = 16f;
    [SerializeField] public float fallMultiplier = 2.5f;
    [SerializeField] public float lowJumpMultiplier = 2f;

    [Header("Ground Check")]
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundCheckRadius = 0.2f;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] float scale;
    [SerializeField] public TextMeshProUGUI tipsText;

    private CannonController nearbyCannon;
    private Rigidbody2D rb;
    private float moveInput;
    public bool isGrounded;
    public bool isMoving;

[Header("MUsics")]
[SerializeField]public  AudioClip jumpclip;
[SerializeField] public AudioClip bounceclip;
[SerializeField] public AudioClip cannonclip;
[SerializeField]public  AudioClip dieclip;
[SerializeField] public AudioClip winclip;
[SerializeField] public AudioClip switchclip;
    private float launchLockTimer = 0f;
    private bool movementLocked = false;

    Animator anim;

    // ✅ Platform delta movement variables
    private Transform platform;
    private Vector3 lastPlatformPos;

    public void LockMovement()
    {
        movementLocked = true;
    }

    public void UnlockMovement()
    {
        movementLocked = false;
    }

    public void LockMovement(float duration)
    {
        launchLockTimer = duration;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MovingPlatform"))
        {
            UnlockMovement();
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            platform = collision.transform;
            lastPlatformPos = platform.position;
        }
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.E) && nearbyCannon != null)
        {
            nearbyCannon.LoadPlayer(gameObject);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            FindAnyObjectByType<CameraAudioClipPlayer>().PlayClip(jumpclip);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Better Jump Physics
        if (rb.velocity.y < 0)
        {
            rb.velocity += (fallMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += (lowJumpMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }

        anim.SetBool("isJumping", !isGrounded);
        anim.SetBool("isRunning", Mathf.Abs(moveInput) > 0.1f);

        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(scale), Mathf.Abs(scale), Mathf.Abs(scale));
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(scale), Mathf.Abs(scale), Mathf.Abs(scale));
        }
    }

    void FixedUpdate()
    {
        if (movementLocked) return;

        // Normal movement
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // ✅ Move with platform smoothly (delta movement)
        if (platform != null)
        {
            Vector3 delta = platform.position - lastPlatformPos;
            rb.position += new Vector2(delta.x, delta.y);
            lastPlatformPos = platform.position;
        }
    }

    public void UnlockMovementDelayed()
    {
        Invoke("UnlockMovement", 3f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Finish"))
        {
            FindAnyObjectByType<UIManager>().ActivateWinAndMoveToNext();
            LockMovement();
        }

        if (collision.CompareTag("Cannon"))
        {
            tipsText.text = "Press E to Interact";
            nearbyCannon = collision.GetComponent<CannonController>();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cannon"))
        {
            tipsText.text = "";
            nearbyCannon = null;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MovingPlatform"))
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            platform = null;
        }
    }
}