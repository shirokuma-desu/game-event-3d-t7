using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillVisual : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    protected GameObject m_previewObject;
    public GameObject PreviewObject { get => m_previewObject; }

    public virtual void SetUp()
    {
        m_previewObject.SetActive(false);
    }

    public virtual void PreviewVisual()
    {
        ShowPreview();
    }

    public virtual void PrepareCastVisual()
    {

    }
    public virtual void CastVisual()
    {
        HidePreview();
    }

    public virtual void ImpactVisual()
    {

    }

    public virtual void ExpireVisual()
    {

    }

    public virtual void ShowPreview()
    {
        PreviewObject.SetActive(true);
    }

    public virtual void HidePreview()
    {
        PreviewObject.SetActive(false);
    }
}
