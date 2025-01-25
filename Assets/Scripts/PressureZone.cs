using UnityEngine;

public class PressureZone : MonoBehaviour
{
    [Header("Zone Timings")]
    [Tooltip("How long before this zone disappears on its own.")]
    public float zoneLifetime = 5f;

    [Tooltip("How long the player can remain inside before being destroyed.")]
    public float timeToDestroyPlayer = 3f;

    private bool playerIsInside = false;
    private float insideTimer = 0f;

    void Start()
    {
        Destroy(gameObject, zoneLifetime);
    }

    void Update()
    {
        if (playerIsInside)
        {
            insideTimer += Time.deltaTime;
            if (insideTimer >= timeToDestroyPlayer)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null) 
                {
                    Destroy(player);
                }
                
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInside = false;
            insideTimer = 0f;
        }
    }
}
