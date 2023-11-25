using System.Runtime.CompilerServices;
using UnityEngine;

public class NormalBulletScript : Bullet
{
    protected override void Move()
    {
        Vector3 dir = m_target.transform.position - transform.position;
        dir.y = 0f;
        float distanceThisFrame = m_speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget(m_target);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
}
