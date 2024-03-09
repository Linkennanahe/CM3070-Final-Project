using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MasterSpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs; // List of enemy prefabs to be spawned
    public float waveInterval = 60f; // Time interval between waves (in seconds) for waves 2 and above
    public float firstWaveDelay = 5f; // Initial delay before the first wave starts (in seconds)
    public float spawnRadius = 5f; // Radius around the spawner where enemies can spawn
    public GameObject[] spawners; // Array of spawner objects controlled by the master spawner

    private int currentWave = 0;

    private void Start()
    {
        // Start spawning waves at intervals
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        // Initial delay before the first wave
        yield return new WaitForSeconds(firstWaveDelay);

        while (currentWave < 50) // Adjust the maximum wave count as needed
        {
            if (currentWave > 1)
            {
                // Apply wave interval only for waves 2 and above
                yield return new WaitForSeconds(waveInterval);
            }

            // Increment the wave count
            currentWave++;

            // Start distributing spawn counts and enemy prefabs to individual spawners
            StartCoroutine(DistributeSpawnCounts(currentWave));
        }
    }

    IEnumerator DistributeSpawnCounts(int wave)
    {
        List<int> spawnCounts = new List<int>();
        int remainingSpawnCount = GetTotalMaxSpawnCountForWave(wave);
        int spawnerCount = spawners.Length;

        // Ensure there is at least 1 spawn count for each spawner
        for (int i = 0; i < spawnerCount; i++)
        {
            if (i == spawnerCount - 1)
            {
                // For the last spawner, assign the remaining spawn count
                spawnCounts.Add(remainingSpawnCount);
            }
            else
            {
                // Generate spawn counts based on the remaining spawn count
                int randomSpawnCount = Random.Range(1, remainingSpawnCount - (spawnerCount - i - 1) + 1);
                spawnCounts.Add(randomSpawnCount);
                remainingSpawnCount -= randomSpawnCount;
            }
        }

        // Shuffle the spawn counts to distribute randomly
        spawnCounts = spawnCounts.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < spawners.Length; i++)
        {
            EnemySpawner enemySpawner = spawners[i].GetComponent<EnemySpawner>();

            if (enemySpawner != null)
            {
                // Set the spawn count for the spawner
                enemySpawner.SetSpawnCount(spawnCounts[i]);

                // Randomly assign an enemy prefab to the spawner
                GameObject randomEnemyPrefab = GetRandomEnemyPrefab();
                enemySpawner.SetEnemyPrefab(randomEnemyPrefab);

                // Start spawning on the spawner
                enemySpawner.StartSpawning();
            }
        }

        yield return null;
    }

    int GetSpawnCountForWave(int wave)
    {
        // Wave spawn count == wave number * X
        return 5 + (wave - 1) * 2;
    }

    int GetTotalMaxSpawnCountForWave(int wave)
    {
        // Calculate the total max spawn count for the given wave
        int totalMaxSpawnCount = 0;
        for (int i = 1; i <= wave; i++)
        {
            totalMaxSpawnCount += GetSpawnCountForWave(i);
        }
        return totalMaxSpawnCount;
    }

    GameObject GetRandomEnemyPrefab()
    {
        // Randomly select an enemy prefab from the list
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
    }
}
