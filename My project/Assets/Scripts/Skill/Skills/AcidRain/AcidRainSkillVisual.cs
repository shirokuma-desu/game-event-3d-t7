using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRainSkillVisual : SkillVisual
{
    [SerializeField]
    private AcidRainSkill m_skillData;

    [Header("Meteor")]
    [SerializeField]
    private GameObject m_acidRainObject;

    public override void SetUp()
    {
        base.SetUp();

        m_acidRainObject.SetActive(false);
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

        m_acidRainObject.SetActive(true);
        
        m_acidRainObject.transform.position = m_skillData.CastPosition;

        Vector3 _scale = m_acidRainObject.transform.localScale;
        _scale.x = m_skillData.Range * 2f;
        _scale.z = m_skillData.Range * 2f;
        m_acidRainObject.transform.localScale = _scale;
    }
    public override void CastVisual()
    {
        base.CastVisual();
    }

    public override void ImpactVisual()
    {
        base.ImpactVisual();

        
    }
}
