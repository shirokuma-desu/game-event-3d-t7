using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemySpawner> m_spawners;

    [Header("Enemy Form")]
    [SerializeField]
    private List<GameObject> m_forms;

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

    public void SpawnEnemyForm(int _index)
    {
        foreach (Transform _templateTransform in m_forms[_index].transform)
        {
            int _templateID = _templateTransform.GetComponent<EnemyFormTemplate>().ID;

            Vector3 _position = EnvironmentManager.Instance.EnemySpawnZone.GetSpawnPoint() + _templateTransform.position;
            m_spawners[_templateID].SpawnEnemy(_position);
        }
    }

    private void Start()
    {
        foreach (EnemySpawner _spawner in m_spawners)
        {
            _spawner.Manager = this;
        }

        StartAllSpawner();
    }
}
