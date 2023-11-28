using UnityEngine;

public class SkillThingContainer : MonoBehaviour
{
    [Header("Skill Prefabs Container")]
    [Space(10)]
    [SerializeField] private GameObject[] skillPrefabs;
    [SerializeField] private GameObject[] skillPreviewPrefabs;

    [Header("Equipped Skill Slots")]
    [SerializeField] private int[] equippedSkill = new int[3] { -1, -1, -1 };

    [Header("Skill GameEvent")]
    [SerializeField] private GameEvent castSkill;
    [SerializeField] private GameEvent useMulticast;

    public GameObject[] SkillPreview { get { return skillPreviewPrefabs; } }
    public GameObject[] SkillPrefabs { get { return skillPrefabs; } }
    public int[] EquippedSkill { get { return equippedSkill; } }
    public GameEvent CastSkill { get { return castSkill; } }
    public GameEvent UseMulticast { get {  return useMulticast; } }

    public void SetSkillIndex(int skillSlot, int skillIndex)
    {
        if (skillSlot >= 0 && skillSlot < 3 && skillIndex >= 0 && skillIndex < skillPrefabs.Length)
        {
            equippedSkill[skillSlot] = skillIndex;
        }
    }
}