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


    }
    public override void CastVisual()
    {
        base.CastVisual();
    }
}
