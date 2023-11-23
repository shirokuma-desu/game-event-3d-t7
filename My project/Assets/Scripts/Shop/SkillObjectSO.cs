using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SkillObjSO",menuName ="Scriptable Objects/Shop/Skill",order = 1)]
public class SkillObjectSO : ScriptableObject
{
    public int ID_Skill;
    public string skill_name;
    public Sprite image;
    public Sprite image_upgrade;
    public int price;
    public int sellprice;
    public string description;
    public bool is_upgraded;
}
