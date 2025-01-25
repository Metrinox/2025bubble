using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class BubbleWandShoot : MonoBehaviour
{

    public GameObject bubblePrefab;
    public float startXOffset = 0.0f;
    public float startYOffset = 0.0f;

    public float thrust = 100f;
    public Vector3 gunDirection = new (1, 0, 0);
    public float shootDelay = 1.0f;
    public float countDown = 0.0f;

    bool firing = false;


    void Update()
    {

        if (Input.GetMouseButtonDown(0) && countDown <= 0.0f)
        {
            countDown = shootDelay;
            firing = true;
        }
        
        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0.0f, 1.0f);

        if (firing && countDown <= 0.0f)
        {
            Shoot();
            firing = false;
        }
    }


    void Shoot()
    {
        gunDirection = transform.parent.rotation * Vector3.right;
        Vector3 startOffset = new(startXOffset, startYOffset, 0);
        Vector3 direction = gunDirection.normalized;

        GameObject bubble = (GameObject)Instantiate(bubblePrefab, transform.position + startOffset, transform.rotation);
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();

        rb.AddForce(direction * thrust);

    }

}