using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestEnemy : Enemy
{
    [Header("Special Effect")]
    [SerializeField]
    private float m_healAmmount = 5f;
    [SerializeField]
    private float m_healRadius = 5f;
    [SerializeField]
    private float m_healingInterval = 0.5f;
    [SerializeField]
    private GameObject m_healEffect;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
        Move();
    }

    protected override void SetupProperties()
    {
        base.SetupProperties();

        InvokeRepeating(nameof(Healing), 0f, m_healingInterval);
    }

    private void Healing()
    {
        Collider[] _collisions = Physics.OverlapCapsule(transform.position + Vector3.down * 10f, transform.position + Vector3.up * 10f, m_healRadius);
        foreach(Collider _collision in _collisions)
        {
            if (_collision.tag == "Enemy")
            {
                Enemy _enemy = _collision.GetComponent<Enemy>();
                _enemy.TakeHealingEffect(m_healAmmount, m_healingInterval);
            }
        }

        GameObject _healObj = Instantiate(m_healEffect, transform.position, Quaternion.identity);
        _healObj.transform.localScale = new Vector3(.2f * m_healRadius, 1f, .2f * m_healRadius);
    }
}
