using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go != null) {
            transform.position = go.transform.position;
        } 
    }
}
