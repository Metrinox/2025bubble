using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float dashSpeed = 3f;
    public float range = 20f;
    public float dashCooldown = 4f;
    public float faintDuration = 8f;
    
    public Vector3 initialPosition;
    private bool isDashing = false;
    private float dashCooldownTimer = 0f;
    private bool isFainted = false;
    public Rigidbody2D rb;
    public float rotationSpeed = 2f;
    public LevelManager manager;

    public int hp;

    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(manager);
        if (other.collider.CompareTag("BubbleBubble")) {
        hp -= 1;
        if (hp <= 0) {
            manager.disabled.Add(gameObject);
            gameObject.SetActive(false);
        }
        }
    }


    void Start()
    {
        initialPosition = transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isFainted)
        {
            return;
        }

        // Check for dash condition

        // Handle movement
        if (!isDashing)
        {
            MoveIdle();
        }

        GameObject bubble = DetectBubble();
        if (bubble != null && !isDashing)
        {
            StartCoroutine(DashTowardsBubble(bubble));
        }

        // Handle cooldown
        if (isDashing && dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    void MoveIdle()
    {
        // float newX = Mathf.PingPong(range * moveSpeed, 0.5f) - 1;
        // transform.position = new Vector3(initialPosition.x + newX, transform.position.y, transform.position.z);
    }

    GameObject DetectBubble()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Bubble"))
            {
                return collider.gameObject;
            }
        }
        return null;
    }

    System.Collections.IEnumerator DashTowardsBubble(GameObject bubble)
    {
        isDashing = true;
        Vector3 targetPosition = bubble.transform.position;

        float dashTime = 2f;
        float elapsed = 0f;

        while (elapsed < dashTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, dashSpeed * Time.deltaTime);
            // Calculate the direction to the target
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Update the rotation to face the target direction
            if (direction.x != 0 && direction.y != 0) // Check to avoid setting rotation to zero
            {
                // Calculate the angle in degrees based on the direction
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    
                // Create a new rotation with the calculated angle on the Z-axis
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
                
                // Smoothly rotate towards the target rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Check for collision with solid grid
        if (Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("SolidGrid")) != null)
        {
            Faint();
        }
        else
        {
            // Start cooldown
            dashCooldownTimer = dashCooldown;
        }
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
    }

    void Faint()
    {
        isFainted = true;
        // Optionally, you can add a faint animation here
        StartCoroutine(RecoverFromFaint());
    }

    System.Collections.IEnumerator RecoverFromFaint()
    {
        yield return new WaitForSeconds(faintDuration);
        isFainted = false;
    }

    public void Reset() {
        isDashing = false;
        StopAllCoroutines();
        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;
        rb.linearVelocity = Vector3.zero;
    }
}