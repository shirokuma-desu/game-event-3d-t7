using UnityEngine;

public class HandleShooting : MonoBehaviour
{
    private NormalShooting ns;

    [SerializeField, Tooltip("Shoot Type:\n0: Normal\n1: Chain\n2: Spread\n3: Stun\n4: Knockback")]
    private int m_shootType = 0;

    public int ShootType { get { return m_shootType; } set { m_shootType = value; } }

    private void Awake()
    {
        ns = GetComponentInChildren<NormalShooting>();
    }

    public void Shoot(int damage)
    {
        switch (m_shootType)
        {
            case 0:
                ns.Shoot(damage); break;
        }
    }
}
