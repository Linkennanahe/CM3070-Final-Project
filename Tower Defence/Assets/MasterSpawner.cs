using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class MasterSpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public float waveInterval = 60f;
    public float firstWaveDelay = 5f;
    public float spawnRadius = 5f;
    public GameObject[] spawners;
    [SerializeField] private TextMeshProUGUI _waveCountText;
    [SerializeField] private TextMeshProUGUI _timeLeftText;
    private int currentWave = 0;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }
    // Spawn Enemy in Waves
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(firstWaveDelay);

        while (currentWave < 50)
        {
            if (currentWave > 1)
            {
                float countdown = waveInterval;

                while (countdown > 0)
                {
                    yield return new WaitForSeconds(1f);
                    countdown -= 1f;
                    UpdateWaveText(countdown);
                }
            }

            currentWave++;

            UpdateWaveText();

            StartCoroutine(DistributeSpawnCounts(currentWave));
        }
    }
    // Distribute the spawn counts so that all spawners get some spawn counts
    IEnumerator DistributeSpawnCounts(int wave)
    {
        List<int> spawnCounts = new List<int>();
        int remainingSpawnCount = GetTotalMaxSpawnCountForWave(wave);
        int spawnerCount = spawners.Length;

        for (int i = 0; i < spawnerCount; i++)
        {
            if (i == spawnerCount - 1)
            {
                spawnCounts.Add(remainingSpawnCount);
            }
            else
            {
                // Split the spawn count at random so that no ever Same wave have same pattern but only same difficulty
                int randomSpawnCount = Random.Range(1, remainingSpawnCount - (spawnerCount - i - 1) + 1);
                spawnCounts.Add(randomSpawnCount);
                remainingSpawnCount -= randomSpawnCount;
            }
        }

        spawnCounts = spawnCounts.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < spawners.Length; i++)
        {
            EnemySpawner enemySpawner = spawners[i].GetComponent<EnemySpawner>();

            if (enemySpawner != null)
            {
                enemySpawner.SetSpawnCount(spawnCounts[i]);

                GameObject randomEnemyPrefab = GetRandomEnemyPrefab();
                enemySpawner.SetEnemyPrefab(randomEnemyPrefab);

                enemySpawner.StartSpawning();
            }
        }

        yield return null;
    }

    int GetSpawnCountForWave(int wave)
    {
        return 5 + (wave - 1) * 2;
    }

    int GetTotalMaxSpawnCountForWave(int wave)
    {
        int totalMaxSpawnCount = 0;
        for (int i = 1; i <= wave; i++)
        {
            totalMaxSpawnCount += GetSpawnCountForWave(i);
        }
        return totalMaxSpawnCount;
    }
    // Get a random enemy prefab everytime it is called, to introduce more variations to the game
    GameObject GetRandomEnemyPrefab()
    {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
    }
    // Shows the Wave Count
    void UpdateWaveText(float countdown = 0f)
    {
    _waveCountText.text = currentWave.ToString();

    if (countdown > 0)
        _timeLeftText.text = countdown.ToString("0") + "s";
    else
        _timeLeftText.text = waveInterval.ToString() + "s";
    }
    // Skip the timer to more difficult waves
    public void IncrementWave()
    {
        currentWave++;
        UpdateWaveText();
        StartCoroutine(DistributeSpawnCounts(currentWave));
    }


}
