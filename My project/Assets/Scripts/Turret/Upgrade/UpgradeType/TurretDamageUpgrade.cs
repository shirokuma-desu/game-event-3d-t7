using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "DamageUpgrade", menuName = "Scriptable Objects/Turret Upgrade/Damage", order = 1
)]
public class TurretDamagehUpgrade : TurretUpgrade
{
    [Space(10)]
    [SerializeField]
    private float m_amount;

    public override void Apply(Turret _turret)
    {
        
    }
}
