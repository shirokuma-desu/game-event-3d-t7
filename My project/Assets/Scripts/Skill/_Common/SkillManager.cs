using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private List<Skill> m_skillPrototypes;

    [SerializeField]
    private InventorySO m_inventoryData;
    private Skill[] m_availableSkills = new Skill[3];
    public bool IsAvailableSkill(int _index) => m_availableSkills != null;

    private Skill m_currentSkillSelected;
    public Skill CurrentSkillSelected { get => m_currentSkillSelected; }
    private bool m_isSelectingASkill;

    [Header("GameEvents")]
    [SerializeField]
    private GameEvent m_castASkill;

    public void SelectSkill()
    {
        if (!m_isSelectingASkill)
        {
            SelectSkill(UIManager.Instance.SkillUI.GetCurrentSkillSelect());
        }
        else 
        {
            Debug.LogWarning($"SkillManager: Already selected a skill"); 
        }
    }

    public void SelectSkill(int _index)
    {
        if (!IsAvailableSkill(_index))
        {
            Debug.LogWarning($"SkillManager: Skill {_index} is not available or not exist"); 
            return;
        }

        m_currentSkillSelected = Instantiate(m_availableSkills[_index]);
        m_currentSkillSelected.Manager = this;
        m_currentSkillSelected.Select();

        m_isSelectingASkill = true;
    }

    public void CastSkill()
    {
        if (m_currentSkillSelected == null)
        {
            Debug.LogWarning("SkillManager: There's no selected skill to cast");
            return;
        }

        m_currentSkillSelected.Cast();

        ResetCurrentSkillSelect();

        m_castASkill.RaiseEvent();
    }

    public void DiscardSkill()
    {
        if (m_currentSkillSelected == null)
        {
            Debug.LogWarning("SkillManager: There's no selected skill to discard");
            return;
        }

        m_currentSkillSelected.Discard();

        ResetCurrentSkillSelect();
    }

    public Vector3 GetMousePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 hitPoint = ray.GetPoint(rayDistance);
            return hitPoint;
        }

        return Vector3.zero;
    }

    private void Start()
    {
        ResetCurrentSkillSelect();
    }

    private void Update()
    {
        SetAvaiableSkills();

        if (m_isSelectingASkill)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CastSkill();
            }
            if (Input.GetMouseButtonDown(1))
            {
                DiscardSkill();
            }
        }
    }

    private void SetAvaiableSkills()
    {
        for (int i = 0; i < m_inventoryData.m_Inventory_Skill.Count; i++)
        {
            m_availableSkills[i] = GetSkillByID(m_inventoryData.m_Inventory_Skill[i].ID_Skill);
        }
    }

    private void ResetCurrentSkillSelect()
    {
        m_currentSkillSelected = null;
        m_isSelectingASkill = false;
    }

    private Skill GetSkillByID(int _ID)
    {
        foreach (Skill _skill in m_skillPrototypes)
        {
            if (_skill.ID == _ID)
            {
                return _skill;
            }
        }

        return null;
    }
}
