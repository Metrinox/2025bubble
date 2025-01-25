using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    // Defines positions where enemies will spawn 
    public Transform[] spawnPoints; 
    public Transform player; 
    public float spawnDelay = 2f;
    public float spawnRadius = 20f; 

    private float nextSpawnTime;  
    
    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTime && Vector3.Distance(player.position, transform.position) < spawnRadius) {
            SpawnEnemy(); 
            nextSpawnTime = Time.time + spawnDelay;
        }
    }

    void SpawnEnemy() 
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length); 
        Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
    }
}
