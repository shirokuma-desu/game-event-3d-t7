using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    private Turret m_turret;

    private NormalShooting ns;
    private SpreadShooting sp;

    [SerializeField, Tooltip("Shoot Type:\n0: Normal\n1: Chain\n2: Spread\n3: Stun\n4: Knockback")]
    private int m_shootType = 0;
    public int ShootType { get { return m_shootType; } set { m_shootType = value; } }

    private void Awake()
    {
        m_turret = GetComponent<Turret>();
        ns = GetComponentInChildren<NormalShooting>();
        sp = GetComponentInChildren<SpreadShooting>();
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
                Debug.Log("a");
                ns.Shoot(); 
                break;
            case 1:
                break;
            case 2:
                sp.Shoot();
                break;

        }
    }
}
