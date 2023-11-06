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

    public bool IsSlowed { get; private set; }
    public bool IsStun { get; private set; }
    public bool IsVulnerable { get; private set; }

    private List<EnemyDebuff> m_currentDebuffs = new List<EnemyDebuff>();
    private float m_speedModifyScale;
    private float m_damageTakenModifyScale;

    protected Vector3 m_target;

    //
    public virtual void TakeDamage(float _ammount)
    {
        m_currentHealth -= _ammount * m_damageTakenModifyScale;
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

    public virtual void TakeSlowEffect(float _ammount, float _duration)
    {
        EnemyDebuff _debuff = new EnemyDebuff(
            EnemyDebuff.DebuffType.Slow, _ammount, _duration
        );

        m_currentDebuffs.Add(_debuff);
        StartCoroutine(ExpireDebuff(_debuff, _duration));

        SetDebuffStatus();
    }

    public virtual void TakeStunEffect(float _duration)
    {
        EnemyDebuff _debuff = new EnemyDebuff(
            EnemyDebuff.DebuffType.Stun, 0f, _duration
        );

        m_currentDebuffs.Add(_debuff);
        StartCoroutine(ExpireDebuff(_debuff, _duration));

        SetDebuffStatus();
    }

    public virtual void TakeVulnerableEffect(float _ammount, float _duration)
    {
        EnemyDebuff _debuff = new EnemyDebuff(
            EnemyDebuff.DebuffType.Vulnerable, _ammount, _duration
        );

        m_currentDebuffs.Add(_debuff);
        StartCoroutine(ExpireDebuff(_debuff, _duration));

        SetDebuffStatus();
    }

    //
    protected virtual void Start()
    {
        m_body = GetComponent<Rigidbody>();

        m_currentHealth = m_maxHealth;
        m_currentMoveSpeed = m_moveSpeed;

        SetDebuffStatus();

        IsDied = false;

        SetTarget();
    }

    protected virtual void Move()
    {
        if (!IsStun)
        {
            Vector3 _direction = (m_target - transform.position).normalized;
            _direction.y = 0;

            transform.forward = _direction;

            m_body.velocity = _direction * m_moveSpeed * m_speedScale * m_speedModifyScale;
        }
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

    protected void SetDebuffStatus()
    {
        IsSlowed = false;
        IsStun = false;
        IsVulnerable = false; 
        m_speedModifyScale = 1f;
        m_damageTakenModifyScale = 1f;

        foreach (EnemyDebuff _debuff in m_currentDebuffs)
        {
            switch (_debuff.Type)
            {
                case EnemyDebuff.DebuffType.Slow:
                    m_speedModifyScale = Mathf.Min(m_speedModifyScale, 
                        (100f - _debuff.Ammount) / 100f
                    );
                    IsSlowed = true;
                    break;

                case EnemyDebuff.DebuffType.Stun:
                    IsStun = true;
                    break;

                case EnemyDebuff.DebuffType.Vulnerable:
                    m_damageTakenModifyScale = Mathf.Max(m_damageTakenModifyScale,
                        (100f + _debuff.Ammount) / 100f
                    );
                    IsVulnerable = true;
                    break;

                default:
                    break;
            }
        }
    }

    //
    private IEnumerator ExpireDebuff(EnemyDebuff _debuff, float _duration)
    {
        yield return new WaitForSeconds(_duration);

        m_currentDebuffs.Remove(_debuff);

        SetDebuffStatus();
    }
}
