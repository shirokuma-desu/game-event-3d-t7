using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChainBulletScript : Bullet
{
    private int bounceTimes;
    private int currentJump = 1;

    private GameObject[] ListEnemySetted = new GameObject[1000];
    int index = 0;

    public override void SetTarget(GameObject _target, int _damage, int _jumps)
    {
        base.SetTarget(_target, _damage);
        bounceTimes = _jumps;

        ListEnemySetted[index] = _target;
        index++;
    }

    protected override void Update()
    {
        base.Update();

        if (currentJump > bounceTimes)
        {
            Spawner.DespawnBullet(this);
        }
    }

    protected override void HitTarget(GameObject _target)
    {
        Enemy enemy = _target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(m_damage);
            ListEnemySetted[index] = _target;
            index++;
            JumpToNextTarget(_target);
        }
    }

    private void JumpToNextTarget(GameObject _target)
    {
        GameObject nextTarget = FindNearestEnemy(_target);
        //if (nextTarget != null)
        //{

            Debug.Log(nextTarget);
            m_target = nextTarget;
            currentJump++;
        //}
    }

    private GameObject FindNearestEnemy(GameObject _target)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f);
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy") && collider.gameObject.activeSelf && CheckSame(_target))
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

    private bool CheckSame(GameObject _target)
    {
        for (int i = 0; i < ListEnemySetted.Length; i++)
        {
            if (_target == ListEnemySetted[i])
            {
                return false;
            }
        }

        return true;
    }

    protected override void ResetProperties()
    {
        base.ResetProperties();

        currentJump = 1;
        bounceTimes = -1;
    }
}
