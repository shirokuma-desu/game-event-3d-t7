using UnityEngine;

public class SpreadShooting : MonoBehaviour
{
    private TurretManager tm;
    private Turret stat;

    private float m_lastShoot = 0f;

    private void Start()
    {
        tm = GameObject.Find("TurretManager").GetComponent<TurretManager>();
        stat = GetComponentInParent<Turret>();
    }

    private GameObject FindNearestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, stat.AttackRange);
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = collider.gameObject;
                }
            }
        }

        return nearestEnemy;
    }

    private void Attack(GameObject target, int damage)
    {
        if (Time.time - m_lastShoot > (1 / stat.AttackSpeed))
        {
            Vector3 bulletPosition = transform.position;
            bulletPosition.y = 1f;

            Bullet bulletScript = tm.StartSpawner(2, bulletPosition);

            if (bulletScript != null)
            {
                bulletScript.SetTarget(target, damage);
            }

            m_lastShoot = Time.time;
        }
    }

    public void Shoot()
    {
        GameObject nearestTarget = FindNearestEnemy();
        if (nearestTarget != null)
        {
            Attack(nearestTarget, stat.AttackDamage);
        }
    }
}
