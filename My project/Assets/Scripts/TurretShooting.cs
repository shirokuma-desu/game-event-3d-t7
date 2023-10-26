using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public Turret turretStat;

    public GameObject bulletPrefab;

    private int attackDamage;
    private float attackSpeed;
    private float attackRange;
    private float currentCooldown = 0f;

    private void Awake()
    {
        turretStat = GetComponent<Turret>();
        attackDamage = turretStat.attackDamage;
        attackRange = turretStat.attackRange;
        attackSpeed = turretStat.attackSpeed;
    }

    private void Update()
    {
        if (turretStat.isSettle)
        {
            Attack(FindNearestEnemy(), attackDamage);
        }
    }

    GameObject FindNearestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
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

    void Attack(GameObject target, int damage)
    {
        if (currentCooldown <= 0f)
        {
            Vector3 bulletPosition = transform.position;
            bulletPosition.y = 1f;

            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
            NormalBulletScript bulletScript = bullet.GetComponent<NormalBulletScript>();

            if (bulletScript != null)
            {
                bulletScript.SetTarget(target, damage);
            }

            currentCooldown = attackSpeed;
        }
        else
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
