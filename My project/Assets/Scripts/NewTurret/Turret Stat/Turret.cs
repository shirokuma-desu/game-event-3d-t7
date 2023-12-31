using Unity.VisualScripting;
using UnityEngine;
using LeakyAbstraction;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private TurretStatSO stat;

    private TurretManager tm;
    private TurretUpgradedStat tus;

    private int m_health;
    private int m_attackDamage;
    private float m_attackSpeed;
    private float m_attackRange;

    private float m_stunDuration;
    private float m_knockbackAmount;
    private float m_knockbackDuration;

    [SerializeField]
    private bool m_isSettled = false;
    private bool m_isDead = false;
    private bool m_isUpgraded = false;
    private bool m_canBeRestored = true;

    private int m_spotIndex;
    private Vector3 m_lockPos;

    [SerializeField]
    private int m_currentHealth;

    public int Health { get { return m_health; } set {  m_health = value; } }
    public int CurrentHealth { get { return m_currentHealth; } set { m_currentHealth = value; } }
    public int AttackDamage { get { return m_attackDamage; } set { m_attackDamage = value; } }
    public float AttackSpeed { get { return m_attackSpeed; } set { m_attackSpeed = value; } }
    public float AttackRange { get { return m_attackRange; } set { m_attackRange = value; } }
    public float StunDuration { get { return m_stunDuration; } set { m_stunDuration = value; } }
    public float KnockbackAmount { get { return m_knockbackAmount; } set { m_knockbackAmount = value; } }
    public float KnockbackDuration { get { return m_knockbackDuration; } set { m_knockbackDuration = value; } }
    public bool IsSettled { get { return m_isSettled; } set { m_isSettled = value; } }
    public bool IsDead { get { return m_isDead; } set { m_isDead = value; } }
    public bool IsUpgraded { get {  return m_isUpgraded; } set { m_isUpgraded = value; } }
    public bool CanBeRestore { get { return m_canBeRestored; } set { m_canBeRestored = value; } }
    public int SpotIndex { get { return m_spotIndex; } set { m_spotIndex = value; } }
    public Vector3 LockPos { get { return m_lockPos; } set { m_lockPos = value; } }

    private void Awake()
    {
        tm = GameObject.Find("TurretManager").GetComponent<TurretManager>();
        tus = GameObject.Find("TurretManager").GetComponent<TurretUpgradedStat>();

        m_health = stat.health;
        m_attackDamage = stat.attackDamage;
        m_attackSpeed = stat.attackSpeed;
        m_attackRange = stat.attackRange;

        m_stunDuration = stat.stunDuration;
        m_knockbackAmount = stat.knockbackAmount;
        m_knockbackDuration = stat.knockbackDuration;

        m_currentHealth = m_health;
    }

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
        tus.SetUp();
        UpdateStat();
    }

    private void Update()
    {
        m_isDead = m_currentHealth <= 0;
        if (m_isDead)
        {
            Die();
        }

        if (m_lockPos != Vector3.zero && m_isSettled)
        {
            transform.position = m_lockPos;
        }
    }

    public void UpdateStat()
    {
        m_currentHealth = tus.BonusHealth * (m_currentHealth / m_health);
        
        m_health = tus.BonusHealth;
        m_attackDamage = tus.BonusAttackDamage;
        m_attackSpeed = tus.BonusAttackSpeed;
        m_attackRange = tus.BonusAttackRange;
        m_stunDuration = tus.BonusStunDuration;
        m_knockbackAmount = tus.BonusKnockbackAmout;
        m_knockbackDuration = tus.BonusKnockbackDuration;
    }

    public void TakeDamage(int damage)
    {
        if (m_isSettled)
        {
            m_currentHealth -= damage;

            if (m_currentHealth < 0)
            {
                m_currentHealth = 0;
            }

            if (m_currentHealth > m_health)
            {
                m_currentHealth = m_health;
            }
        }
    }

    private void Die()
    {
        GameManager.Instance.TurretManager.DeleteOccupied(m_spotIndex);
        GameManager.Instance.TurretManager.TurretSpots[m_spotIndex].GetComponent<TurretSpot>().IsSettled = false;

        tm.ATurretDestroyed.RaiseEvent();
        SoundManager.Instance.PlaySound(GameSound.TurretPlace);

        Destroy(gameObject);
    }
}
