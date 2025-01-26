using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        transform.position = go.transform.position;
    }
}
