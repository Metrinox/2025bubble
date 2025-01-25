using Unity.VisualScripting;
using UnityEngine;

public class LeafBehavior : MonoBehaviour
{
    // Maximum Y-coordinate where leaf self-destructs 
    public float maxY = 15f; 

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 15) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject); 
            Destroy(gameObject);
        }
    }
}
