using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeakyAbstraction;

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
        _scale.x = m_skillData.Range / 4f;
        _scale.z = m_skillData.Range / 4f;
        m_previewObject.transform.localScale = _scale;
    }

    public override void PrepareCastVisual()
    {
        base.PrepareCastVisual();

        SoundManager.Instance.PlaySound(GameSound.StarCast);
    }
    public override void CastVisual()
    {
        base.CastVisual();


    }
}
