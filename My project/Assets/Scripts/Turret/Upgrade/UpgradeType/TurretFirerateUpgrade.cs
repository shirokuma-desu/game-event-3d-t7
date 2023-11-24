using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "FirerateUpgrade", menuName = "Scriptable Objects/Turret Upgrade/Firerate", order = 1
)]
public class TurretFirerateUpgrade : TurretUpgrade
{
    [Space(10)]
    [SerializeField]
    private float m_amount;

    public override void Apply(Turret _turret)
    {
        
    }
}
