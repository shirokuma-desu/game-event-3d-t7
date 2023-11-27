using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableSkillManager : MonoBehaviour
{
    [SerializeField]
    private InventorySO m_inventory;

    [SerializeField]
    private SkillThingContainer m_skillContainer;

    public void UpdateInventorySkill()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i >= m_inventory.m_Inventory_Skill.Count)
            {
                m_skillContainer.SetSkillIndex(i, 6);
            }   
            else
            {
                int _index = m_inventory.m_Inventory_Skill[i].ID_Skill - 1;
                
                m_skillContainer.SetSkillIndex(i, _index);
            }
        }
    }

    private void Start()
    {
        UpdateInventorySkill();
    }
}
