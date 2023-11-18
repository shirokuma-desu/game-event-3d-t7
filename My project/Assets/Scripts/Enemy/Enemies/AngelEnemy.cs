using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelEnemy : Enemy
{
    private void FixedUpdate()
    {
        Move();
    }

    protected override Vector3 GetTarget()
    {
        return EnvironmentManager.Instance.PlayerPosition;
    }
}