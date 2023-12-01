using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulObjectPool : GenericObjectPool<Soul>
{
    private SoulSpawner m_spawner;
    public SoulSpawner Spawner
    {
        get => m_spawner;
        set => m_spawner = value;
    }

    public List<Soul> GetActiveEnemies()
    {
        return new List<Soul>(GetComponentsInChildren<Soul>());
    }

    protected override Soul OnCreateInstance()
    {
        GameObject _enemyInstance = Instantiate(
            m_spawner.Prototype, Vector3.up * 1000f, Quaternion.identity, transform
        );

        return _enemyInstance.GetComponent<Soul>();
    }
}
