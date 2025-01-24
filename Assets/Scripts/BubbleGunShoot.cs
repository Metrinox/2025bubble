using UnityEngine;
public class BubbleGunShoot : MonoBehaviour
{

    public GameObject bubblePrefab;
    public float startXOffset = 0.0f;
    public float startYOffset = 0.0f;

    public float thrust = 100.0f;
    public int bubblesPerSecond = 20;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ShootWrapper(bubblesPerSecond);
    }

    void ShootWrapper(int perSecond)
    {
        if ( (float) perSecond / 50.0f > Random.value) Shoot();
        Debug.Log(Random.value);
    }

    void Shoot()
    {
        Vector3 startOffset = new Vector3(startXOffset, startYOffset, 0);
        GameObject bubble = (GameObject) Instantiate(bubblePrefab, transform.position + startOffset, transform.rotation);
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * thrust);

    }

}
