using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SkillObjSO",menuName ="Scriptable Objects/Shop/Item",order = 1)]
public class ItemDataSO : ScriptableObject
{
    [Header("ID")]
    public int ID_Skill;
    [Header("UI")]
    public Sprite image;
    public Sprite image_upgrade;
    public string skill_name;
    public string description;
    public int    multi_cast_chance;
    [Header("Currency Stats")]
    public int price;
    public int sellprice;
    public int price_increase;
    public int sell_price_increase;
    [Header("Common Stats")]
    public int level_skill;
    public int damage;
    [Header("Skill Stats")]
    public int radius;
    public float cooldown;
    public int instance_per_cast;
    public float debuff_duration;
    public float debuff_effective;
    [Header("Upgrade Stats Per Level")]
    public int damage_increase;
    public int radius_increase;
    public float cooldown_decrease;
    public int instance_increase;
    public float debuff_duration_increase;
    public float debuff_effective_increase;
    [Header("Turret Effect")]
    public int ID_Effect;
    public float stun_duration_increase;
    public float knockback_distance_increase;
    public float knockback_duration_increase;
    [Header("Turret Stats Per Level")]
    public int range_increase;
    public int fire_rate_increase;
    public int hp_increase;
    [Header("Mics Stats")]
    public bool is_upgraded;
    
}
