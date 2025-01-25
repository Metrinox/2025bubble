using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject birdPrefab;          
    public float spawnInterval = 2f;       
    public float minY = -4f;               
    public float maxY = 4f;                
    public float spawnXLeft = -10f;        
    public float spawnXRight = 10f;        

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnBird();
            timer = 0f;
        }
    }

    void SpawnBird()
    {
        
        bool fromLeft = (Random.value < 0.5f);

        float spawnY = Random.Range(minY, maxY);

        float spawnX = fromLeft ? spawnXLeft : spawnXRight;
        Vector2 spawnPos = new Vector2(spawnX, spawnY);

        GameObject newBird = Instantiate(birdPrefab, spawnPos, Quaternion.identity);

        Bird birdScript = newBird.GetComponent<Bird>();
        birdScript.movingRight = fromLeft; // if fromLeft, then  move to  right
    }
}
