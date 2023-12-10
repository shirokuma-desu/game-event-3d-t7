using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelEnemy : Enemy
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (GameManager.Instance.GameState != GameManager.State.GameOver)
        {
            Move();
        }
    }

    protected override Vector3 GetTarget()
    {
        return EnvironmentManager.Instance.PlayerPosition;
    }
}
