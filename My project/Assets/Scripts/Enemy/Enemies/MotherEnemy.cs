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
        Spawner.Manager.SpawnEnemyForm("MotherDeadFormation");

        base.Die();
    }
}
