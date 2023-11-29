using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherEnemy : Enemy
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Move();
    }

    protected override IEnumerator Die()
    {
        m_visual.StartDeadEffect();

        IsDied = true;

        yield return new WaitUntil(() => m_visual.ReadyToDie);

        Spawner.Manager.SpawnEnemyForm("MotherDeadFormation", transform.position);

        if (Spawner != null)
        {
            Spawner.DespawnEnemy(this);
        }
        else
        {
            Destroy(gameObject);
        }

        m_anEnemyDie.RaiseEvent();
    }
}
