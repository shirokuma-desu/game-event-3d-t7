using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBeamSkill : Skill
{
    [Header("Lazer Beam")]
    [SerializeField]
    private float m_radius;
    public float Radius { get => m_radius; }

    protected override void Impact()
    {
        base.Impact();

        Vector3 _rayOrigin = EnvironmentManager.Instance.PlayerPosition;
        Vector3 _direction = (CastPosition - _rayOrigin).normalized;
        RaycastHit[] _hits;

        _hits = Physics.RaycastAll(_rayOrigin, _direction, m_range);
        foreach (RaycastHit _hit in _hits)
        {
            Collider collider = _hit.collider;
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().TakeDamage(m_damage);
            }
        }
        _hits = Physics.RaycastAll(_rayOrigin + new Vector3(_direction.z, _direction.y, -_direction.x) * m_radius, _direction, m_range);
        foreach (RaycastHit _hit in _hits)
        {
            Collider collider = _hit.collider;
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().TakeDamage(m_damage);
            }
        }
        _hits = Physics.RaycastAll(_rayOrigin - new Vector3(_direction.z, _direction.y, -_direction.x) * m_radius, _direction, m_range);
        foreach (RaycastHit _hit in _hits)
        {
            Collider collider = _hit.collider;
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
