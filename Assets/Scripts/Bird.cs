using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("Bird Settings")]
    public float speed = 5f;         // horizontal speed
    public float leftBound = -10f;   
    public float rightBound = 10f;   
    public bool movingRight = true; 
    private float direction; 

    void Update()
    {
        float direction = movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (transform.position.x < leftBound || transform.position.x > rightBound)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);    
        }
    }
}

