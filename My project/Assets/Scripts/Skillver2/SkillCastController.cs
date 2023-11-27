using UnityEditor.Rendering;
using UnityEngine;

public class SkillCastController : MonoBehaviour
{
    private SkillThingContainer container;
    private SkillVisualScript visualScript;
    private int[] equippedSkillIndex = new int[3];

    private float[] skillCooldown = new float[3];
    [HideInInspector] public float[] skillLastUsed = new float[3] { 0, 0, 0 };

    private UISkill m_skillUI;

    private void Awake()
    {
        container = GameObject.Find("SkillManager").GetComponent<SkillThingContainer>();
        visualScript = GameObject.Find("SkillManager").GetComponent<SkillVisualScript>();

        m_skillUI = UIManager.Instance.SkillUI;
    }

    private void Update()
    {
        #region Is Interactable Skill
        if (visualScript.isUsingSkill)
        {
            CanPressSkill(0, false);
            CanPressSkill(1, false);
            CanPressSkill(2, false);
        } else
        {
            CanPressSkill(0, true);
            CanPressSkill(1, true);
            CanPressSkill(2, true);
        }
        HandleCooldownSkill(0);
        HandleCooldownSkill(1);
        HandleCooldownSkill(2);
        #endregion

        #region Equip Skill
        for (int i = 0; i < 3; i++)
        {
            equippedSkillIndex[i] = container.EquippedSkill[i];
        }
        for (int i = 0; i < 3; i++)
        {
            if (equippedSkillIndex[i] >= 0 && equippedSkillIndex[i] <= 5)
            {
                skillCooldown[i] = container.SkillPrefabs[equippedSkillIndex[i]].GetComponent<SkillStats>().Cooldown;
                container.SetSkillIndex(i, equippedSkillIndex[i]);
            }
        }
        //skillCooldown[0] = container.SkillPrefabs[4].GetComponent<SkillStats>().Cooldown;
        //container.SetSkillIndex(0, 4);
        //skillCooldown[1] = container.SkillPrefabs[1].GetComponent<SkillStats>().Cooldown;
        //container.SetSkillIndex(1, 1);
        //skillCooldown[2] = container.SkillPrefabs[2].GetComponent<SkillStats>().Cooldown;
        //container.SetSkillIndex(2, 2);
        #endregion

        HandePropertiesSkillIndex();
    }

    #region Cast Skill

    public void CastSkill()
    {
        int _index = m_skillUI.GetCurrentSkillCasted();
        Debug.Log(_index);

        if (isSkillAvailable(_index))
        {
            switch (equippedSkillIndex[_index])
            {
                case 0:
                    visualScript.UseMeteorSkill();
                    visualScript.skillPressedIndex = _index;
                    visualScript.skillPrefabUsingIndex = 0;
                    break;
                case 1:
                    visualScript.UseAcidRainSkill();
                    visualScript.skillPressedIndex = _index;
                    visualScript.skillPrefabUsingIndex = 1;
                    break;
                case 2:
                    visualScript.UseLazerBeamSkill();
                    visualScript.skillPressedIndex = _index;
                    visualScript.skillPrefabUsingIndex = 2;
                    break;
                case 3:
                    visualScript.UseStarFallSkill();
                    visualScript.skillPressedIndex = _index;
                    visualScript.skillPrefabUsingIndex = 3;
                    break;
                case 4:
                    visualScript.UseShadeSkill();
                    visualScript.skillPressedIndex = _index;
                    visualScript.skillPrefabUsingIndex = 4;
                    break;
                case 5:
                    visualScript.UseLavaSkill();
                    visualScript.skillPressedIndex = _index;
                    visualScript.skillPrefabUsingIndex = 5;
                    break;
                default:
                    visualScript.skillPrefabUsingIndex = -1;
                    visualScript.skillPressedIndex = -1;
                    break;
            }
        }
    }
    #endregion

    #region Handle Interactable Skill
    private void HandleCooldownSkill(int index)
    {
        if (Time.time - skillLastUsed[index] < skillCooldown[index])
        {
            CanPressSkill(index, false);
        }
    }

    private void CanPressSkill(int index, bool able)
    {
        m_skillUI.SkillButtons[index].interactable = able;
    }

    private bool isSkillAvailable(int index)
    {
        float currentTime = Time.time;
        return currentTime - skillLastUsed[index] >= skillCooldown[index];
    }

    private void HandePropertiesSkillIndex()
    {
        if (visualScript.isCurrentPreviewAvai())
        {
            visualScript.skillPrefabUsingIndex = -1;
            visualScript.skillPressedIndex = -1;
        }
    }
    #endregion
}