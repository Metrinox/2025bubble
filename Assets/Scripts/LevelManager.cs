using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEngine.Vector3 checkpoint;
    public Camera camera;
    public Bubble bubblePrefab;
    public Bubble bubble;
    public TextMeshProUGUI respawnText;
    public TextMeshProUGUI respawnCountDown;
    public List<GameObject> disabled = new List<GameObject> {};
    public void Start()
    {
        respawnCountDown.enabled = false;
        respawnText.enabled = false;
        ResetEnemy();
        RespawnBubble();
    }

    public void RespawnBubble() {
        Bubble bubble = Instantiate(bubblePrefab, checkpoint, UnityEngine.Quaternion.identity);
        bubble.manager = this;
        camera.player = bubble;
        camera.transform.position = new UnityEngine.Vector3(bubble.transform.position.x,bubble.transform.position.z, camera.transform.position.z);
    }

    public void Die() {
        respawnText.enabled = true;
        respawnCountDown.enabled = true;
        respawnText.transform.position = new UnityEngine.Vector3(camera.transform.position.x, camera.transform.position.y + 2, respawnText.transform.position.z);//camera.transform.position + 3 * UnityEngine.Vector3.up - new UnityEngine.Vector3(0,0,1);
        respawnCountDown.transform.position = new UnityEngine.Vector3(camera.transform.position.x, camera.transform.position.y - 2, respawnCountDown.transform.position.z);
        StartCoroutine(CountDown(3));
    }

    public void ResetEnemy() {
        GameObject[] chasers = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in chasers) {
            Enemy enemy = obj.GetComponent<Enemy>();
            enemy.manager = this;
            // chaser.manager = this;
            enemy.Reset();
        }
    }

    public IEnumerator CountDown(int n)
    {
        ResetEnemy();
        // Iterate through the list in reverse
        for (int i = disabled.Count - 1; i >= 0; i--)
        {
            // Set the GameObject to active
            disabled[i].SetActive(true);

            // Remove the GameObject from the list
            disabled.RemoveAt(i);
        }
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
