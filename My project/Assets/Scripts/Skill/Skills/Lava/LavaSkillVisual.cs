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

        m_lavaObject.SetActive(true);
        
        Vector3 _position = m_skillData.CastPosition;
        _position.y = m_lavaObject.transform.position.y;
        m_lavaObject.transform.position = _position;

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
