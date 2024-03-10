using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
public class EnemySpawner : MonoBehaviour
{
    // Reference to the enemy prefab to be spawned
    public GameObject enemyPrefab;
    // Time interval between enemy spawns
    public float spawnInterval = 3f; 
    // Radius around the spawner where enemies can spawn
    public float spawnRadius = 5f; 
    // Maximum number of enemies to spawn for this spawner
    [HideInInspector] public int maxSpawnCount; 
    // Counter for the number of spawned enemies
    private int currentSpawnCount = 0; 
    // Reference to the current enemy prefab
    private GameObject currentEnemyPrefab; 

    public Tilemap walkableTilemap;

    public void SetSpawnCount(int spawnCount)
    {
        maxSpawnCount = spawnCount;
    }

    public void SetEnemyPrefab(GameObject prefab)
    {
        currentEnemyPrefab = prefab;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (currentSpawnCount < maxSpawnCount)
        {
            // Spawn an enemy using the current enemy prefab
            SpawnEnemy();

            // Increase the spawn count
            currentSpawnCount++;

            // Wait for the specified interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }
    }

void SpawnEnemy()
{
    // Check if a valid enemy prefab and tilemap are set
    if (currentEnemyPrefab != null && walkableTilemap != null)
    {
        // Generate a random position within the spawn radius
        Vector2 spawnPosition = (Random.insideUnitCircle * spawnRadius) + (Vector2)transform.position;

        // Instantiate the current enemy prefab at the spawn position
        GameObject enemyInstance = Instantiate(currentEnemyPrefab, spawnPosition, Quaternion.identity);

        // Attach the tilemap reference to the spawned enemy's AutoMove script
        AutoMove autoMoveScript = enemyInstance.GetComponent<AutoMove>();

        if (autoMoveScript != null)
        {
            // Set the Tilemap directly to prevent mistype error
            autoMoveScript.walkableTilemap = walkableTilemap; 
        }
        else
        {
            Debug.LogError("Enemy prefab is missing the AutoMove script!");
        }
    }
    else
    {
        Debug.LogError("No enemy prefab or walkable tilemap set for spawning!");
    }
}
}
