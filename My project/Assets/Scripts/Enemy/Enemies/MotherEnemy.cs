using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherEnemy : Enemy
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Move();
    }

    protected override void Die()
    {
        Spawner.Manager.SpawnEnemyForm("MotherDeadFormation", transform.position);

        base.Die();
    }
}
