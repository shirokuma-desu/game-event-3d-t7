using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherEnemy : Enemy
{
    [SerializeField]
    private GameObject m_motherDeadParticle;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Move();
    }

    protected override IEnumerator Die()
    {
        m_visual.StartDeadEffect();
    
        IsDied = true;

        Spawner.Manager.SpawnDrop(transform.position, Bounty);

        yield return new WaitUntil(() => m_visual.ReadyToDie);

        EnvironmentManager.Instance.SpawnParticle(m_motherDeadParticle, transform.position);

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
