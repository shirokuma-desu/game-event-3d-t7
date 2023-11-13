using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStun", menuName = "Scriptable Objects/Enemy Effects/Debuff/Stun", order = 1)]
public class EnemyStunEffectSO : EnemyEffectSO
{
    public float Duration;

    public override void Apply(Enemy _target)
    {
        _target.TakeStunEffect(Duration);
    }
}