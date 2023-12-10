using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private List<Skill> m_skillPrototypes;

    [SerializeField]
    private InventorySO m_inventoryData;
    private Skill[] m_availableSkills = new Skill[3];
    private int m_availableSkillNumber;
    public int AvailableSkillNumber { get => m_availableSkillNumber; }

    private float[] m_skillCooldownFull = new float[3];
    public float[] SkillCooldownFull { get => m_skillCooldownFull; }
    private float[] m_skillCooldownLeft = new float[3];
    public float[] SkillCooldownLeft { get => m_skillCooldownLeft; }

    private Skill m_currentSkillSelected;
    public Skill CurrentSkillSelected { get => m_currentSkillSelected; }
    private int m_currentSkillSelectedIndex;
    private int CurrentSkillSelectedIndex {get => m_currentSkillSelectedIndex; }
    private bool m_isSelectingASkill;

    [SerializeField]
    private int m_maxMulticastTime;
    private int m_multicastTime;
    public int MulticastTime { get => m_multicastTime; }
    private float m_multicastRateScale;

    [Header("GameEvents")]
    [SerializeField]
    private GameEvent m_castASkill;
    
    public void GetANewSkill()
    {
        if (m_availableSkillNumber >= 3) 
        {
            Debug.LogWarning($"SkillManager: Skill slot are full"); 
            return;
        }

        int _index = m_availableSkillNumber;
        m_availableSkills[_index] = GetSkillByID(m_inventoryData.m_Inventory_Skill[_index].ID_Skill);

        m_availableSkillNumber++;

        UpdateAvailableSkillsProperties(_index);

        SetSkillFullCooldown(_index, m_availableSkills[_index].Cooldown);
        SetSkillLeftCooldown(_index, 0f);
    }
    public void ReleaseASkill()
    {
        List<int> _currentAvailableSkillID = new();
        for (int i = 0; i < m_availableSkillNumber; i++)
        {
            _currentAvailableSkillID.Add(m_availableSkills[i].ID);
        }

        int _index = 0;
        while (_index < m_inventoryData.m_Inventory_Skill.Count)
        {
            if (_currentAvailableSkillID[_index] != m_inventoryData.m_Inventory_Skill[_index].ID_Skill) break;

            _index++;
        }

        for (int i = _index; i < m_availableSkillNumber - 1; i++)
        {
            m_availableSkills[i] = m_availableSkills[i + 1];
        }
        m_availableSkills[m_availableSkillNumber - 1] = null;

        m_availableSkillNumber--;

        for (int i = 0; i < m_availableSkillNumber; i++) 
        {
            UpdateAvailableSkillsProperties(i);

            SetSkillFullCooldown(i, m_skillCooldownFull[i + 1]);
            SetSkillLeftCooldown(_index, m_skillCooldownLeft[i + 1]);
        }
    }
    public void SetupAvailableSkills()
    {
        for (int i = 0; i < m_inventoryData.m_Inventory_Skill.Count; i++)
        {
            m_availableSkills[i] = GetSkillByID(m_inventoryData.m_Inventory_Skill[i].ID_Skill);

            m_availableSkillNumber++;

            UpdateAvailableSkillsProperties(i);

            SetSkillFullCooldown(i, m_availableSkills[i].Cooldown);
            SetSkillLeftCooldown(i, 0f);
        }
    }
    public void UpdateAvailableSkillsProperties(int _index)
    {
        m_availableSkills[_index].SetUp();
    }

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
        if (_index >= m_availableSkillNumber)
        {
            Debug.LogWarning($"SkillManager: Skill {_index} is not available or not exist"); 
            return;
        }

        if (m_isSelectingASkill)
        {
            Debug.LogWarning($"SkillManager: Already selected a skill"); 
            return;
        }

        m_currentSkillSelectedIndex = _index;

        m_currentSkillSelected = Instantiate(m_availableSkills[_index]);
        m_currentSkillSelected.SetUp();
        m_currentSkillSelected.Manager = this;
        m_currentSkillSelected.Preview();

        m_isSelectingASkill = true;
    }

    public void CastSelectSkillInit()
    {
        if (m_currentSkillSelected == null)
        {
            Debug.LogWarning("SkillManager: There's no selected skill to cast");
            return;
        }

        m_multicastTime = GetMulticastTime(m_currentSkillSelected);
        m_currentSkillSelected.CastInit(GetMousePoint(), m_multicastTime);

        ResetSkillCooldownToFull(m_currentSkillSelectedIndex);

        ResetCurrentSkillSelect();

        m_castASkill.RaiseEvent();
    }

    public void CastSkillRaw(Skill _skill, Vector3 _position)
    {
        Skill _instance = Instantiate(_skill);
        _instance.Visual.HidePreview();
        _instance.SetUp();
        _instance.CastRaw(_position);
    }
    public void CastSkillRaw(int _id, Vector3 _position)
    {
        Skill _skill = GetSkillByID(_id);

        Skill _instance = Instantiate(_skill);
        _instance.Visual.HidePreview();
        _instance.SetUp();
        _instance.CastRaw(_position);
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

        SetupAvailableSkills();

        m_multicastRateScale = 1f;
    }

    private void Update()
    {
        UpdateSkillsCoolDown();

        if (m_isSelectingASkill)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CastSelectSkillInit();
            }
            if (Input.GetMouseButtonDown(1))
            {
                DiscardSkill();
            }
        }


        if (Input.GetKeyDown(KeyCode.Q) && m_skillCooldownLeft[0] <= 0f)
        {
            SelectSkill(0);
        }

        if (Input.GetKeyDown(KeyCode.W) && m_skillCooldownLeft[1] <= 0f)
        {
            SelectSkill(1);
        }

        if (Input.GetKeyDown(KeyCode.E) && m_skillCooldownLeft[2] <= 0f)
        {
            SelectSkill(2);
        }
    }

    private void UpdateSkillsCoolDown()
    {
        for (int i = 0; i < m_availableSkillNumber; i++)
        {
            m_skillCooldownLeft[i] -= Time.deltaTime;
            if (m_skillCooldownLeft[i] < 0) m_skillCooldownLeft[i] = 0f;
        }
    }
    private void ResetSkillCooldownToFull(int _index)
    {
        if (_index >= m_availableSkillNumber)
        {
            Debug.LogWarning($"SkillManager: Skill {_index} is not available or not exist"); 
            return;
        }

        m_skillCooldownLeft[_index] = m_skillCooldownFull[_index];
    }
    private void SetSkillFullCooldown(int _index, float _time)
    {
        m_skillCooldownFull[_index] = _time;
    }
    private void SetSkillLeftCooldown(int _index, float _time)
    {
        m_skillCooldownLeft[_index] = _time;
    }

    private int GetMulticastTime(Skill _skill)
    {
        for (int i = 0; i < m_maxMulticastTime; i++)
        {
            if (Random.Range(0f, 100f) > _skill.MulticastRate * m_multicastRateScale)
            {
                return i;
            }
        }

        return m_maxMulticastTime;
    }

    private void ResetCurrentSkillSelect()
    {
        m_currentSkillSelectedIndex = -1;
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
