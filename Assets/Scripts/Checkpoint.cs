using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public LevelManager manager;


    private void Start()
    {
        manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            manager.checkpoint = transform.position;
        }
    }
}
