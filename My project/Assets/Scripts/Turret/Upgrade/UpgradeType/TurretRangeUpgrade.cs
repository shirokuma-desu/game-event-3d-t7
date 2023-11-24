using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "RangeUpgrade", menuName = "Scriptable Objects/Turret Upgrade/Range", order = 1
)]
public class TurretRangeUpgrade : TurretUpgrade
{
    [Space(10)]
    [SerializeField]
    private float m_amount;

    public override void Apply(Turret _turret)
    {
        
    }
}
