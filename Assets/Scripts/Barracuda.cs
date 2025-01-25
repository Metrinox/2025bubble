using UnityEngine;
using System.Collections;
// WTF c# ERRORS???!!?!!?
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

    private Transform player;

    private bool isStopped = false;   
    private bool isChargingUp = false; 
    private bool isDashing = false;   
    void Start()
    {

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }


        StartCoroutine(AttackRoutine());
    }

    void Update()
    {

        if (!isStopped && !isChargingUp && !isDashing && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * approachSpeed * Time.deltaTime);
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
                Vector2 dashTargetPos = player.position;   // Lock onto player's position now
                Vector2 direction = (dashTargetPos - dashStartPos).normalized;

                float elapsed = 0f;
                while (elapsed < dashTime)
                {
                    elapsed += Time.deltaTime;
                    transform.Translate(direction * dashSpeed * Time.deltaTime);
                    yield return null;
                }
                isDashing = false;
            }

            // COOLDOWN
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
