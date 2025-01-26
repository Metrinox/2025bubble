using UnityEngine;
using System.Collections;

public class Barracuda : MonoBehaviour
{
    [Header("Movement Speeds")]
    public float approachSpeed = 2f;
    public float dashSpeed = 10f;

    [Header("Timing")]
    public float stopBeforeChargeTime = 1f;
    public float chargeUpTime = 1.5f;
    public float dashTime = 1f;
    public float cooldownTime = 2f;

    [Header("Scale Settings")]
    public float scale = 0.82f;   

    private Transform player;

    private bool isStopped = false;   
    private bool isChargingUp = false; 
    private bool isDashing = false;

    private Vector3 baseScale;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }

        baseScale = transform.localScale;

        StartCoroutine(AttackRoutine());
    }

    void Update()
    {
        if (!isStopped && !isChargingUp && !isDashing && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * approachSpeed * Time.deltaTime);

            if (direction.x < 0)
            {
                transform.localScale = new Vector3(
                    Mathf.Abs(baseScale.x) * scale,  
                    Mathf.Abs(baseScale.y) * scale, 
                    Mathf.Abs(baseScale.z)
                );
            }
            else
            {
                transform.localScale = new Vector3(
                    -Mathf.Abs(baseScale.x) * scale, 
                    Mathf.Abs(baseScale.y) * scale, 
                    Mathf.Abs(baseScale.z)
                );
            }
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            isStopped = true;
            yield return new WaitForSeconds(stopBeforeChargeTime);
            isStopped = false;

            isChargingUp = true;
            yield return new WaitForSeconds(chargeUpTime);
            isChargingUp = false;

            if (player != null)
            {
                isDashing = true;

                Vector2 dashStartPos = transform.position;
                Vector2 dashTargetPos = player.position;   
                Vector2 direction = (dashTargetPos - dashStartPos).normalized;

                float elapsed = 0f;
                while (elapsed < dashTime)
                {
                    elapsed += Time.deltaTime;
                    transform.Translate(direction * dashSpeed * Time.deltaTime);

                    if (direction.x < 0)
                    {
                        transform.localScale = new Vector3(
                            Mathf.Abs(baseScale.x) * scale,
                            Mathf.Abs(baseScale.y) * scale,
                            Mathf.Abs(baseScale.z)
                        );
                    }
                    else
                    {
                        transform.localScale = new Vector3(
                            -Mathf.Abs(baseScale.x) * scale,
                            Mathf.Abs(baseScale.y) * scale,
                            Mathf.Abs(baseScale.z)
                        );
                    }

                    yield return null;
                }
                isDashing = false;
            }

            // 4) Cooldown
            yield return new WaitForSeconds(cooldownTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}

