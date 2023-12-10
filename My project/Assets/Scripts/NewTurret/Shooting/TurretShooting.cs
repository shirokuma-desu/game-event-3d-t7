using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    private Turret m_turret;

    private NormalShooting ns;
    private StunShooting ss;
    private KnockbackShooting ks;


    [SerializeField, Tooltip("Shoot Type:\n0: Normal\n1: Stun\n2: Knockback")]
    private int m_shootType = 0;
    public int ShootType { get { return m_shootType; } set { m_shootType = value; } }

    private void Awake()
    {
        m_turret = GetComponent<Turret>();
        ns = GetComponentInChildren<NormalShooting>();
        ss = GetComponentInChildren<StunShooting>();
        ks = GetComponentInChildren<KnockbackShooting>();
    }

    private void Update()
    {
        if (m_turret.IsSettled)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        switch (m_shootType)
        {
            case 0:
                ns.Shoot(); 
                break;
            case 1:
                ss.Shoot();
                break;
            case 2:
                ks.Shoot();
                break;

        }
    }
}
