using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherEnemy : Enemy
{
    private void FixedUpdate()
    {
        Move();
    }

    protected override void Die()
    {
        base.Die();

        Spawner.Manager.SpawnEnemyForm("MotherDeadFormation");
    }
}
