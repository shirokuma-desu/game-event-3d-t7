using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSkill : Skill
{
    [Header("Lava")]
    [SerializeField]
    private float m_duration;
    public float Duration { get => m_duration; }

    [SerializeField]
    private float m_effectInterval;
    public float EffectInterval { get => m_effectInterval; }

    private bool m_endDuration = false;

    protected override void Impact()
    {
        StartCoroutine(DealEffect());
        StartCoroutine(CountDuration());
    }

    private IEnumerator DealEffect()
    {
        Collider[] colliders = Physics.OverlapSphere(CastPosition, m_range);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().TakeDamage(m_damage);
            }
        }

        yield return new WaitForSeconds(m_effectInterval);
    
        if (!m_endDuration) StartCoroutine(DealEffect());
        else Expire();
    }

    protected override IEnumerator Expiring()
    {
        yield return new WaitForSeconds(m_expireTime);

        Destroy(gameObject);
    }

    private IEnumerator CountDuration()
    {
        yield return new WaitForSeconds(m_duration);

        m_endDuration = true;
    }
}
