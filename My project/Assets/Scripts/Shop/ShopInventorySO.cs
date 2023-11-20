using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopInventorySO", menuName = "Scriptable Objects/Shop/Inventory", order = 1)]
public class ShopInventorySO : ScriptableObject
{
    public List<SkillObjectSO> m_Inventory = new List<SkillObjectSO>();
}
