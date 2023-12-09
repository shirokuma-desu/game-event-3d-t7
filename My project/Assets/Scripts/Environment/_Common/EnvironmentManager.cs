using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : GenericSingleton<EnvironmentManager>
{
    [Header("References")]
    [SerializeField]
    private GameObject m_player;
    public GameObject Player { get => m_player; }
    
    [SerializeField]
    private TurretSpot[] m_towerSpots;

    [SerializeField]
    private EnemySpawnZone m_enemySpawnZone;
    public EnemySpawnZone EnemySpawnZone { get => m_enemySpawnZone; }

    private int m_currentTowerNumber;

    private bool m_isAnyTowerLeft;
    public bool IsAnyTurretLeft { get => m_isAnyTowerLeft; }

    public Vector3 PlayerPosition
    {
        get => m_player.transform.position;
    }

    public Vector3 GetTowerPosition(int _index)
    {
        return m_towerSpots[_index].transform.position;
    }

    public bool IsTowerSettled(int _index)
    {
        return m_towerSpots[_index].IsSettled;
    }

    //
    public void AddATower()
    {
        m_currentTowerNumber++;
        if (m_currentTowerNumber > 0) m_isAnyTowerLeft = true;
    }
    public void RemoveATower()
    {
        if (m_currentTowerNumber <= 0)
        {
            Debug.LogWarning("No more tower to remove.");
            return;
        }

        m_currentTowerNumber--;
        if (m_currentTowerNumber == 0) m_isAnyTowerLeft = false;
    }

    public void SpawnParticle(GameObject _particle, Vector3 _position)
    {
        _position.y = 0f;

        Instantiate(_particle.gameObject, _position, Quaternion.identity);
    }

    //
    private void Start()
    {
        m_currentTowerNumber = 0;
        m_isAnyTowerLeft = false;
    }
}
