using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MiniSpreadBulletScript : Bullet
{
    private Vector3 dir;
    private Vector3 pos;

    private List<GameObject> hitEnemies = new List<GameObject>();

    public override void SetTarget(Vector3 _dir, int _damage, GameObject _ignoreTarget)
    {
        dir = _dir;
        m_damage = _damage;
        pos = transform.position;
        hitEnemies.Add(_ignoreTarget);
    }

    protected override void Update()
    {
        if ((transform.position - pos).magnitude > 15f)
        {
            Spawner.DespawnBullet(this);
        }

        CheckCollision();

        transform.Translate(dir * m_speed * Time.deltaTime);
    }

    private void CheckCollision()
    {
        Collider[] colliders = Physics.OverlapBox(new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(0.5f, 0.5f, 0.5f));

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy") && !hitEnemies.Contains(collider.gameObject))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(m_damage);
                    hitEnemies.Add(collider.gameObject);
                    m_damage /= 2;
                }
            }
        }
    }

    protected override void ResetProperties()
    {
        base.ResetProperties();
        hitEnemies.Clear();
    }
}
