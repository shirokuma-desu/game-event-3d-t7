using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRainSkill : Skill
{
    [Header("Acid Rain")]
    [SerializeField]
    private float m_duration;
    public float Duration { get => m_duration; }

    [SerializeField]
    private float m_effectInterval;
    public float EffectInterval { get => m_effectInterval; }

    [SerializeField]
    private float m_slowAmmout;
    [SerializeField]
    private float m_vulnerableAmmout;

    private bool m_endDuration = false;

    protected override void Impact()
    {
        StartCoroutine(DealEffect());
        StartCoroutine(CountDuration());
    }

    private IEnumerator DealEffect()
    {
        Collider[] colliders = Physics.OverlapSphere(CastPosition, m_range / 2);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().TakeDamage(m_damage);
                collider.gameObject.GetComponent<Enemy>().TakeSlowEffect(m_slowAmmout, m_effectInterval);
                collider.gameObject.GetComponent<Enemy>().TakeVulnerableEffect(m_vulnerableAmmout, m_effectInterval);
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