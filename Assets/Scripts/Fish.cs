using System.Collections;
using UnityEngine;

public class HorizontalPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    public float minX = -8.8f;      // The minimum X boundary for the patrol points
    public float maxX = 8.8f;       // The maximum X boundary for the patrol points
    public float patrolY = 0f;    // The Y-position at which this enemy patrols (fixed)
    public float speed = 2f;      // Movement speed
    public float escapeVelocity = 5f; // Speed at which fish escapes at
    public float maxAge = 10.0f;

    private Vector2 pointA;       // First random endpoint
    private Vector2 pointB;       // Second random endpoint
    private Vector2 target;       // Current target point
    private float currentAge;

    private bool isEscaping = false;
    private Vector3 escapeDirection = Vector2.zero;

    void Start()
    {
        float x1 = Random.Range(minX, maxX);
        float x2 = Random.Range(minX, maxX);

        pointA = new Vector2(x1, patrolY);
        pointB = new Vector2(x2, patrolY);

        transform.position = pointA;

        target = pointB;

        currentAge = maxAge;
    }

    void Update()
    {
        if (!isEscaping)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target,
                speed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, target) < 0.01f)
            {
                target = (target == pointA) ? pointB : pointA;
            }
        }
        else
        {
            transform.position += escapeDirection * Time.deltaTime * escapeVelocity;
        }
        currentAge -= Time.deltaTime;
        if (currentAge < 0f) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject go_other = other.gameObject;
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go_other.CompareTag("Projectile"))
        {
            isEscaping = true;
            escapeDirection = (transform.position - go.transform.position).normalized;
        }



    }


}
