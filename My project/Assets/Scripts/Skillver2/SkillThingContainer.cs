using UnityEngine;
using UnityEngine.UI;

public class SkillThingContainer : MonoBehaviour
{
    [Header("Skill Button Container")]
    [SerializeField] private Button skill1;
    [SerializeField] private Button skill2;
    [SerializeField] private Button skill3;

    [Header("Skill Prefabs Container")]
    [Space(10)]
    [SerializeField] private GameObject[] skillPrefabs;
    [SerializeField] private GameObject[] skillPreviewPrefabs;

    [Header("Equipped Skill Slots")]
    [SerializeField] private int[] equippedSkill = new int[3] { -1, -1, -1 };

    public Button Skill1 { get {  return skill1; } }
    public Button Skill2 { get {  return skill2; } }
    public Button Skill3 { get {  return skill3; } }
    public GameObject[] SkillPreview { get { return skillPreviewPrefabs; } }
    public GameObject[] SkillPrefabs { get { return skillPrefabs; } }
    public int[] EquippedSkill { get { return equippedSkill; } }

    public void SetSkillIndex(int skillSlot, int skillIndex)
    {
        if (skillSlot >= 0 && skillSlot < 3 && skillIndex >= 0 && skillIndex < skillPrefabs.Length)
        {
            equippedSkill[skillSlot] = skillIndex;
        }
    }
}