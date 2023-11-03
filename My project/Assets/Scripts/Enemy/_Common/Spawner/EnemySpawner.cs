using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prototype")]
    [SerializeField]
    private GameObject m_enemyPrototype;
    public GameObject EnemyPrototype
    {
        get => m_enemyPrototype;
    }

    [Header("Spawn")]
    [Space(15f)]
    [SerializeField]
    private float m_spawningDelay = 0f;

    [SerializeField]
    private float m_spawningInterval = 1f;

    [Range(0f, 1f)] [SerializeField]
    private float m_spawningProbability = 1f;

    [Header("Pools")]
    [SerializeField]
    private EnemyObjectPool m_enemyPool;

    private void Start()
    {
        m_enemyPool.Spawner = this;

        InvokeRepeating(nameof(CalculatePosition), m_spawningDelay, m_spawningInterval);
    }

    private void CalculatePosition()
    {
        float _probability = Random.Range(0f, 1f);
        if (_probability < m_spawningProbability)
        {
            Vector3 position = EnvironmentManager.Instance.EnemySpawnZone.GetSpawnPoint();
            SpawnEnemy(position);
        }
    }

    int _count = 0;
    private void SpawnEnemy(Vector3 _position)
    {
        var enemy = m_enemyPool.Get(_position);
        Debug.Log(++_count);

        StartCoroutine(DespawnEnemy(enemy));
    }

    private IEnumerator DespawnEnemy(Enemy _enemy)
    {
        yield return new WaitUntil(() => _enemy.IsDied);
        m_enemyPool.Release(_enemy);
    }
}
