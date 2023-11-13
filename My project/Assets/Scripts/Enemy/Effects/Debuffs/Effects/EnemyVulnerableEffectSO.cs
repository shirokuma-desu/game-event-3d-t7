using UnityEngine;

[CreateAssetMenu(fileName = "EnemyVulnerable", menuName = "Scriptable Objects/Enemy Effects/Debuff/Vulnerable", order = 1)]
public class EnemyVulnerableEffectSO : EnemyEffectSO
{
    public float Ammount;
    public float Duration;

    public override void Apply(Enemy _target)
    {
        _target.TakeVulnerableEffect(Ammount, Duration);
    }
}
