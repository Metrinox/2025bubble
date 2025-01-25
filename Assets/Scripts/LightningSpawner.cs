using UnityEngine;
using System.Collections;

public class LightningSpawner : MonoBehaviour
{
    [Header("Lightning Settings")]
    public GameObject warningZonePrefab;      // visual warning prefab
    public GameObject lightningPrefab;        //  lightning prefab 
    
    public float strikeInterval = 6f;         

    public float minX = -8f;   
    public float maxX = 8f;    
    public float spawnY = 0f;  
    
    void Start()
    {
        StartCoroutine(SpawnLightningRoutine());
    }

    private IEnumerator SpawnLightningRoutine()
    {
        while (true)
        {
            float randomX = Random.Range(minX, maxX);
            GameObject warningZone = Instantiate(
                warningZonePrefab,
                new Vector3(randomX, spawnY, 0f),
                Quaternion.identity
            );
            
            float warningDuration = strikeInterval / 2f;
            yield return new WaitForSeconds(warningDuration);

            Destroy(warningZone);

            GameObject lightning = Instantiate(
                lightningPrefab,
                new Vector3(randomX, spawnY, 0f),
                Quaternion.identity
            );

            float lightningDuration = strikeInterval / 2f;
            yield return new WaitForSeconds(lightningDuration);

            Destroy(lightning);

        }
    }
}
