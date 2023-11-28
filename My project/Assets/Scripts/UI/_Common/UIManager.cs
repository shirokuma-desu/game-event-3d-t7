using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : GenericSingleton<UIManager>
{
    [SerializeField]
    private UISkill m_skillUI;
    public UISkill SkillUI { get => m_skillUI; }

    [SerializeField]
    private UIBaseTower m_baseTowerUI;
    public UIBaseTower BaseTowerUI { get => m_baseTowerUI; }
}
