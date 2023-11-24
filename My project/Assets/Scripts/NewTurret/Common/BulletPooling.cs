using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : GenericObjectPool<NormalBulletScript>
{
    private BulletSpawner m_spawner;
    public BulletSpawner Spawner
    {
        get => m_spawner;
        set => m_spawner = value;
    }

    public List<NormalBulletScript> GetActiveEnemies()
    {
        return new List<NormalBulletScript>(GetComponentsInChildren<NormalBulletScript>());
    }

    protected override NormalBulletScript OnCreateInstance()
    {
        GameObject _bulletIntance = Instantiate(
            m_spawner.BulletPrefab, Vector3.zero, Quaternion.identity, transform
        );
        _bulletIntance.GetComponent<NormalBulletScript>().Spawner = m_spawner;

        return _bulletIntance.GetComponent<NormalBulletScript>();
    }
}