using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfallSkillVisual : SkillVisual
{
    [SerializeField]
    private StarfallSkill m_skillData;

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


    }
    public override void CastVisual()
    {
        base.CastVisual();
    }
}
