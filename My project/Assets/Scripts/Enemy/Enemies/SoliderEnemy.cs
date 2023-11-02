using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderEnemy : Enemy
{
    private void Start()
    {
        SetTarget();
    }

    private void FixedUpdate()
    {
        Move();
    }
}
