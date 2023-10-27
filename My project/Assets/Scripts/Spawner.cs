using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Collider[] spawnAreas;

    private int maxEnemy = 20;
    [SerializeField]
    private int _maxEnemy;
    [SerializeField]
    private float spawnInterval = 2f;
    [SerializeField]
    private float waveInterval = 10f;
    [SerializeField]
    private int currentWave = 1;

    //[SerializeField]
    //private float groupSpawnChance = 0.2f;

    private int currentEnemy = 0;
    private float currentSpawnInterval = 0f;
    private float currentWaveInterval = 0f;

    private void Start()
    {
        _maxEnemy = maxEnemy;
    }

    private void Update()
    {
            SpawnSingleEnemy();
    }

    void SpawnSingleEnemy()
    {
        if (currentWaveInterval <= 0f && currentEnemy <= _maxEnemy)
        {
            if (currentSpawnInterval <= 0f)
            {
                int randomIndex = Random.Range(0, spawnAreas.Length);
                Vector3 spawnPos = GetRandomPointInBounds(spawnAreas[randomIndex].bounds);
                spawnPos.y = 0f;

                GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

                currentSpawnInterval = spawnInterval;
                currentEnemy++;
            }
            else
            {
                currentSpawnInterval -= Time.deltaTime;
            }

            if (currentEnemy >= _maxEnemy)
            {
                currentWaveInterval = waveInterval;
                currentEnemy = 0;
                currentWave++;
            }
        }
        else
        {
            currentWaveInterval -= Time.deltaTime;
        }
    }

    //void SpawnGroupEnemy(int numberEnemy)
    //{
    //    if (currentWaveInterval <= 0f)
    //    {
    //        if (currentSpawnInterval <= 0f)
    //        {
    //            Debug.Log("Spawn Group");
    //            int randomIndex = Random.Range(0, spawnAreas.Length);
    //            Vector3 spawnPos = GetRandomPointInBounds(spawnAreas[randomIndex].bounds);

    //            float groupRadius = 10f;

    //            for (int i = 0; i < numberEnemy; i++)
    //            {
    //                do
    //                {
    //                    Vector3 randomOffset = Random.insideUnitCircle * groupRadius;
    //                    spawnPos = spawnPos + randomOffset;
    //                } while (!spawnAreas[randomIndex].bounds.Contains(spawnPos));
    //                spawnPos.y = 0f;

    //                GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    //            }
    //            currentSpawnInterval = spawnInterval;
    //            currentEnemy += numberEnemy;
    //        }
    //        else
    //        {
    //            currentSpawnInterval -= Time.deltaTime;
    //        }

    //        if (currentEnemy >= _maxEnemy)
    //        {
    //            currentWaveInterval = waveInterval;
    //            currentEnemy = 0;
    //            currentWave++;
    //        }
    //    }
    //    else
    //    {
    //        currentWaveInterval -= Time.deltaTime;
    //    }
    //}

    Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        return new Vector3(x, y, z);
    }
}
