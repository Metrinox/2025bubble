using UnityEngine;

public class SulphurBubbleSpawner : MonoBehaviour
{
    public GameObject sulphurBubblePrefab;
    public float spawnInterval = 3f;

    public float minX = -8f;
    public float maxX = 8f;
    public float spawnY = -5f; // Typically below camera

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnBubble();
            timer = 0f;
        }
    }

    private void SpawnBubble()
    {
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPos = new Vector2(randomX, spawnY);

        Instantiate(sulphurBubblePrefab, spawnPos, Quaternion.identity);
    }
}
