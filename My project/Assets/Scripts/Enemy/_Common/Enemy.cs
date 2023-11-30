using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    protected Rigidbody m_body;

    [Header("Reference")]
    [SerializeField]
    protected EnemyVisual m_visual;

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
    private int m_attackDamage;
    public int AttackDamage { get => m_attackDamage; }

    [SerializeField]
    private float m_bounty;
    public float Bounty { get => m_bounty; }

    [SerializeField]
    private float m_attackRayLength;

    [Header("GameEvents")]
    [SerializeField]
    protected GameEvent m_anEnemyDie;
    [SerializeField]
    protected GameEvent m_anEnemyAttacking;

    public EnemySpawner Spawner { get; set; }

    //
    public bool IsSpawned { get; protected set; }
    public bool IsDied { get; protected set; }

    public bool IsSlowed { get; protected set; }
    public bool IsStun { get; protected set; }
    public bool IsVulnerable { get; protected set; }
    public bool IsHealing { get; protected set; }
    public bool IsKnockback { get; protected set; }

    private List<EnemyDebuff> m_currentDebuffs = new List<EnemyDebuff>();
    private float m_speedModifyScale;
    private float m_damageTakenModifyScale;
    private float m_knockbackSpeedScale;

    protected Vector3 m_target;

    // ------ PUBLIC ------
    public virtual void TakeDamage(float _ammount)
    {
        m_currentHealth -= _ammount * m_damageTakenModifyScale;

        if (m_currentHealth <= 0)
        {
            if (!IsDied)
            {
                StartCoroutine(Die());
            }
        }
        else 
        {
            m_visual.StartBeHitEffect();
        }
    }

    protected virtual IEnumerator Die()
    {
        m_visual.StartDeadEffect();

        IsDied = true;

        yield return new WaitUntil(() => m_visual.ReadyToDie);

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
        m_visual.StartAttackEffect();

        EnvironmentManager.Instance.Player.GetComponent<BaseTurret>().TakeDamage(m_attackDamage);
        
        IsDied = true;

        if (Spawner != null)
        {
            Spawner.DespawnEnemy(this);
        }
        else
        {
            Destroy(gameObject);
        }

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

    public virtual void TakeKnockbackEffect(float _ammount, float _duration)
    {
        EnemyDebuff _debuff = new EnemyDebuff(
            EnemyDebuff.DebuffType.Healing, _ammount, _duration
        );

        if (!IsKnockback)
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

        m_currentHealth = m_maxHealth;
        m_currentMoveSpeed = m_moveSpeed;
    }

    protected virtual void FixedUpdate()
    {
        m_body.velocity = Vector3.zero;

        if (IsKnockback)
        {
            Vector3 _direction = -(m_target - transform.position).normalized;
            _direction.y = 0;

            m_body.MovePosition(
                transform.position + _direction * m_knockbackSpeedScale * SPEEDSCALE
            );
        }
    }

    protected virtual void Update()
    {
        RaycastHit[] _hits = Physics.RaycastAll(transform.position, transform.forward, m_attackRayLength);
        foreach (RaycastHit _hit in _hits)
        {
            if (_hit.collider.tag == "BaseTower" || _hit.collider.tag == "Turret")
            {
                Attack();

                break;
            }
        }
    }

    protected virtual void SetupProperties()
    {
        IsSpawned = true;

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
        if (!IsStun && !IsDied)
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
        IsKnockback = false;

        m_speedModifyScale = 1f;
        m_damageTakenModifyScale = 1f;
        m_knockbackSpeedScale = 1f;

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

                case EnemyDebuff.DebuffType.Knockback:
                    m_knockbackSpeedScale = _debuff.Ammount;
                    IsStun = true;
                    IsKnockback = true;
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
