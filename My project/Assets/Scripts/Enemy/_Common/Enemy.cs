using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    protected Rigidbody m_body;

    [Header("Stats")]
    [SerializeField]
    private float m_maxHealth;
    public float MaxHealth { get => m_maxHealth; }
    private float m_currentHealth;
    public float CurrentHealth { get => m_currentHealth; }

    [SerializeField]
    private float m_moveSpeed;
    public float MoveSpeed { get => m_moveSpeed; }
    private float m_currentMoveSpeed;
    public float CurrentMoveSpeed { get => m_currentMoveSpeed; }
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

    public EnemySpawner Spawner { get; set; }

    //
    public bool IsDied { get; private set; }

    public bool IsSlowed { get => (SlowAmmount > 0); }
    public float SlowAmmount { get; private set; } 

    protected Vector3 m_target;

    //
    public virtual void TakeDamage(float _ammount)
    {
        m_currentHealth -= _ammount;
    }

    protected virtual void Die()
    {
        IsDied = true;

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

    public virtual void TakeSlowDebuff(float _ammount, float _duration)
    {
        float _trueAmmount = Mathf.Min(_ammount, 100 - SlowAmmount);
        SlowAmmount += _trueAmmount;

        StartCoroutine(SlowDebuffExpire(_trueAmmount, _duration));
    }

    //
    protected virtual void Start()
    {
        m_body = GetComponent<Rigidbody>();

        m_currentHealth = m_maxHealth;
        m_currentMoveSpeed = m_moveSpeed;

        IsDied = false;
        SetTarget();
    }

    protected virtual void Move()
    {
        Vector3 _direction = (m_target - transform.position).normalized;
        _direction.y = 0;

        transform.forward = _direction;
        m_body.velocity = _direction * m_moveSpeed * m_speedScale * ((100f - SlowAmmount) / 100f);
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

    //
    private IEnumerator SlowDebuffExpire(float _ammount, float _duration)
    {
        yield return new WaitForSeconds(_duration);

        SlowAmmount -= _ammount;
    }
}
