using UnityEngine;

public class TurretUpgradedStat : MonoBehaviour
{
    [SerializeField]
    private TurretStatSO ts;

    private int m_bonusHealth = 0;
    private int m_bonusAttackDamage = 0;
    private float m_bonusAttackSpeed = 0;
    private float m_bonusAttackRange = 0;
    private float m_bonusStunDuration = 0;
    private float m_bonusKnockbackDuration = 0;
    private float m_bonusKnockbackAmount = 0;

    public int BonusHealth { get { return m_bonusHealth; } set { m_bonusHealth = value; } }
    public int BonusAttackDamage { get { return m_bonusAttackDamage; } set { m_bonusAttackDamage = value; } }
    public float BonusAttackSpeed { get { return m_bonusAttackSpeed; } set { m_bonusAttackSpeed = value; } }
    public float BonusAttackRange { get { return m_bonusAttackRange; } set { m_bonusAttackRange = value; } }
    public float BonusStunDuration {  get { return m_bonusStunDuration;} set { m_bonusStunDuration = value; } }

    public float BonusKnockbackDuration { get { return m_bonusKnockbackDuration; } set { m_bonusKnockbackDuration = value; } }

    public float BonusKnockbackAmout { get { return m_bonusKnockbackAmount; } set { m_bonusKnockbackAmount = value; } }

    private void Start()
    {
        m_bonusHealth += ts.health;
        m_bonusAttackDamage = ts.attackDamage;
        m_bonusAttackSpeed += ts.attackSpeed;
        m_bonusAttackRange += ts.attackRange;
        m_bonusStunDuration += ts.stunDuration;
        m_bonusKnockbackAmount += ts.knockbackAmount;
        m_bonusKnockbackDuration += ts.knockbackDuration;
    }
}
