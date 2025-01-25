using UnityEngine;

public class Fish : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float dashSpeed = 3f;
    public float range = 20f;
    public float dashCooldown = 4f;
    public float faintDuration = 8f;
    
    private Vector3 initialPosition;
    private bool isDashing = false;
    private float dashCooldownTimer = 0f;
    private bool isFainted = false;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isFainted)
        {
            return;
        }

        // Handle movement
        if (!isDashing)
        {
            MoveIdle();
        }

        // Check for dash condition
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
        float newX = Mathf.PingPong(Time.time * moveSpeed, 2) - 1;
        transform.position = new Vector3(initialPosition.x + newX, transform.position.y, transform.position.z);
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
            Faint();
        }
        else
        {
            // Start cooldown
            dashCooldownTimer = dashCooldown;
        }

        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
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
}