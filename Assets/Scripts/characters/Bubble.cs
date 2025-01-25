using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The bubble requires a rb to proceed
[RequireComponent(typeof(Rigidbody2D))]
public class Bubble : MonoBehaviour
{
    public float speed = 3f; // Speed of movement
    public float jumpForce = 3f; // Force applied when jumping
    public float maxSize = 10f; // Max allowed size
    public float minSize = 1f; // Min allowed size, dont set too small or unity collision can be mysterious
    public float size = 10f; // Current size of the bubble
    public float cost = 0.5f; // Size cost for shooting a bubble
    public GameObject bubblePrefab; // Prefab to shoot

    private Rigidbody2D rb; // Rigidbody component
    private bool isGrounded; // Check if the bubble is on the ground
    private bool onLadder; // check if bubble is on the ladder
    public float climbSpeed = 3f;

    public float dashCooldown = 2f; // Cooldown duration for dashing
    public float shootCooldown = 1f; // Cooldown duration for shooting
    private float lastShootTime = 0f;
    private float lastDashTime = 0f;
    public LevelManager manager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // not necessary, but just in case we messed up with the params
        // transform.localScale = new Vector3(size/maxSize, size/maxSize, transform.localScale.z);
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
        transform.localScale = new Vector3(size/maxSize, size/maxSize, transform.localScale.z);

        // HandleClimbing();
    }

    // void FixedUpdate()
    // {
    //     Dash();
    // }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime));
    }

    // private void Dash()
    // {
    //     if (Input.GetKey(KeyCode.F) && Time.time >= lastDashTime + dashCooldown)
    //     {
    //         rb.AddForce(new Vector2(rb.linearVelocity.x * 2, 0), ForceMode2D.Impulse); // Dash force
    //         lastDashTime = Time.time; // Reset the cooldown timer
    //     }
    // }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastShootTime + shootCooldown && size > cost)
        {
            ShootBubble();
            lastShootTime = Time.time;
        }
    }

    // private void HandleClimbing()
    // {
    //     float vertical = Input.GetAxis("Vertical");
    //     Debug.Log(onLadder);

    //     if (onLadder)
    //     {
    //         // Set vertical velocity for climbing
    //         rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * climbSpeed);
    //         isGrounded = false;
    //     } else if (vertical > 0 && isGrounded)
    //     {
    //         rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
    //         rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    //     }

    // }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.CompareTag("BounsBubble")) {
            size += collision.gameObject.GetComponent<BounsBubble>().size;
            Debug.Log(size);
            Destroy(collision.gameObject);
        } else {
            Die();
        }

        // // // Check if the bubble is grounded
        
        // if (collision.transform.CompareTag("Death")) {
        //     Die();
        // }
        // // Check if the colliding object is beneath the current object
        // if (collision.contacts.Length > 0)
        // {
        //     foreach (ContactPoint2D contact in collision.contacts)
        //     {
        //         // If the contact point's normal is pointing upwards, the collision is below
        //         if (contact.normal.y > 0 && contact.collider.CompareTag("solid"))
        //         {
        //             isGrounded = true;
        //             break; // Exit the loop early since we found a valid contact
        //         }
        //     }
        // }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Ladder"))
    //     {
    //         // Set onLadder to true when entering the ladder
    //         onLadder = true;
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("Ladder"))
    //     {
    //         // Check if still overlapping with any ladder colliders
    //         Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);

    //         foreach (var collider in colliders)
    //         {
    //             if (collider.CompareTag("Ladder"))
    //             {
    //                 // If still touching a ladder, return and do not set onLadder to false
    //                 return;
    //             }
    //         }

    //         // If no ladder colliders are detected, set onLadder to false
    //         onLadder = false; 
    //     }
    // }


    // void OnCollisionExit2D(Collision2D collision)
    // {
    //     // Set isGrounded to false when leaving the ground
    //     onLadder = false;
    //     isGrounded = false;
    // }

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
        manager.Die();
        Destroy(gameObject, 0);
    }
}