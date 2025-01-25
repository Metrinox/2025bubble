using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public float movementSpeed = 5f; // Speed of the bubble
    private float lifetime = 5f; // Time before the bubble is destroyed

    void Start()
    {
        // Destroy the bubble after a set time
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the bubble to the right
        transform.Translate(movementSpeed * Time.deltaTime * Vector2.right);
    }
}