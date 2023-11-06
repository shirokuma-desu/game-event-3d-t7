using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prototype")]
    [SerializeField]
    private GameObject m_enemyPrototype;
    public GameObject EnemyPrototype { get => m_enemyPrototype; }

    [Header("Spawn")]
    [Space(15f)]
    [SerializeField]
    private float m_spawnYPosition = 0f;
    [SerializeField]
    private float m_spawningInterval = 1f;
    [Range(0f, 1f)] [SerializeField]
    private float m_spawningProbability = 1f;

    [Header("Pools")]
    [SerializeField]
    private EnemyObjectPool m_enemyPool;

    public EnemySpawnManager Manager { get; set; }

    public void SetSpawningInterval(float _value)
    {
        m_spawningInterval = _value;
    }
    public void SetSpawningProbability(float _value)
    {
        m_spawningProbability = _value;
    }

    private void Start()
    {
        m_enemyPool.Spawner = this;
    }

    public void StartSpawning()
    {
        InvokeRepeating(nameof(SpawnRandomPosition), 0f, m_spawningInterval);
    }

    private void SpawnRandomPosition()
    {
        float _probability = Random.Range(0f, 1f);
        if (_probability < m_spawningProbability)
        {
            Vector3 position = EnvironmentManager.Instance.EnemySpawnZone.GetSpawnPoint();
            SpawnEnemy(position);
        }
    }

    public void SpawnEnemy(Vector3 _position)
    {
        _position.y = m_spawnYPosition;
        var enemy = m_enemyPool.Get(_position);

        StartCoroutine(DespawnEnemy(enemy));
    }

    private IEnumerator DespawnEnemy(Enemy _enemy)
    {
        yield return new WaitUntil(() => _enemy.IsDied);
        m_enemyPool.Release(_enemy);
    }
}
