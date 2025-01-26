using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpaceLevelManager : MonoBehaviour
{
    public GameObject player; 
    public TextMeshProUGUI endText; 
    public Camera mainCamera; 
    private float exitCountDown = 5f; 
    private Animation bubbleDeath; 

    void Main() 
    {
        bubbleDeath = player.GetComponent<Animation>(); 
    }


    // Update is called once per frame
    void Update()
    {
        if (player != null && player.transform.position.y > 45) {
            StartCoroutine(GameEnd()); 
        }

        if (player != null && player.transform.position.y < -80) {
            StartCoroutine(AlternateEnd()); 
        }
    }

    private IEnumerator GameEnd() {
        // bubbleDeath.Play("BubbleDie"); 
        Destroy(player); 
        endText.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 2, endText.transform.position.z);
        yield return new WaitForSeconds(exitCountDown);
        Application.Quit(); 
    }

    private IEnumerator AlternateEnd() 
    {
        Destroy(player); 
        endText.text = "Nice try, but it's too late. There's no going back now."; 
        endText.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 2, endText.transform.position.z);
        yield return new WaitForSeconds(exitCountDown);
        SceneManager.LoadScene(4);
    }
}
