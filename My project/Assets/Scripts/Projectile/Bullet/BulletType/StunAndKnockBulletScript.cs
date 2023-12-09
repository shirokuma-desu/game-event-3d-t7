using UnityEngine;

public class StunAndKnockBulletScript : Bullet
{
    private float m_sduration;

    private float m_kamount;
    private float m_kduration;

    public override void SetTarget(GameObject _target, int _damage, float _sduration, float _kamount, float _kduration)
    {
        m_target = _target;
        m_damage = _damage;
        m_sduration = _sduration;
        m_kamount = _kamount;
        m_kduration = _kduration;
    }

    protected override void HitTarget(GameObject _target)
    {
        Enemy enemy = m_target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(m_damage);
            enemy.TakeKnockbackEffect(m_kamount, m_kduration);
            enemy.TakeStunEffect(m_sduration);
        }

        Spawner.DespawnBullet(this);
    }
}
