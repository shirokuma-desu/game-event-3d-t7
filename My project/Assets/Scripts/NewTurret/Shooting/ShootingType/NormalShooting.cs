using UnityEngine;

public class NormalShooting : MonoBehaviour
{
    private TurretManager tm;
    private TurretShooting ts;

    [SerializeField]
    private GameObject bulletPrefab;

    private float m_lastShoot = 0f;

    private void Awake()
    {
        tm = GameObject.Find("TurretManager").GetComponent<TurretManager>();
        ts = GetComponentInParent<TurretShooting>();
    }

    private GameObject FindNearestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ts.AttackRange);
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
        if (Time.time - m_lastShoot > (1 / ts.AttackSpeed))
        {
            Vector3 bulletPosition = transform.position;
            bulletPosition.y = 1f;

            Bullet bulletScript = tm.StartSpawner(0, bulletPosition);

            if (bulletScript != null)
            {
                bulletScript.SetTarget(target, damage);
            }

            m_lastShoot = Time.time;
        }
    }

    public void Shoot(int damage)
    {
        Attack(FindNearestEnemy(), damage);
    }
}
