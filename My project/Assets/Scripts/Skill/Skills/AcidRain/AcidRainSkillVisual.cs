using System.Collections;
using System.Collections.Generic;
using LeakyAbstraction;
using UnityEngine;

public class AcidRainSkillVisual : SkillVisual
{
    [SerializeField]
    private AcidRainSkill m_skillData;

    [Header("Acid Rain")]
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

        Vector3 _position = m_skillData.Manager.GetMousePoint();
        _position.y = m_previewObject.transform.position.y;
        m_previewObject.transform.position = _position;

        Vector3 _scale = m_previewObject.transform.localScale;
        _scale.x = m_skillData.Range / 4f;
        _scale.z = m_skillData.Range / 4f;
        m_previewObject.transform.localScale = _scale;
    }

    public override void PrepareCastVisual()
    {
        base.PrepareCastVisual();

        m_acidRainObject.SetActive(true);
        
        Vector3 _position = m_skillData.CastPosition;
        _position.y = m_acidRainObject.transform.position.y;
        m_acidRainObject.transform.position = _position;

        Vector3 _scale = m_acidRainObject.transform.localScale;
        _scale.x = m_skillData.Range * 2f;
        _scale.z = m_skillData.Range * 2f;
        m_acidRainObject.transform.localScale = _scale;

        SoundManager.Instance.PlaySound(GameSound.AcidCast);
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
