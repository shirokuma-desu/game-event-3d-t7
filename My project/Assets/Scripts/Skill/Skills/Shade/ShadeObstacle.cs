using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShadeObstacle : SkillObstacle
{
    [SerializeField]
    private float m_activeRange;
    public float ActiveRange { get => m_activeRange; }
    private float m_impactRange;
    public float ImpactRange { get => m_impactRange; }
    private float m_chaseRange;
    public float ChaseRange { get => m_chaseRange; }
    private float m_chaseSpeed;
    public float ChaseSpeed { get => m_chaseSpeed; }

    private Enemy m_target;

    public void SetupProperties(int _id, float _damage, float _impactRange, float _chaseRange, float _chaseSpeed)
    {
        m_skillID = _id;
        m_damage = _damage;
        m_impactRange = _impactRange;
        m_chaseRange = _chaseRange;
        m_chaseSpeed = _chaseSpeed;
    }

    private void Update()
    {
        if (IsAbleToChase())
        {
            if (m_target == null)
            {
                SetTarget();
            }
            else 
            {
                if (m_target.IsDied)
                {
                    SetTarget();
                }

                ChaseEnemy();
            }
        }
    }

    private void SetTarget()
    {
        float _tempDistance = Mathf.Infinity;

        Collider[] _colliders = Physics.OverlapSphere(transform.position, m_chaseRange);
        foreach (Collider collider in _colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                if (!collider.GetComponent<Enemy>().IsDied && Vector3.Distance(transform.position, collider.transform.position) < _tempDistance)
                {
                    _tempDistance = Vector3.Distance(transform.position, collider.transform.position);
                    m_target = collider.GetComponent<Enemy>();
                }
            }
        }
    }

    private void ChaseEnemy()
    {
        transform.forward = (m_target.transform.position - transform.position).normalized;

        transform.position += transform.forward * m_chaseSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, m_target.transform.position) <= m_activeRange) 
        {
            Explode();
        }
    }

    private bool IsAbleToChase()
    {
        Collider[] _colliders = Physics.OverlapSphere(transform.position, m_chaseRange);
        foreach (Collider collider in _colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                return true;
            }
        }

        return false;
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
