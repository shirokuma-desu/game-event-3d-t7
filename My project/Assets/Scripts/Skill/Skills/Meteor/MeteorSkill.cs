using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSkill : Skill
{
    protected override void Impact()
    {
        base.Impact();

        Collider[] colliders = Physics.OverlapSphere(CastPosition, m_range);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().TakeDamage(m_damage);
            }
        }
    
        Expire();
    }

    protected override IEnumerator Expiring()
    {
        yield return new WaitForSeconds(m_expireTime);

        Destroy(gameObject);
    }
}