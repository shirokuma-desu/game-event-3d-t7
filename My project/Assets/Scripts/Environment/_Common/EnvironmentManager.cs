using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : GenericSingleton<EnvironmentManager>
{
    [Header("References")]
    [SerializeField]
    private GameObject m_player;
    [SerializeField]
    private GameObject[] m_towerSpot = new GameObject[4];
    [SerializeField]
    private EnemySpawnZone m_enemySpawnZone;
    public EnemySpawnZone EnemySpawnZone { get => m_enemySpawnZone; }

    private bool[] m_settledTower = new bool[4];

    private int m_currentTowerNumber;

    private bool m_isAnyTowerLeft;
    public bool IsAnyTowerLeft { get => m_isAnyTowerLeft; }

    public Vector3 PlayerPosition
    {
        get => m_player.transform.position;
    }

    public Vector3 GetTowerPosition(int _index)
    {
        return m_towerSpot[_index].transform.position;
    }

    public bool IsTowerSettled(int _index)
    {
        return m_settledTower[_index];
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

    //
    private void Start()
    {
        m_currentTowerNumber = 0;
        m_isAnyTowerLeft = false;
    }
}
