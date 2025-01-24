using UnityEngine;

// The bubble requires a rb to proceed
[RequireComponent(typeof(Rigidbody2D))]
public class Bubble : MonoBehaviour
{
    public float speed = 3f; // Speed of movement
    public float jumpForce = 3f; // Force applied when jumping
    public float maxSize = 10f; // Max allowed size
    public float minSize = 1f; // Min allowed size, dont set too small or unity collision can be mysterious
    public float size; // Current size of the bubble
    public float cost = 0.5f; // Size cost for shooting a bubble
    public GameObject bubblePrefab; // Prefab to shoot

    private Rigidbody2D rb; // Rigidbody component
    private bool isGrounded; // Check if the bubble is on the ground
    private bool onLadder; // check if bubble is on the ladder
    public float climbSpeed = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // not necessary, but just in case we messed up with the params
        transform.localScale = new Vector3(1.0f, 1.0f, transform.position.z);
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
        HandleClimbing();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 velocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        rb.linearVelocity = velocity;
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && size > cost)
        {
            ShootBubble();
        }
    }

    private void HandleClimbing()
    {
        float vertical = Input.GetAxis("Vertical");

        // if (onLadder)
        // {
            // rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * climbSpeed);
            // Debug.Log(rb.linearVelocity);
        // }
        // else
        // {
        if (vertical > 0 && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        // }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // // Check if the bubble is grounded
        // if (collision.otherCollider.CompareTag("Ladder")) {
        //     onLadder = true;
        // } else {
        //     onLadder = false;
        // }
        if (collision.otherCollider.CompareTag("Death")) {
            Die();
        }
        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Set isGrounded to false when leaving the ground
        // onLadder = false;
        isGrounded = false;
    }

    void ShootBubble()
    {
        if (size > cost + minSize) {
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

    void Die() {
        Destroy(this, 0);
    }
}