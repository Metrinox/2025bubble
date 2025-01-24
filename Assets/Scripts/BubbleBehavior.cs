using UnityEngine;

public class BubbleBehavior : MonoBehaviour
{

    private float age = 0.0f;
    public float maximumAge = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        age += Time.deltaTime;

        if (age > maximumAge) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Friendly") Destroy(gameObject);
    }
}
