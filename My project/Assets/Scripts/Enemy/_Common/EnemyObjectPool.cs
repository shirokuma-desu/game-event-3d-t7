using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : GenericObjectPool<Enemy>
{
    private EnemySpawner m_spawner;
    public EnemySpawner Spawner
    {
        get => m_spawner;
        set => m_spawner = value;
    }

    public List<Enemy> GetActiveEnemies()
    {
        return new List<Enemy>(GetComponentsInChildren<Enemy>());
    }

    protected override Enemy OnCreateInstance()
    {
        GameObject _enemyInstance = Instantiate(
            m_spawner.EnemyPrototype, Vector3.zero, Quaternion.identity, transform
        );
        _enemyInstance.GetComponent<Enemy>().Spawner = m_spawner;

        return _enemyInstance.GetComponent<Enemy>();
    }
}
