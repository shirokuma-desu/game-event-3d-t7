using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelEnemy : Enemy
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Move();
    }

    public override void AttackTurret(Turret _turret)
    {

    }

    protected override Vector3 GetTarget()
    {
        return EnvironmentManager.Instance.PlayerPosition;
    }
}
