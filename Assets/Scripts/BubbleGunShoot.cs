using UnityEngine;
using UnityEngine.InputSystem;
public class BubbleGunShoot : MonoBehaviour
{

    public GameObject bubblePrefab;
    public float startXOffset = 0.0f;
    public float startYOffset = 0.0f;

    public float thrust = 200.0f;
    public float bubblesPerSecond = 20.0f;
    public float angularSpray = 15.0f;
    public Vector3 gunDirection = new Vector3(1, 0, 0);



    void Update()
    {

        bool canShoot = GetComponentInParent<GunAim>().canShoot;

        if (Input.GetMouseButton(0) && canShoot)
        {
            ShootWrapper(bubblesPerSecond);
            if (transform.parent.transform.parent != null)
            {
                Rigidbody2D rb = transform.parent.transform.parent.GetComponent<Rigidbody2D>();
                rb.AddRelativeForce(-gunDirection);
            }
        }

    }

    void ShootWrapper(float perSecond)
    {
        if (perSecond / 50.0f > Random.value) Shoot();
        Debug.Log(Random.value);
    }

    void Shoot()
    {
        gunDirection = transform.parent.rotation * Vector3.right;
        Vector3 startOffset = new(startXOffset, startYOffset, 0);
        Quaternion rotation = Quaternion.AngleAxis(angularSpray * (Random.value-0.5f), Vector3.back);
        Vector3 direction = rotation * gunDirection.normalized;

        GameObject bubble = (GameObject) Instantiate(bubblePrefab, transform.position + startOffset, transform.rotation);
        Rigidbody2D rb = bubble.GetComponent<Rigidbody2D>();
        
        rb.AddForce(direction * thrust);

    }

}
