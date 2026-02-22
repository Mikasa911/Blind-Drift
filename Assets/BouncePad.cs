using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float baseBounceForce = 10f;       // Minimum bounce
    public float bounceMultiplier = 1.2f;     // How much fall speed affects bounce
    public float maxBounceForce = 25f;        // Maximum allowed bounce

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
        PlayerMove playermove=FindAnyObjectByType<PlayerMove>();
    FindAnyObjectByType<CameraAudioClipPlayer>().PlayClip(playermove.bounceclip);
        // Only bounce if player is falling
        if (playerRb.velocity.y <= 0f)
        {
            float fallSpeed = Mathf.Abs(playerRb.velocity.y);
            fallSpeed = 1;
            // Bounce based on fall speed
            float bounceForce = baseBounceForce + (fallSpeed * bounceMultiplier);

            // Clamp to max limit
            bounceForce = Mathf.Clamp(bounceForce, baseBounceForce, maxBounceForce);

            // Preserve horizontal velocity
            playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
        }
    }
}