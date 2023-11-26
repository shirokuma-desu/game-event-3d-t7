using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySO", menuName = "Scriptable Objects/Shop/Inventory", order = 1)]
public class InventorySO : ScriptableObject
{
    public List<ItemData> m_Inventory_Skill = new List<ItemData>();
    public List<ItemData> m_Inventory_Turret = new List<ItemData>();
}
