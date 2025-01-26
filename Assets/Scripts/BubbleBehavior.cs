using UnityEngine;

public class BubbleBehavior : MonoBehaviour
{

    private float age = 0.0f;
    public float maximumAge = 3.0f;
    public bool isBigBubble = false; // Big Bubbles don't instantly pop

    //public float otherForce = 10.0f;
    //public float noise = 3.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        age += Time.deltaTime;

        if (age > maximumAge) Destroy(gameObject);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector3 v3Velocity = rb.linearVelocity;

        //rb.AddForce(new Vector2((Random.value-0.5f)*noise, (Random.value-0.5f))*noise);

        if (v3Velocity.y < 0) rb.linearDamping = 2.5f;
        else rb.linearDamping = 0.0f;

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (isBigBubble) return;

        GameObject go = other.gameObject;
        //Rigidbody2D rb_other = go.GetComponent<Rigidbody2D>();

        //if (rb_other != null)
        //{
        //    rb_other.AddForce(transform.rotation * Vector3.up * otherForce);
        //}

        if (!go.CompareTag("Friendly") && !go.CompareTag("Player") && !go.CompareTag("Projectile")) Destroy(gameObject);

    }
}
