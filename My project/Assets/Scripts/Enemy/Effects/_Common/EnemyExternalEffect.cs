using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyExternalEffect : ScriptableObject
{
    public abstract void Apply(Enemy _target);
}
