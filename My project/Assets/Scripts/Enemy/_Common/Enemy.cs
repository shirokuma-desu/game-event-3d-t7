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

    [Header("GameEvents")]
    [SerializeField]
    private GameEvent m_anEnemyDie;
    [SerializeField]
    private GameEvent m_anEnemyAttacking;

    protected Vector3 m_target;

    public virtual void TakeDamage(float _ammount)
    {
        m_health -= _ammount;
    }

    public virtual void TakeDisable(float _time)
    {

    }

    protected virtual void Die()
    {
        m_anEnemyDie.RaiseEvent();
    }

    public virtual void Attack()
    {
        m_anEnemyAttacking.RaiseEvent();
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

        if (EnvironmentManager.Instance.IsAnyTowerLeft)
        {
            float _tempDistance = Mathf.Infinity;
            for (int i = 1; i < 4; i++)
            {
                if (EnvironmentManager.Instance.IsTowerSettled(i))
                {
                    Vector3 _towerPosition = EnvironmentManager.Instance.GetTowerPosition(i);
                    float _towerDistance = Vector3.Distance(transform.position, _towerPosition);
                    if (_towerDistance < _tempDistance)
                    {
                        _tempDistance = _towerDistance;
                        _target = _towerPosition;
                    }
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
