using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkyLevelManager : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (player != null && player.transform.position.y > 77) {
            SceneManager.LoadScene(4); 
        }
    }
}
