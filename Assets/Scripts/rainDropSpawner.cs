using UnityEngine;

public class rainDropSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject raindropPrefab;      
    public float spawnInterval = 0.2f;    
    public float minX = -8f;               
    public float maxX = 8f;                
    public float spawnY = 6f;           

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRaindrop();
            timer = 0f;
        }
    }

    void SpawnRaindrop()
    {
        float randomX = Random.Range(minX, maxX);

        Vector2 spawnPos = new Vector2(randomX, spawnY);

        Instantiate(raindropPrefab, spawnPos, Quaternion.identity);
    }
}
