using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UISkill : MonoBehaviour
{
    [SerializeField]
    private Button[] m_skillButtons = new Button[3];
    public Button[] SkillButtons { get => m_skillButtons; }

    [SerializeField]
    private Image[] m_skillImages = new Image[3];

    [SerializeField]
    private SpellCooldown[] m_skillCooldown = new SpellCooldown[3];

    [SerializeField]
    private Sprite m_emptySprite;

    [SerializeField]
    private InventorySO m_inventory;

    private int m_currentSkillSelected; 

    [SerializeField]
    private SkillManager m_skillManager;

    [Header("Game Events")]
    [SerializeField]
    private GameEvent m_aSkillIsSelect;

    public void UpdateInventorySkillUI()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i >= m_inventory.m_Inventory_Skill.Count)
            {
                m_skillImages[i].sprite = m_emptySprite;
            }   
            else 
            {
                m_skillImages[i].sprite = m_inventory.m_Inventory_Skill[i].image;
            }
        }
    }
    public void SelectSkill(int _index)
    {
        m_currentSkillSelected = _index;
        
        m_aSkillIsSelect.RaiseEvent();
    }

    public int GetCurrentSkillSelect()
    {
        return m_currentSkillSelected;
    }

    private void Start()
    {
        UpdateInventorySkillUI();
    }

    private void Update()
    {
        for (int i = 0; i < m_skillManager.AvailableSkillNumber; i++)
        {
            if (m_skillManager.SkillCooldownLeft[i] > 0) m_skillButtons[i].interactable = false;
            else m_skillButtons[i].interactable = true;

            m_skillCooldown[i].SetCooldownTime(m_skillManager.SkillCooldownFull[i]);
            m_skillCooldown[i].SetCooldownTimer(m_skillManager.SkillCooldownLeft[i]);
        }

        for (int i = m_skillManager.AvailableSkillNumber; i < 3; i++)
        {
            m_skillButtons[i].interactable = false;

            m_skillCooldown[i].SetCooldownTime(0f);
            m_skillCooldown[i].SetCooldownTimer(0f);
        }
    }
}
