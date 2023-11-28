using UnityEngine;

public class SpreadBulletScript : Bullet
{
    private TurretManager tm;

    private int m_numberOfBullets;
    private float m_bulletSpreadAngle;

    private void Awake()
    {
        tm = GameObject.Find("TurretManager").GetComponent<TurretManager>();
    }

    public override void SetTarget(GameObject _target, int _damage, int _numberOfBullets, float _spreadAngle)
    {
        m_target = _target;
        m_damage = _damage;
        m_numberOfBullets = _numberOfBullets;
        m_bulletSpreadAngle = _spreadAngle;
    }

    protected override void HitTarget(GameObject _target)
    {
        Enemy enemy = m_target.GetComponent<Enemy>();
        if (enemy != null)
        {
            SpreadBullets(_target);
            enemy.TakeDamage(m_damage);
        }

        Spawner.DespawnBullet(this);
    }
    
    private void SpreadBullets(GameObject _target)
    {
        float angleStep = m_bulletSpreadAngle / (m_numberOfBullets - 1);
        float angle = -m_bulletSpreadAngle / 2f;

        Vector3 starterPoint = transform.position;
        Vector3 fpoint = transform.position - basePos;
        fpoint.y = 0f;
        fpoint += fpoint.normalized;

        for (int i = 0; i < m_numberOfBullets; i++)
        {
            Vector3 projectileMoveDir = Quaternion.Euler(0f, angle, 0f) * fpoint;

            Bullet newBullet = tm.StartSpawner(5, starterPoint);
            newBullet.SetTarget(projectileMoveDir.normalized, m_damage / 2, _target);

            angle += angleStep;
        }
    }
}
