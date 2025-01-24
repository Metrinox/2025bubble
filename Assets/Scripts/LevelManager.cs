using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEngine.Vector3 checkpoint = new UnityEngine.Vector3(0,0);
    public Camera camera;
    public Bubble bubblePrefab;
    public Bubble bubble;
    public void Start()
    {
        RespawnBubble();
    }

    public void RespawnBubble() {
        Instantiate(bubblePrefab, checkpoint, UnityEngine.Quaternion.identity);
        bubble = GetComponent<Bubble>();
        camera.player = bubble;
    }
}
