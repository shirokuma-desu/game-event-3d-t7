using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "HealthUpgrade", menuName = "Scriptable Objects/Turret Upgrade/Health", order = 1
)]
public class TurretHealthUpgrade : TurretUpgrade
{
    [Space(10)]
    [SerializeField]
    private float m_amount;

    public override void Apply(Turret _turret)
    {
        
    }
}
