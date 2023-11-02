using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnZone : MonoBehaviour
{
    [SerializeField]
    private List<Transform> m_spawnPoints;

    [Header("Configs")]
    [SerializeField]
    private float m_spawnYPosition;
    [SerializeField]
    private float m_spawnPointRadius;

    public Vector3 GetSpawnPoint()
    {
        int _pointIndex = Random.Range(0, m_spawnPoints.Count);

        Vector3 _targetSpawnPoint = m_spawnPoints[_pointIndex].position;
        Vector2 _randomCircleSpawn = Random.insideUnitCircle * Random.Range(0f, m_spawnPointRadius);
        _targetSpawnPoint.x += _randomCircleSpawn.x;
        _targetSpawnPoint.y = m_spawnYPosition;
        _targetSpawnPoint.z += _randomCircleSpawn.y;

        return _targetSpawnPoint;
    }
}
