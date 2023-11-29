using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField]
    private GameObject m_model;

    [Header("BeHit Effect")]
    [SerializeField]
    private Color m_beHitColor;
    [SerializeField]
    private float m_beHitKnockback;
    [SerializeField]
    private float m_beHitTime;

    [Header("Dead Effect")]
    [SerializeField]
    private GameObject m_deadPartical;
    [SerializeField]
    private float m_deadKnockback;
    [SerializeField]
    private float m_deadTime;
    public bool ReadyToDie { get; private set; }

    public void StartBeHitEffect()
    {
        StartCoroutine(BeHitEffect());
    }

    public void StartDeadEffect()
    {
        StartCoroutine(DeadEffect());
    }

    // ============================================
    private void Start()
    {
        m_deadPartical.SetActive(false);

        ReadyToDie = false;
    }

    private void SetupProperties()
    {

    }

    private void ResetProperties()
    {
        m_deadPartical.SetActive(false);

        ReadyToDie = false;
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
        m_deadPartical.SetActive(true);

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
