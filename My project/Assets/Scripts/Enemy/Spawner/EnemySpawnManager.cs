using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemySpawner> m_spawners;

    public void StartAllSpawner()
    {
        foreach (EnemySpawner _spawner in m_spawners)
        {
            _spawner.StartSpawning();
        }
    }

    public void StartSpawner(int _index)
    {
        m_spawners[_index].StartSpawning();
    }

    private void Start()
    {
        StartAllSpawner();
    }
}
