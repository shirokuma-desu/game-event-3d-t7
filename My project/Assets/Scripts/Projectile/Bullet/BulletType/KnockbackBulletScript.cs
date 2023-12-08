using UnityEngine;

public class KnockbackBulletScript : Bullet
{
    private float m_amount;
    private float m_duration;

    public override void SetTarget(GameObject _target, int _damage, float _amount, float _duration)
    {
        m_target = _target;
        m_damage = _damage;
        m_amount = _amount;
        m_duration = _duration;
    }

    protected override void HitTarget(GameObject _target)
    {
        Enemy enemy = m_target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(m_damage);
            enemy.TakeKnockbackEffect(m_amount, m_duration);
        }

        Spawner.DespawnBullet(this);
    }
}
