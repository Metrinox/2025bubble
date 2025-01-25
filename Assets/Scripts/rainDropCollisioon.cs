using UnityEngine;

public class rainDropCollisioon : MonoBehaviour
{
    
    [Header("Raindrop Settings")]
    public float speed = 2f;            
    public float offScreenY = -10f;

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < offScreenY)
        {
            Destroy(gameObject); 
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
    
            Destroy(gameObject);
        }
    }
    
}
