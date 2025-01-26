using UnityEngine;

public class PoisonCloud : MonoBehaviour
{
    public float cloudLifetime = 3f;  // how long the cloud lingers

    void Start()
    {
        // Destroy the poison cloud after cloudLifetime seconds
        Destroy(gameObject, cloudLifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the player touches the cloud, destroy the player
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
