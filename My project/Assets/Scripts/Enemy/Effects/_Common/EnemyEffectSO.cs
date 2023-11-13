using UnityEngine;

public abstract class EnemyEffectSO : ScriptableObject
{
    public abstract void Apply(Enemy _target);
}
