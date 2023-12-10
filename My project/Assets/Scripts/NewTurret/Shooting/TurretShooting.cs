using UnityEngine;
using LeakyAbstraction;

public class TurretShooting : MonoBehaviour
{
    private Turret m_turret;

    private NormalShooting ns;
    private StunShooting ss;
    private KnockbackShooting ks;
    private StunAndKnockShooting saks;

    private bool isStun;
    private bool isKnock;

    private void Awake()
    {
        m_turret = GetComponent<Turret>();
        ns = GetComponentInChildren<NormalShooting>();
        ss = GetComponentInChildren<StunShooting>();
        ks = GetComponentInChildren<KnockbackShooting>();
        saks = GetComponentInChildren<StunAndKnockShooting>();
    }

    private void Update()
    {
        isStun = GameObject.Find("TurretManager").GetComponent<TurretManager>().IsStunEnabled;
        isKnock = GameObject.Find("TurretManager").GetComponent<TurretManager>().IsKnockbackEnabled;

        if (m_turret.IsSettled)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (isKnock && isStun)
        {
            saks.Shoot();
        }

        if (isStun && !isKnock)
        {
            ss.Shoot();
        }

        if (!isStun && isKnock)
        {
            ks.Shoot();
        }

        if (!isStun && !isKnock)
        {
            ns.Shoot();
        }        
    }
}
