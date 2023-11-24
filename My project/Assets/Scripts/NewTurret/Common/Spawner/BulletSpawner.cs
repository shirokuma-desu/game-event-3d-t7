using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Prototype")]
    [SerializeField]
    private GameObject m_bulletPrefab;
    public GameObject BulletPrefab { get => m_bulletPrefab; }

    [Header("Spawn")]
    [Space(15f)]
    [SerializeField]
    private float m_spawnYPosition = 0f;
    [SerializeField]
    private float m_spawningInterval = 1f;
    [Range(0f, 1f)]
    [SerializeField]
    private float m_spawningProbability = 1f;

    [Header("Pools")]
    [SerializeField]
    private BulletPooling m_bulletPool;

    public TurretManager Manager { get; set; }

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
        m_bulletPool.Spawner = this;
    }

    public NormalBulletScript SpawnBullet(Vector3 _position)
    {
        _position.y = m_spawnYPosition;
        var bullet = m_bulletPool.Get(_position);
        return bullet.GetComponent<NormalBulletScript>();
    }

    public void DespawnBullet(NormalBulletScript _bullet)
    {
        m_bulletPool.Release(_bullet);
    }
}
