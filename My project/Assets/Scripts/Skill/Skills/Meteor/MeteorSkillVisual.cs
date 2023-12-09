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
    private GameObject m_guideObject;
    [SerializeField]
    private GameObject m_impactObject;

    [SerializeField]
    private float m_meteorAccelerate;
    private float m_meteorSpeed;
    [SerializeField]
    private float m_meteorAngle;
    [SerializeField]
    private float m_meteorOffset;
    private Vector3 m_meteorDirection;

    public override void SetUp()
    {
        base.SetUp();

        m_meteorObject.SetActive(false);
        m_guideObject.SetActive(false);
    }

    public override void PreviewVisual()
    {
        base.PreviewVisual();

        Vector3 _position = m_skillData.Manager.GetMousePoint();
        _position.y = m_previewObject.transform.position.y;
        m_previewObject.transform.position = _position;

        Vector3 _scale = m_previewObject.transform.localScale;
        _scale.x = m_skillData.Range;
        _scale.y = m_skillData.Range;
        m_previewObject.transform.localScale = _scale;
    }

    public override void PrepareCastVisual()
    {
        base.PrepareCastVisual();

        m_guideObject.SetActive(true);

        Vector3 _position = m_skillData.CastPosition;
        _position.y = m_guideObject.transform.position.y;
        m_guideObject.transform.position = _position;

        Vector3 _scale = m_guideObject.transform.localScale;
        _scale.x = m_skillData.Range;
        _scale.z = m_skillData.Range;
        m_guideObject.transform.localScale = _scale;

        m_meteorObject.SetActive(true);

        _position = m_skillData.CastPosition;

        float _meteorTravelDistance = .5f * m_meteorAccelerate * m_skillData.CastDelay * m_skillData.CastDelay;

        m_meteorDirection = (m_skillData.CastPosition - EnvironmentManager.Instance.PlayerPosition).normalized;
        _position += -m_meteorDirection * Mathf.Cos(m_meteorAngle * Mathf.Deg2Rad) * _meteorTravelDistance;
        _position.y += Mathf.Sin(m_meteorAngle * Mathf.Deg2Rad) * _meteorTravelDistance + m_meteorOffset;
        
        m_meteorObject.transform.position = _position;
        m_meteorDirection = (m_skillData.CastPosition - m_meteorObject.transform.position).normalized;
    }
    public override void CastVisual()
    {
        base.CastVisual();

        m_meteorSpeed += m_meteorAccelerate * Time.deltaTime;
        m_meteorObject.transform.position += m_meteorDirection * m_meteorSpeed * Time.deltaTime;
    }
    public override void ImpactVisual()
    {
        base.ImpactVisual();
        
        m_meteorObject.SetActive(false);
        m_guideObject.SetActive(false);
        
        GameObject _obj = Instantiate(m_impactObject, m_skillData.CastPosition, Quaternion.identity);
        _obj.GetComponent<ParticleSystem>().startSize = m_skillData.Range / 3f;
    }
}
