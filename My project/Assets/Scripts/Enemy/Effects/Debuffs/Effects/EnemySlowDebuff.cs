using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlowDebuff : EnemyDebuffEffect
{
    public float Ammount;
    public float Duration;

    public override void Apply(GameObject _target)
    {
        _target.GetComponent<Enemy>().TakeSlowDebuff(Ammount, Duration);
    }
}
