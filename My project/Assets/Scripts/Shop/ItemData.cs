using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SkillObjSO",menuName ="Scriptable Objects/Shop/Item",order = 1)]
public class ItemData : ScriptableObject
{
    public int ID_Skill;
    public string skill_name;
    public int level_skill;
    public Sprite image;
    public Sprite image_upgrade;
    public int price;
    public int price_increase;
    public int sell_price_increase;
    public int sellprice;
    public string description;
    public bool is_upgraded;
}
