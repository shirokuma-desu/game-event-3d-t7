using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBeamSkillVisual : SkillVisual
{
    [SerializeField]
    private LazerBeamSkill m_skillData;
    
    [Header("Lazer")]
    [SerializeField]
    private GameObject m_prepareBeamObject;
    [SerializeField]
    private GameObject m_beamObject;

    public override void SetUp()
    {
        base.SetUp();

        m_prepareBeamObject.SetActive(false);
        m_beamObject.SetActive(false);
    }

    public override void PreviewVisual()
    {
        base.PreviewVisual();

        m_previewObject.transform.position = EnvironmentManager.Instance.PlayerPosition;

        Vector3 _direction = m_skillData.Manager.GetMousePoint() - m_previewObject.transform.position;
        m_previewObject.transform.forward = _direction;

        Vector3 _scale = m_previewObject.transform.localScale;
        _scale.z = m_skillData.Range;
        m_previewObject.transform.localScale = _scale;
    }

    public override void PrepareCastVisual()
    {
        base.PrepareCastVisual();

        m_prepareBeamObject.SetActive(true);
    }
    public override void CastVisual()
    {
        base.CastVisual();

        m_prepareBeamObject.transform.position = EnvironmentManager.Instance.PlayerPosition;

        Vector3 _direction = m_skillData.CastPosition - transform.position;
        m_prepareBeamObject.transform.forward = _direction;

        Vector3 _scale = m_prepareBeamObject.transform.localScale;
        _scale.z = m_skillData.Range;
        m_prepareBeamObject.transform.localScale = _scale;
    }

    public override void ImpactVisual()
    {
        base.ImpactVisual();

        m_beamObject.SetActive(true);

        m_beamObject.transform.position = EnvironmentManager.Instance.PlayerPosition;

        Vector3 _direction = m_skillData.CastPosition - transform.position;
        m_beamObject.transform.forward = _direction;

        Vector3 _scale = m_beamObject.transform.localScale;
        _scale.z = m_skillData.Range;
        _scale.x = m_skillData.Radius * .7f;
        m_beamObject.transform.localScale = _scale;
    }
}
