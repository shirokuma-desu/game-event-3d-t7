using UnityEngine;

public class SpreadBulletScript : Bullet
{
    [SerializeField]
    private int numberOfBullets;
    [SerializeField]
    private float bulletSpreadAngle = 30f;
    [SerializeField]
    private GameObject spreadPrefab;

    protected override void HitTarget(GameObject _target)
    {
        Enemy enemy = m_target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(m_damage);
            SpreadBullets();
        }

        Spawner.DespawnBullet(this);
    }

    //void SpreadBullets()
    //{
    //    // Get the direction from the current bullet to the target
    //    Vector3 directionToTarget = m_target.transform.position - transform.position;
    //    // Calculate the rotation angle based on the inverse direction
    //    float baseAngle = Mathf.Atan2(directionToTarget.z, directionToTarget.x) * Mathf.Rad2Deg;

    //    for (int i = 0; i < numberOfBullets; i++)
    //    {
    //        // Create a new bullet
    //        GameObject newBullet = Instantiate(spreadPrefab, transform.position, Quaternion.identity);
    //        MiniSpreadBulletScript miniScript = newBullet.GetComponent<MiniSpreadBulletScript>();

    //        if (miniScript != null)
    //        {
    //            miniScript.SetDamage(m_damage);
    //        }

    //        // Calculate the angle for the spread based on the base angle
    //        float angle = baseAngle + i * (bulletSpreadAngle / (numberOfBullets - 1)) - (bulletSpreadAngle / 2);
    //        newBullet.transform.rotation = Quaternion.Euler(0f, angle, 0f);
    //    }
    //}

    void SpreadBullets()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            GameObject newBullet = Instantiate(spreadPrefab, transform.position, Quaternion.identity);
            MiniSpreadBulletScript miniScript = newBullet.GetComponent<MiniSpreadBulletScript>();

            if (miniScript != null)
            {
                float angle = i * (bulletSpreadAngle / (numberOfBullets - 1)) - (bulletSpreadAngle / 2);
                Vector3 dir = m_target.transform.position - basePos;
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
                Vector3 dir2 = rotation * dir.normalized;
                miniScript.SetDamage(dir2, m_damage);
            }
        }
    }
}
