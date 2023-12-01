using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnZone : MonoBehaviour
{
    [SerializeField]
    public Vector3 m_mainPivot;
    [SerializeField]
    public float m_mainRadius;
    [SerializeField]
    public float m_mainMaxAngle;

    [SerializeField]
    public float m_zoneRadius;

    public Vector3 GetSpawnPoint()
    {
        float _randomMainAngle = Random.Range(-m_mainMaxAngle, m_mainMaxAngle);
        Vector3 _randomZonePivot = m_mainPivot + new Vector3(Mathf.Sin(_randomMainAngle * Mathf.Deg2Rad) * m_mainRadius, 0f, Mathf.Cos(_randomMainAngle * Mathf.Deg2Rad) * -m_mainRadius);

        Vector2 _randomCircleSpawn = Random.insideUnitCircle * Random.Range(0f, m_zoneRadius);

        Vector3 _targetSpawnPoint = _randomZonePivot;
        _targetSpawnPoint.x += _randomCircleSpawn.x;
        _targetSpawnPoint.z += _randomCircleSpawn.y;

        return _targetSpawnPoint;
    }
}
