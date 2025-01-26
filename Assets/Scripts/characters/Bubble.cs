using System.Collections;
using System.Collections.Generic;
//using LDtkUnity;
using UnityEngine;

// The bubble requires a rb to proceed
[RequireComponent(typeof(Rigidbody2D))]
public class Bubble : MonoBehaviour
{
    public float speed; // Speed of movement
    public float jumpForce; // Force applied when jumping
    public float maxSize; // Max allowed size
    public float minSize; // Min allowed size, dont set too small or unity collision can be mysterious
    public float size; // Current size of the bubble
    public float cost; // Size cost for shooting a bubble
    public GameObject bubblePrefab; // Prefab to shoot

    private Rigidbody2D rb; // Rigidbody component
    private bool isGrounded; // Check if the bubble is on the ground
    private bool onLadder; // check if bubble is on the ladder
    public float climbSpeed;

    public float dashCooldown; // Cooldown duration for dashing
    public float shootCooldown; // Cooldown duration for shooting
    private float lastShootTime = 0f;
    private float lastDashTime = 0f;
    private bool isAlive = true;
    public LevelManager manager;

    public float range;

    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // not necessary, but just in case we messed up with the params
        // transform.localScale = new Vector3(size/maxSize, size/maxSize, transform.localScale.z);
    }

    void Update()
    {
        HandleShooting();
        // transform.localScale = new Vector3(size/maxSize, size/maxSize, transform.localScale.z);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector3(horizontal * speed, vertical * speed, 0);
        // HandleClimbing();
    }

    // void FixedUpdate()
    // {
    //     float horizontal = Input.GetAxis("Horizontal");
    //     float vertical = Input.GetAxis("Vertical");
    //     Debug.Log(Time.time);
    //     Debug.Log(lastDashTime + dashCooldown);
    //     if (Time.time > lastDashTime + dashCooldown && horizontal != 0 && vertical != 0) {
    //         rb.linearVelocity = new Vector3(horizontal * speed, vertical * speed, 0);
    //         lastDashTime = Time.time;
    //     }
    // }

    // private IEnumerator HandleMove() {
    //     while (true) {
            
    //     }
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
            transform.localScale = new Vector3(size/maxSize, size/maxSize, transform.localScale.z);
            manager.disabled.Add(collision.gameObject);
            collision.gameObject.SetActive(false);
        } else if (collision.transform.CompareTag("BubbleBubble")){
            
        } else if (collision.transform.CompareTag("Enemy")){
            StartCoroutine(Die());
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
        GameObject enemy = DetectEnemy();
        if (size > cost + minSize) {
            // Create a new bubble instance
            GameObject newBubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);

            // Set the bubble's movement script and speed
            BubbleMovement bubbleMovement = newBubble.GetComponent<BubbleMovement>();
            if (enemy != null) {
                bubbleMovement.dashSpeed = 10f; // Set desired speed
                bubbleMovement.enemy = enemy;
            } else {
                bubbleMovement.moveSpeed = 10f;
                bubbleMovement.position = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, 0);
            }
            // Reduce the size of the current bubble
            size -= cost;
            transform.localScale = new Vector3(size/maxSize, size/maxSize, transform.position.z);
        }
    }

    public IEnumerator Die() {
        animator.Play("die");
        yield return new WaitForSeconds(1);
        Destroy(GameObject.Find("FollowPlayer").transform.GetChild(0));
        Destroy(gameObject, 0);
        manager.Die();
    }

    public void Destroy() {
        Destroy(gameObject, 0);
    }

    GameObject DetectEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                return collider.gameObject;
            }
        }
        return null;
    }
}