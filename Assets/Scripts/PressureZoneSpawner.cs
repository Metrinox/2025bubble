using UnityEngine;

public class PressureZoneSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject pressureZonePrefab;   
    public float spawnInterval = 5f;        

    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -4f;
    public float maxY = 4f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPressureZone();
            timer = 0f;
        }
    }

    void SpawnPressureZone()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector2 spawnPos = new Vector2(randomX, randomY);
        Instantiate(pressureZonePrefab, spawnPos, Quaternion.identity);
    }
}
