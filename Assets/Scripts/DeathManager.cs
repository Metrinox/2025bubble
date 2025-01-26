using System.Collections;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEngine.Vector3 checkpoint = new UnityEngine.Vector3(0,0);
    public CameraBehavior camera;
    public Bubble bubblePrefab;
    public Bubble bubble;
    public TextMeshProUGUI respawnText;
    public TextMeshProUGUI respawnCountDown;
    public void Start()
    {
        respawnCountDown.enabled = false;
        respawnText.enabled = false;
        // RespawnBubble();
    }

    public void RespawnBubble() {
        Bubble bubble = Instantiate(bubblePrefab, checkpoint, UnityEngine.Quaternion.identity);
        bubble.manager = this;
        camera.player = bubble;
        camera.transform.position = new UnityEngine.Vector3(bubble.transform.position.x,bubble.transform.position.z, camera.transform.position.z);
    }

    public void Die() {
        Debug.Log("Bubble has entered death stages");
        respawnText.enabled = true;
        respawnCountDown.enabled = true;
        respawnText.transform.position = new UnityEngine.Vector3(camera.transform.position.x, camera.transform.position.y + 2, respawnText.transform.position.z);//camera.transform.position + 3 * UnityEngine.Vector3.up - new UnityEngine.Vector3(0,0,1);
        respawnCountDown.transform.position = new UnityEngine.Vector3(camera.transform.position.x, camera.transform.position.y - 2, respawnCountDown.transform.position.z);
        // Destroy(bubble, 0);
        StartCoroutine(CountDown(3));
    }

    public IEnumerator CountDown(int n)
    {
        Debug.Log("Now counting down");
        // Start the countdown
        for (int i = n; i > 0; i--)
        {
            // Update the UI Text with the current remaining seconds
            respawnCountDown.text = i.ToString();
            yield return new WaitForSeconds(1); // Wait for 1 second
        }

        // Trigger respawn once the countdown is complete
        respawnCountDown.enabled = false;
        respawnText.enabled = false;
        RespawnBubble();
    }

}
