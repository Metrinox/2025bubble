using UnityEngine;

public class PatrolEnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject patrolEnemyPrefab;  // The prefab with HorizontalPatrol script
    public float spawnInterval = 5f;      // How often a new enemy spawns

    [Tooltip("If you want each spawned enemy at a specific Y or random Y, adjust here.")]
    public float fixedYPosition = 0f;     // We'll spawn them at this Y by default
    public bool randomizeY = false;       
    public float minY = -3f;
    public float maxY = 3f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        // Choose a Y position
        float yPos = (randomizeY) ? Random.Range(minY, maxY) : fixedYPosition;

        // Create the enemy at that position (X can be anything, but typically center if you're not using a random X for spawn)
        Vector2 spawnPos = new Vector2(0f, yPos);  // Spawn at x=0 or anywhere you like

        // Instantiate the patrol enemy
        GameObject newEnemy = Instantiate(patrolEnemyPrefab, spawnPos, Quaternion.identity);

        // If you want each enemy to patrol at its own Y:
        HorizontalPatrol patrol = newEnemy.GetComponent<HorizontalPatrol>();
        if (patrol != null)
        {
            patrol.patrolY = yPos; 
        }
    }
}
