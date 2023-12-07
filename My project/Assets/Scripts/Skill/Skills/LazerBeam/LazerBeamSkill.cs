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

    protected override IEnumerator HandleMulticast(int _num, Vector3 _position)
    {
        int _enemyNum = Mathf.Min(_num, GameManager.Instance.EnemyManager.GetEnemies().Count);
        List<Enemy> _closetEnemies = new();
        for (int i = 0; i < _enemyNum; i++)
        {
            Enemy _closetEnemy = null;
            float _minDistance = Mathf.Infinity;
            foreach (Enemy _enemy in GameManager.Instance.EnemyManager.GetEnemies())
            {
                if (_closetEnemies.Contains(_enemy)) continue;

                Vector3 _tempPos = _enemy.transform.position;
                float _tempDistance = Vector3.Distance(transform.position, _tempPos);
                if (_tempDistance < _minDistance)
                {
                    _closetEnemy = _enemy;
                    _minDistance = _tempDistance;
                }
            }
            _closetEnemies.Add(_closetEnemy);
        }

        for (int i = 0; i < _closetEnemies.Count; i++)
        {
            yield return new WaitForSeconds(m_multicastDelay);
            Vector3 _target = _closetEnemies[i].transform.position;
            if (_closetEnemies[i] == null || _closetEnemies[i].IsDied || _closetEnemies[i].transform.position == Vector3.zero) 
            {
                Vector2 _random = Random.insideUnitCircle;
                _target = _position;
                _target.x += _random.x * m_multicastOffset;
                _target.z += _random.y * m_multicastOffset;
            }
            Manager.CastSkillRaw(m_ID, _target);
        }

        for (int i = _closetEnemies.Count; i < _num; i++)
        {
            yield return new WaitForSeconds(m_multicastDelay);

            Vector2 _random = Random.insideUnitCircle;
            Vector3 _target = _position;
            _target.x += _random.x * m_multicastOffset;
            _target.z += _random.y * m_multicastOffset;

            yield return new WaitForSeconds(m_multicastDelay);
            Manager.CastSkillRaw(m_ID, _target);
        }
    }

    protected override IEnumerator Expiring()
    {
        yield return new WaitForSeconds(m_expireTime);

        Destroy(gameObject);
    }
}
