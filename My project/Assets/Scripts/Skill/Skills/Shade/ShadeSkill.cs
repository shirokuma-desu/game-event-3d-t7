using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeSkill : Skill
{
    [Header("Shade")]
    [SerializeField]
    private int m_obstacleNumber;
    [SerializeField]
    private float m_explodeRange;
    [SerializeField]
    private float m_chaseRange;
    [SerializeField]
    private float m_chaseSpeed;
    [SerializeField]
    private GameObject m_shadeObstacle;

    protected override void Impact()
    {
        for (int i = 0; i < m_obstacleNumber; i++)
        {
            Vector3 _position = GetRandomSpawnPosition();
            GameObject _obstacle = Instantiate(m_shadeObstacle, _position, Quaternion.identity);
            _obstacle.GetComponent<ShadeObstacle>().SetupProperties(m_ID, m_damage, m_explodeRange, m_chaseRange, m_chaseSpeed);
        }

        Expire();
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 _random = Random.insideUnitCircle;
        Vector3 _target = CastPosition;
        _target.x += _random.x * Range;
        _target.z += _random.y * Range;
        return _target;
    }
}
