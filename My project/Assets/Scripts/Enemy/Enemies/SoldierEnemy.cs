using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierEnemy : Enemy
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Move();
    }
}
