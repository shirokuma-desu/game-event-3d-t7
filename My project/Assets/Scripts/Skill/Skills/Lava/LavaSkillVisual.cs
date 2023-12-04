using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSkillVisual : SkillVisual
{
    [SerializeField]
    private LavaSkill m_skillData;

    [Header("Lava")]
    [SerializeField]
    private GameObject m_lavaObject;

    public override void SetUp()
    {
        base.SetUp();

        m_lavaObject.SetActive(false);
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

        m_lavaObject.SetActive(true);
        
        m_lavaObject.transform.position = m_skillData.CastPosition;

        Vector3 _scale = m_lavaObject.transform.localScale;
        _scale.x = m_skillData.Range * 2f;
        _scale.z = m_skillData.Range * 2f;
        m_lavaObject.transform.localScale = _scale;
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
