using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeakyAbstraction;

public class UIManager : GenericSingleton<UIManager>
{
    [SerializeField]
    private UISkill m_skillUI;
    public UISkill SkillUI { get => m_skillUI; }

    [SerializeField]
    private UIBaseTower m_baseTowerUI;
    public UIBaseTower BaseTowerUI { get => m_baseTowerUI; }

    [SerializeField]
    private ShopUIManager m_shopUI;
    public ShopUIManager ShopUI { get => m_shopUI; }


    public void PlayClickSound()
    {
        SoundManager.Instance.PlaySound(GameSound.UIClick);
    }
}
