using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyDebuffEffect : ScriptableObject
{
    public abstract void Apply(GameObject _target);
}
