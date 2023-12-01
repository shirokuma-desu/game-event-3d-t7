using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField]
    private GameObject m_model;
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private AnimationClip[] m_aniClips;

    [Header("Spawn Effect")]
    [SerializeField]
    private GameObject m_spawnParticle;

    [Header("BeHit Effect")]
    [SerializeField]
    private Color m_beHitColor;
    [SerializeField]
    private float m_beHitKnockback;
    [SerializeField]
    private float m_beHitTime;

    [Header("Dead Effect")]
    [SerializeField]
    private GameObject m_deadParticle;
    [SerializeField]
    private float m_deadTime;
    public bool ReadyToDie { get; private set; }

    [Header("Attack Effect")]
    [SerializeField]
    private GameObject m_attackParticle;

    public void StartSpawnEffect()
    {
        EnvironmentManager.Instance.SpawnParticle(m_spawnParticle, transform.position);
    }

    public void StartBeHitEffect()
    {
        StartCoroutine(BeHitEffect());
    }

    public void StartDeadEffect()
    {
        StartCoroutine(DeadEffect());
    }

    public void StartAttackEffect()
    {
        EnvironmentManager.Instance.SpawnParticle(m_attackParticle, transform.position);
    }

    // ============================================
    private void Start()
    {
        ReadyToDie = false;
    }

    private void SetupProperties()
    {
        m_animator.SetBool("Reset", false);
    }

    private void ResetProperties()
    {
        ReadyToDie = false;
        
        m_animator.SetBool("Reset", true);
        m_animator.SetBool("Die", false);
    }

    private IEnumerator BeHitEffect()
    {
        Vector3 _position = m_model.transform.localPosition;

        _position.z -= m_beHitKnockback;
        m_model.transform.localPosition = _position;

        yield return new WaitForSeconds(m_beHitTime);

        _position.z += m_beHitKnockback;
        m_model.transform.localPosition = _position;
    }

    private IEnumerator DeadEffect()
    {
        m_animator.SetBool("Die", true);

        EnvironmentManager.Instance.SpawnParticle(m_deadParticle, transform.position);

        yield return new WaitForSeconds(m_deadTime);

        ReadyToDie = true;
    }

    private void OnEnable()
    {
        SetupProperties();
    }

    private void OnDisable()
    {
        ResetProperties();
    }
}
