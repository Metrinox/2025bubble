using UnityEngine;

public class BarracudaSpawner : MonoBehaviour
{
    public GameObject barracudaPrefab;
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
            SpawnBarracuda();
            timer = 0f;
        }
    }

    private void SpawnBarracuda()
    {

        Vector2 spawnPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

        Instantiate(barracudaPrefab, spawnPos, Quaternion.identity);
    }
}
