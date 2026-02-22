using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public float distance = 5f;
    public float speed = 2f;
    [SerializeField] bool hasTraps = false;

    private Vector2 startPos;
    private Rigidbody2D rb;
    private Vector2 lastPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = rb.position;
        lastPos = rb.position;
    }

    private float moveTimer = 0f;
    private bool movingForward = true;

    void FixedUpdate()
    {
        float moveAmount = speed * Time.fixedDeltaTime;

        if (movingForward)
        {
            moveTimer += moveAmount;
            if (moveTimer >= distance)
            {
                moveTimer = distance;
                movingForward = false;
            }
        }
        else
        {
            moveTimer -= moveAmount;
            if (moveTimer <= 0f)
            {
                moveTimer = 0f;
                movingForward = true;
            }
        }

        Vector2 newPos = startPos + new Vector2(moveTimer, 0f);
        rb.MovePosition(newPos);
        Vector2 delta = rb.position - lastPos;
        lastPos = rb.position;

        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Player"))
                {
                    child.position += (Vector3)delta;
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        // Player standing on top of platform       
        collision.transform.SetParent(transform);
        Debug.Log("Player Parent Set");

    }

    void OnCollisionExit2D(Collision2D collision)
    {

        if (!collision.gameObject.CompareTag("Player")) return;
        collision.transform.SetParent(null);
        Debug.Log("Player Parent Removed");
    }

}