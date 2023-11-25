using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [Header("Stats")]
    private int m_damage;
    [SerializeField]
    protected float m_speed;

    [Header("GameEvents")]
    [SerializeField]
    private GameEvent m_aBulletSpawn;
    [SerializeField]
    private GameEvent m_aBulletDespawn;

    public BulletSpawner Spawner { get; set; }

    protected GameObject m_target;

    // ------ PUBLIC ------
    public virtual void SetTarget(GameObject _target, int _damage)
    {
        m_target = _target;
        m_damage = _damage;
    }

    // ------ PROTECTED ------
    protected virtual void Update()
    {
        if (m_target != null)
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
