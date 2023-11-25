using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : GenericObjectPool<Bullet>
{
    private BulletSpawner m_spawner;
    public BulletSpawner Spawner
    {
        get => m_spawner;
        set => m_spawner = value;
    }

    public List<Bullet> GetActiveEnemies()
    {
        return new List<Bullet>(GetComponentsInChildren<Bullet>());
    }

    protected override Bullet OnCreateInstance()
    {
        GameObject _bulletIntance = Instantiate(
            m_spawner.BulletPrefab, Vector3.zero, Quaternion.identity, transform
        );
        _bulletIntance.GetComponent<Bullet>().Spawner = m_spawner;

        return _bulletIntance.GetComponent<Bullet>();
    }
}