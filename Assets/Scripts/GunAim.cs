using UnityEngine;

public class GunAim : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool canShoot = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseRelativeToPlayer = new Vector2(0, 0);

        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the Z position is 0 for 2D

        if (transform.parent != null)
        {
            Transform transformParent = transform.parent;
            mouseRelativeToPlayer = mousePosition - transformParent.position;
        } else
        {
            mouseRelativeToPlayer = mousePosition - transform.position;
        }

        // Calculate the direction from the fire point to the mouse position
        Vector2 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        canShoot = (Mathf.Abs(mouseRelativeToPlayer.y) >= 1.2f || Mathf.Abs(mouseRelativeToPlayer.x) >= 0.4f);

        if (!canShoot) return;

        if (direction.x < 0)
        {
            transform.localScale = new Vector3(1, -1, 1);
            if (direction.x < -0.5f) transform.localPosition = new Vector3(-0.5f, -0.2f, 0);
        } else 
        {
            transform.localScale = Vector3.one;
            if (direction.x > 0.5f) transform.localPosition = new Vector3(0.5f, -0.2f, 0);
        }


    }
}
