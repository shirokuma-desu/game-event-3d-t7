using UnityEngine;

[CreateAssetMenu(fileName = "EnemySlow", menuName = "Scriptable Objects/Enemy Effects/Debuff/Slow", order = 1)]
public class EnemySlowEffectSO : EnemyEffectSO
{
    public float Ammount;
    public float Duration;

    public override void Apply(Enemy _target)
    {
        _target.TakeSlowEffect(Ammount, Duration);
    }
}
