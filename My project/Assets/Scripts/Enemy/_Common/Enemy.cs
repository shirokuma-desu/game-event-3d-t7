using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    protected Rigidbody m_body;

    [Header("Stats")]
    [SerializeField]
    private float m_health;
    public float Health { get => m_health; }

    [SerializeField]
    private float m_moveSpeed;
    public float MoveSpeed { get => m_moveSpeed; }
    private const float m_speedScale = 0.05f;

    [SerializeField]
    private float m_attackDamage;
    public float AttackDamage { get => m_attackDamage; }

    [SerializeField]
    private float m_bounty;
    public float Bounty { get => m_bounty; }

    protected Vector3 m_target;

    public virtual void TakeDamage(float _ammount)
    {
        m_health -= _ammount;
    }

    public virtual void TakeDisable(float _time)
    {

    }

    public virtual void Die()
    {

    }

    public virtual void Attack()
    {

    }

    public virtual void SetTarget()
    {
        m_target = GetTarget();
    }

    protected virtual void Move()
    {
        Vector3 _direction = (m_target - transform.position).normalized;
        _direction.y = transform.position.y;

        transform.forward = _direction;
        m_body.velocity = _direction * m_moveSpeed * m_speedScale;
    }

    protected virtual Vector3 GetTarget()
    {
        Vector3 _target = Vector3.zero;

        if (!EnvironmentManager.Instance.IsAnyTowerLeft)
        {
            _target = EnvironmentManager.Instance.GetTowerPosition(0);
            for (int i = 1; i < 4; i++)
            {
                Vector3 _towerPosition = EnvironmentManager.Instance.GetTowerPosition(i);
                if (Vector3.Distance(transform.position, _towerPosition) < Vector3.Distance(transform.position, _target))
                {
                    _target = _towerPosition;
                }
            }
        }
        else
        {
            _target = EnvironmentManager.Instance.PlayerPosition;
        }

        return _target;
    } 
}
