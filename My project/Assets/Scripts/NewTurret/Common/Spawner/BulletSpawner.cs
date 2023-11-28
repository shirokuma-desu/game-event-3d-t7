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

    [Header("Pools")]
    [SerializeField]
    private BulletPooling m_bulletPool;

    public TurretManager Manager { get; set; }

    private void Start()
    {
        m_bulletPool.Spawner = this;
    }

    public Bullet SpawnBullet(Vector3 _position)
    {
        _position.y = m_spawnYPosition;
        var bullet = m_bulletPool.Get(_position);
        return bullet.GetComponent<Bullet>();
    }

    public void DespawnBullet(Bullet _bullet)
    {
        m_bulletPool.Release(_bullet);
    }
}
