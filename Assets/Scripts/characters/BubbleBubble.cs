using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float dashSpeed = 3f;
    public float range = 20f;
    public float dashCooldown = 4f;
    
    // private Vector3 initialPosition;
    private bool isDashing = false;
    private float dashCooldownTimer = 0f;
    // private bool isFainted = false;
    private Rigidbody2D rb;

    public GameObject enemy;
    public Vector3 position;

    void Start()
    {
        // initialPosition = transform.position;
    }

    void Update()
    {
        if (enemy != null && !isDashing)
        {
            StartCoroutine(DashTowardsEnemy(enemy));
        } else {
            transform.Translate(position * Time.deltaTime * moveSpeed);
        }

        // Handle cooldown
        if (isDashing && dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    System.Collections.IEnumerator DashTowardsEnemy(GameObject enemy)
    {
        isDashing = true;
        Vector3 targetPosition = enemy.transform.position;

        float dashTime = 0.5f;
        float elapsed = 0f;

        while (elapsed < dashTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, dashSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Check for collision with solid grid
        if (Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("SolidGrid")) != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Input.mousePosition, dashSpeed * Time.deltaTime);
        }
        else
        {
            // Start cooldown
            dashCooldownTimer = dashCooldown;
        }
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy")) {
            Destroy(gameObject,0);
        }
    }
}