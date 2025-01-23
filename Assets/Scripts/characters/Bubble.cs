using UnityEngine;

// The bubble requires a rb to proceed
[RequireComponent(typeof(Rigidbody2D))]
public class Bubble : MonoBehaviour
{
    public float speed = 5f; // Speed of movement
    public float jumpForce = 5f; // Force applied when jumping
    public float maxSize = 10f; // Max allowed size
    public float size = 10f; // Current size of the bubble
    public float cost = 0.5f; // Size cost for shooting a bubble
    public GameObject bubblePrefab; // Prefab to shoot

    private Rigidbody2D rb; // Rigidbody component
    private bool isGrounded; // Check if the bubble is on the ground

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // not necessary, but just in case we messed up with the params
        transform.localScale = new Vector3(1.0f, 1.0f, transform.position.z);
    }

    void Update()
    {
        // Horizontal movement
        float horizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        // Shooting bubbles
        if (Input.GetKeyDown(KeyCode.Space) && size > cost)
        {
            ShootBubble();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bubble is grounded
        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Set isGrounded to false when leaving the ground
        isGrounded = false;
    }

    void ShootBubble()
    {
        if (size > cost) {
            // Create a new bubble instance
            GameObject newBubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);

            // Set the bubble's movement script and speed
            BubbleMovement bubbleMovement = newBubble.GetComponent<BubbleMovement>();
            bubbleMovement.movementSpeed = 5f; // Set desired speed

            // Reduce the size of the current bubble
            size -= cost;
            transform.localScale = new Vector3(size/maxSize, size/maxSize, transform.position.z);
        }
    }
}