using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [Header("Stats")]
    protected int m_damage;
    [SerializeField]
    protected float m_speed;

    [Header("GameEvents")]
    [SerializeField]
    private GameEvent m_aBulletSpawn;
    [SerializeField]
    private GameEvent m_aBulletDespawn;

    public BulletSpawner Spawner { get; set; }

    protected GameObject m_target;

    protected Vector3 basePos;

    // ------ PUBLIC ------
    public virtual void SetTarget(GameObject _target, int _damage)
    {
        m_target = _target;
        m_damage = _damage;

        basePos = transform.position;

        transform.forward = m_target.transform.position - transform.position;
    }

    public virtual void SetTarget(GameObject _target, int _damage, float _amount, float _duration)
    {

    }

    public virtual void SetTarget(GameObject _target, int _damage, float _duration)
    {

    }

    public virtual void SetTarget(GameObject _target, int _damage, float _sduration, float _kamount, float _kduration)
    {

    }

    // ------ PROTECTED ------
    protected virtual void Update()
    {
        if (m_target.activeSelf)
        {
            Move();
        }
        else
        {
            Spawner.DespawnBullet(this);
        }
    }

    protected virtual void Move()
    {
        Vector3 dir = m_target.transform.position - transform.position;
        dir.y = 0f;
        float distanceThisFrame = m_speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget(m_target);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    protected virtual void HitTarget(GameObject _target)
    {
        Enemy enemy = m_target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(m_damage);
        }

        Spawner.DespawnBullet(this);
    }

    protected virtual void SetupProperties()
    {

    }

    protected virtual void ResetProperties()
    {
        m_target = null;
        m_damage = 0;

        StopAllCoroutines();
    }   

    // ------ PRIVATE ------
    private void OnEnable()
    {
        SetupProperties();
    }

    private void OnDisable()
    {
        ResetProperties();
    }
}
