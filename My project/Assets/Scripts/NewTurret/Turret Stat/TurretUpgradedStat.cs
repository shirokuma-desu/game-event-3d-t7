using UnityEngine;

public class TurretUpgradedStat : MonoBehaviour
{
    [SerializeField]
    private TurretStatSO ts;

    private int m_bonusHealth;
    private int m_bonusAttackDamage;
    private float m_bonusAttackSpeed;
    private float m_bonusAttackRange;

    public int BonusHealth { get { return m_bonusHealth; } set { m_bonusHealth = value; } }
    public int BonusAttackDamage { get { return m_bonusAttackDamage; } set { m_bonusAttackDamage = value; } }
    public float BonusAttackSpeed { get { return m_bonusAttackSpeed; } set { m_bonusAttackSpeed = value; } }
    public float BonusAttackRange { get { return m_bonusAttackRange; } set { m_bonusAttackRange = value; } }

    private void Start()
    {
        m_bonusHealth = ts.health;
        m_bonusAttackDamage = ts.attackDamage;
        m_bonusAttackSpeed += ts.attackSpeed;
        m_bonusAttackRange += ts.attackRange;
    }
}
