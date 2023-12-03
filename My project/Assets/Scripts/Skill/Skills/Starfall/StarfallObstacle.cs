using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfallObstacle : SkillObstacle
{
    [SerializeField]
    private float m_activeRange;
    public float ActiveRange { get => m_activeRange; }
    private float m_impactRange;
    public float ImpactRange { get => m_impactRange; }

    public void SetupProperties(int _id, float _damage, float _impactRange)
    {
        m_skillID = _id;
        m_damage = _damage;
        m_impactRange = _impactRange;
    }

    private void Update()
    {
        Collider[] _colliders = Physics.OverlapSphere(transform.position, m_activeRange);

        foreach (Collider collider in _colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Explode();

                break;
            }
        }
    }

    private void Explode()
    {
        Collider[] _colliders = Physics.OverlapSphere(transform.position, m_impactRange);

        foreach (Collider collider in _colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().TakeDamage(m_damage);
            }
        }

        Expire();
    }
}
