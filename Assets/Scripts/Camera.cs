using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//let camera follow target
public class Camera : MonoBehaviour
{
    public GameObject player;
    public float lerpSpeed = 1.0f;

    private Vector3 offset;

    private Vector3 targetPos;

    private void Start()
    {
        if (player == null) return;
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y, transform.position.z);

        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        if (player == null) return;

        targetPos = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
    }

}
