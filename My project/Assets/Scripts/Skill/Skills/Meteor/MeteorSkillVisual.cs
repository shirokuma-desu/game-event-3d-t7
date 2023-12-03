using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSkillVisual : SkillVisual
{
    [SerializeField]
    private MeteorSkill m_skillData;

    [Header("Meteor")]
    [SerializeField]
    private GameObject m_meteorObject;
    [SerializeField]
    private GameObject m_impactParticleObject;

    [SerializeField]
    private float m_meteorAccelerate;
    private float m_meteorSpeed;
    [SerializeField]
    private float m_meteorAngle;
    [SerializeField]
    private float m_meteorOffset;

    public override void SetUp()
    {
        base.SetUp();

        m_meteorObject.SetActive(false);
        m_impactParticleObject.SetActive(false);
    }

    public override void PreviewVisual()
    {
        base.PreviewVisual();

        m_previewObject.transform.position = m_skillData.Manager.GetMousePoint();

        Vector3 _scale = m_previewObject.transform.localScale;
        _scale.x = m_skillData.Range * 2f;
        _scale.z = m_skillData.Range * 2f;
        m_previewObject.transform.localScale = _scale;
    }

    public override void PrepareCastVisual()
    {
        base.PrepareCastVisual();

        m_meteorObject.SetActive(true);

        Vector3 _position = m_skillData.CastPosition;

        float _meteorTravelDistance = .5f * m_meteorAccelerate * m_skillData.CastDelay * m_skillData.CastDelay;
        Vector3 _direction = (m_skillData.CastPosition - m_meteorObject.transform.position).normalized;
        _position += -_direction * Mathf.Cos(m_meteorAngle * Mathf.Deg2Rad) * _meteorTravelDistance;
        _position.y += Mathf.Sin(m_meteorAngle * Mathf.Deg2Rad) * _meteorTravelDistance + m_meteorOffset;

        m_meteorObject.transform.position = _position;
    }
    public override void CastVisual()
    {
        base.CastVisual();

        Vector3 _direction = (m_skillData.CastPosition - m_meteorObject.transform.position).normalized;
        m_meteorSpeed += m_meteorAccelerate * Time.deltaTime;
        m_meteorObject.transform.position += _direction * m_meteorSpeed * Time.deltaTime;
    }
    public override void ImpactVisual()
    {
        base.ImpactVisual();

        m_impactParticleObject.SetActive(true);

        
    }
}
