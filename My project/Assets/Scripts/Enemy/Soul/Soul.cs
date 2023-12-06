using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public SoulSpawner Spawner { get; set; }

    [SerializeField]
    private TrailRenderer m_trail;

    private int m_bounty;

    [SerializeField]
    private float m_acceleration;
    private float m_speed;

    private Vector3 m_direction;

    private bool m_isCollected;

    [Header("Game Events")]
    [SerializeField]
    private GameEvent m_collectSoul;

    public void SetTarget()
    {
        m_trail.Clear();
        m_direction = (EnvironmentManager.Instance.PlayerPosition - transform.position).normalized;
    }

    public void SetBounty(int _amount)
    {
        m_bounty = _amount;
    }

    private void SetupProperties()
    {
        SetTarget();
    }

    private void ResetPreperties()
    {
        m_isCollected = false;
        m_speed = 0f;
    }

    private void Move()
    {
        m_speed += m_acceleration * Time.deltaTime;
        transform.Translate(m_direction * m_speed * Time.deltaTime);
    }

    private void Collected()
    {
        m_isCollected = true;

        m_collectSoul.RaiseEvent();

        GameManager.Instance.Shop.Gain(m_bounty);

        StartCoroutine(Despawn());
    }
    private IEnumerator Despawn()
    {
        yield return new WaitForSeconds(.6f);

        if (Spawner == null)
        {
            Destroy(gameObject);
        }
        else 
        {
            Spawner.DespawnSoul(this);
        }
    }

    private void Start()
    {
        ResetPreperties();
        SetupProperties();
    }

    private void FixedUpdate()
    {
        if (!m_isCollected)
        {
            Move();

            if (Vector3.Distance(transform.position, EnvironmentManager.Instance.PlayerPosition) <= .5f)
            {
                transform.position = EnvironmentManager.Instance.PlayerPosition;

                Collected();
            }
        }
        
    }

    private void OnEnable()
    {
        SetupProperties();
    }

    private void OnDisable()
    {
        ResetPreperties();
    }
}
