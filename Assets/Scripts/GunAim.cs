using UnityEngine;

public class GunAim : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the Z position is 0 for 2D

        // Calculate the direction from the fire point to the mouse position
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (direction.x < 0)
        {
            transform.localScale = new Vector3(1, -1, 1);
        } else
        {
            transform.localScale = Vector3.one;
        }
    }
}
