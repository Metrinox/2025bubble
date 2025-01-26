using UnityEngine;
using System.Collections;

public class SulphurBubble : MonoBehaviour
{
    [Header("Movement")]
    public float riseSpeed = 1f;
    
    [Header("Popping Logic")]
    public float minPopTime = 2f;
    public float maxPopTime = 5f;
    
    [Header("Explosion")]
    public GameObject explosionPrefab;
    public float explosionRadius = 2f;
    public float explosionDuration = 0.5f;

    [Header("Poison Cloud")]
    public GameObject poisonCloudPrefab;
    public float cloudSpawnOffsetY = 0.2f;
    public float destroyDelayAfterPop = 0.1f;

    private float popTimer;

    void Start()
    {
        popTimer = Random.Range(minPopTime, maxPopTime);
        
        StartCoroutine(PopAfterDelay(popTimer));
    }

    void Update()
    {
        transform.Translate(Vector2.up * riseSpeed * Time.deltaTime);
    }

    private IEnumerator PopAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PopBubble();
    }

    // i gave p and used chatgpt for the following code. thanks chatgpt

    private void PopBubble()
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(
                explosionPrefab, 
                transform.position, 
                Quaternion.identity
            );

 
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    Destroy(hit.gameObject);
                }
            }

            Destroy(explosion, explosionDuration);
        }

        if (poisonCloudPrefab != null)
        {
            Vector3 cloudPos = transform.position + new Vector3(0f, cloudSpawnOffsetY, 0f);
            Instantiate(poisonCloudPrefab, cloudPos, Quaternion.identity);
        }

        Destroy(gameObject, destroyDelayAfterPop);
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //       
    //         Destroy(other.gameObject);
    //     }
    // }
}
