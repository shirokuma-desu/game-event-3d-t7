using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using LeakyAbstraction;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    protected Rigidbody m_body;

    [SerializeField]
    protected GameObject FloatingDamageText;

    [Header("Reference")]
    [SerializeField]
    protected EnemyVisual m_visual;
    [SerializeField]
    protected Collider m_collider;
    public EnemySpawner Spawner { get; set; }

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
    private int m_bounty;
    public int Bounty { get => m_bounty; }

    [SerializeField]
    private float m_attackRayLength;

    [Header("GameEvents")]
    [SerializeField]
    protected GameEvent m_anEnemyDie;
    [SerializeField]
    protected GameEvent m_anEnemyAttacking;

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

        if (!IsDied)
        {
            var go = Instantiate(FloatingDamageText, transform.position + Random.insideUnitSphere, Quaternion.identity, transform);
            go.GetComponent<TextMeshPro>().text = ((int)(_ammount * m_damageTakenModifyScale)).ToString();
        }
    }

    public void Kill()
    {
        StartCoroutine(Die());
    }

    protected virtual IEnumerator Die()
    {
        m_visual.StartDeadEffect();
        m_collider.enabled = false;

        IsDied = true;

        Spawner.Manager.SpawnDrop(transform.position, Bounty);

        SoundManager.Instance.PlaySound(GameSound.EnemyDie);

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

    public virtual void AttackBaseTower()
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
        SoundManager.Instance.PlaySound(GameSound.BeAttacked);
    }

    public virtual void AttackTurret(Turret _turret)
    {
        m_visual.StartAttackEffect();

        _turret.TakeDamage(m_attackDamage);
        
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
        SoundManager.Instance.PlaySound(GameSound.BeAttacked);
    }

    public virtual void Spawned()
    {
        m_visual.StartSpawnEffect();
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
        if (_duration >= .1f) _duration = .1f;
        EnemyDebuff _debuff = new EnemyDebuff(
            EnemyDebuff.DebuffType.Knockback, _ammount, _duration
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

        ResetProperties();
        
        SetupProperties();
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
            if (_hit.collider.tag == "BaseTower")
            {
                AttackBaseTower();
                break;
            }
            if (_hit.collider.tag == "Turret")
            {
                AttackTurret(_hit.collider.GetComponent<Turret>());
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
        m_collider.enabled = true;
        
        if (GameManager.Instance.GameTime > 6 * 60f) m_currentHealth = m_maxHealth + (GameManager.Instance.GameTime * 1f);
        else m_currentHealth = m_maxHealth;
        m_currentMoveSpeed = m_moveSpeed;

        IsSpawned = false;
        IsDied = false;

        m_currentDebuffs.Clear();
        m_speedModifyScale = 1f;
        m_damageTakenModifyScale = 1f;

        m_visual.Reset();

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
        if (EnvironmentManager.Instance.IsAnyTurretLeft) _tempDistance = Mathf.Infinity;
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
                        1f - _debuff.Ammount
                    );
                    IsSlowed = true;
                    break;

                case EnemyDebuff.DebuffType.Stun:
                    IsStun = true;
                    break;

                case EnemyDebuff.DebuffType.Vulnerable:
                    m_damageTakenModifyScale = Mathf.Max(m_damageTakenModifyScale,
                        1f + _debuff.Ammount
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
