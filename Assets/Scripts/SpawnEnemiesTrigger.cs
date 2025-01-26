using UnityEngine;

public class SpawnEnemiesTrigger : MonoBehaviour
{
    public GameObject[] enemies;
    void ActivateSpawn()
    {
        foreach (var enemy in enemies)
            enemy.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ActivateSpawn();
        }
    }

}
