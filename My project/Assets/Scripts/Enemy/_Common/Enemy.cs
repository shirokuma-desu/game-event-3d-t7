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
    private const float SPEEDSCALE = 0.0005f;

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
    public bool IsSpawned { get; private set; }
    public bool IsDied { get; private set; }

    public bool IsSlowed { get; private set; }
    public bool IsStun { get; private set; }
    public bool IsVulnerable { get; private set; }
    public bool IsHealing { get; private set; }

    private List<EnemyDebuff> m_currentDebuffs = new List<EnemyDebuff>();
    private float m_speedModifyScale;
    private float m_damageTakenModifyScale;

    protected Vector3 m_target;

    // ------ PUBLIC ------
    public virtual void TakeDamage(float _ammount)
    {
        m_currentHealth -= _ammount * m_damageTakenModifyScale;

        if (m_currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        IsDied = true;

        if (Spawner != null)
        {
            Spawner.DespawnEnemy(this);
        }
        else
        {
            Destroy(gameObject);
        }

        m_anEnemyDie.RaiseEvent();
    }

    public virtual void Attack()
    {
        EnvironmentManager.Instance.Player.GetComponent<BaseTurret>().TakeDamage(1);

        m_anEnemyAttacking.RaiseEvent();
    }

    public virtual void SetTarget()
    {
        m_target = GetTarget();
    }

    public virtual void Heal(float _ammount)
    {
        m_currentHealth += _ammount;
        m_currentHealth = Mathf.Min(m_currentHealth, m_maxHealth);
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

    public virtual void TakeHealingEffect(float _ammount, float _duration)
    {
        EnemyDebuff _debuff = new EnemyDebuff(
            EnemyDebuff.DebuffType.Healing, _ammount, _duration
        );

        if (!IsHealing)
        {
            m_currentDebuffs.Add(_debuff);
            StartCoroutine(ExpireDebuff(_debuff, _duration));

            SetDebuffStatus();
        }
    }

    // ------ PROTECTED ------
    protected virtual void Start()
    {
        m_body = GetComponent<Rigidbody>();
    }
    protected virtual void FixedUpdate()
    {
        m_body.velocity = Vector3.zero;
    }
    protected virtual void SetupProperties()
    {
        IsSpawned = true;

        m_currentHealth = m_maxHealth;
        m_currentMoveSpeed = m_moveSpeed;

        SetDebuffStatus();

        SetTarget();
    }

    protected virtual void ResetProperties()
    {
        m_currentHealth = m_maxHealth;
        m_currentMoveSpeed = m_moveSpeed;

        IsSpawned = false;
        IsDied = false; 

        m_currentDebuffs.Clear();
        m_speedModifyScale = 1f;
        m_damageTakenModifyScale = 1f;

        StopAllCoroutines();
    }

    protected virtual void Move()
    {
        if (!IsStun)
        {
            Vector3 _direction = (m_target - transform.position).normalized;
            _direction.y = 0;

            transform.forward = _direction;

            m_body.MovePosition(
                transform.position + _direction * m_moveSpeed * m_speedModifyScale * SPEEDSCALE
            );
        }
    }

    protected virtual Vector3 GetTarget()
    {
        Vector3 _target = EnvironmentManager.Instance.PlayerPosition;

        float _tempDistance = Vector3.Distance(transform.position, _target);
        for (int i = 0; i < 4; i++)
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

        return _target;
    }

    protected void SetDebuffStatus()
    {
        IsSlowed = false;
        IsStun = false;
        IsVulnerable = false;
        IsHealing = false;

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

                case EnemyDebuff.DebuffType.Healing:
                    Heal(_debuff.Ammount);
                    IsHealing = true;
                    break;

                default:
                    break;
            }
        }
    }

    // ------ PRIVATE ------
    private IEnumerator ExpireDebuff(EnemyDebuff _debuff, float _duration)
    {
        yield return new WaitForSeconds(_duration);

        m_currentDebuffs.Remove(_debuff);

        SetDebuffStatus();
    }

    private void OnEnable()
    {
        SetupProperties();
    }

    private void OnDisable()
    {
        ResetProperties();
    }
}
