using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    private Turret m_turret;
    private HandleShooting m_shooting;

    private int m_attackDamage;
    private float m_attackSpeed;
    private float m_attackRange;

    public int AttackDamage { get { return m_attackDamage; } }
    public float AttackSpeed { get {  return m_attackSpeed; } }
    public float AttackRange { get {  return m_attackRange; } }

    private void Awake()
    {
        m_shooting = GetComponent<HandleShooting>();
        m_turret = GetComponent<Turret>();
        m_attackDamage = m_turret.AttackDamage;
        m_attackSpeed = m_turret.AttackSpeed;
        m_attackRange = m_turret.AttackRange;
    }

    private void Update()
    {
        if (m_turret.IsSettled)
        {
            m_shooting.Shoot(m_attackDamage);
        }
    }
}
