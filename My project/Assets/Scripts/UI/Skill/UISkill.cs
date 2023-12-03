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
    private Sprite m_emptySprite;

    [SerializeField]
    private InventorySO m_inventory;

    private int m_currentSkillSelected; 

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
}
