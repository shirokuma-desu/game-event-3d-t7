using UnityEngine;

public class SoulSpawner : MonoBehaviour
{
    [Header("Prototype")]
    [SerializeField]
    private GameObject m_prototype;
    public GameObject Prototype { get => m_prototype; }

    [Header("Pools")]
    [SerializeField]
    private SoulObjectPool m_enemyPool;
    public SoulObjectPool Pool { get => m_enemyPool; }

    private void Start()
    {
        m_enemyPool.Spawner = this;
    }

    public void SpawnSoul(Vector3 _position, int _bounty)
    {
        _position.y = 0f;
        var _soul = m_enemyPool.Get(_position);
        _soul.Spawner = this;
        _soul.SetTarget();
        _soul.SetBounty(_bounty);
    }

    public void DespawnSoul(Soul _soul)
    {
        m_enemyPool.Release(_soul);
    }
}
