using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinEnemy : Enemy
{
    private void FixedUpdate()
    {
        Move();
    }

    public override void TakeSlowEffect(float _ammount, float _duration) { }
    public override void TakeStunEffect(float _duration) { }
}
