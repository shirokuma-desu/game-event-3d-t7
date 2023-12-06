using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfallSkill : Skill
{
    [Header("Starfall")]
    [SerializeField]
    private int m_obstacleNumber;
    [SerializeField]
    private float m_explodeRange;
    [SerializeField]
    private GameObject m_starfallObstacle;

    public override void SetUp()
    {
        base.SetUp();

        m_obstacleNumber = m_statData.instance_per_cast;

        m_obstacleNumber += m_statData.instance_increase * m_level;
    }

    protected override void Impact()
    {
        for (int i = 0; i < m_obstacleNumber; i++)
        {
            Vector3 _position = GetRandomSpawnPosition();
            GameObject _obstacle = Instantiate(m_starfallObstacle, _position, Quaternion.identity);
            _obstacle.GetComponent<StarfallObstacle>().SetupProperties(m_ID, m_damage, m_explodeRange);
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
