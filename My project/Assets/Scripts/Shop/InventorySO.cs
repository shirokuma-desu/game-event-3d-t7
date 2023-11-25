using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySO", menuName = "Scriptable Objects/Shop/Inventory", order = 1)]
public class InventorySO : ScriptableObject
{
    public List<SkillObjectSO> m_Inventory = new List<SkillObjectSO>();
}