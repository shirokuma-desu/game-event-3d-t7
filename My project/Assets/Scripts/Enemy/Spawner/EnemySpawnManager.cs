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

            Vector3 _spawnPosition = EnvironmentManager.Instance.EnemySpawnZone.GetSpawnPoint() + _templateTransform.position;
            m_spawners[_templateID].SpawnEnemy(_spawnPosition);
        }
    }

    public void SpawnEnemyForm(string _name)
    {
        foreach (GameObject _form in m_forms)
        {
            if (_form.name == _name)
            {
                foreach (Transform _templateTransform in _form.transform)
                {
                    int _templateID = _templateTransform.GetComponent<EnemyFormTemplate>().ID;

                    Vector3 _spawnPosition = EnvironmentManager.Instance.EnemySpawnZone.GetSpawnPoint() + _templateTransform.position;
                    m_spawners[_templateID].SpawnEnemy(_spawnPosition);
                }

                return;
            }
        }

        Debug.LogWarning("No Formation has such name");
    }
    
    public void SpawnEnemyForm(int _index, Vector3 _position)
    {
        foreach (Transform _templateTransform in m_forms[_index].transform)
        {
            int _templateID = _templateTransform.GetComponent<EnemyFormTemplate>().ID;

            Vector3 _spawnPosition = _position + _templateTransform.position;
            m_spawners[_templateID].SpawnEnemy(_spawnPosition);
        }
    }

    public void SpawnEnemyForm(string _name, Vector3 _position)
    {
        foreach (GameObject _form in m_forms)
        {
            if (_form.name == _name)
            {
                foreach (Transform _templateTransform in _form.transform)
                {
                    int _templateID = _templateTransform.GetComponent<EnemyFormTemplate>().ID;

                    Vector3 _spawnPosition = _position + _templateTransform.position;
                    m_spawners[_templateID].SpawnEnemy(_spawnPosition);
                }

                return;
            }
        }

        Debug.LogWarning("No Formation has such name");
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
