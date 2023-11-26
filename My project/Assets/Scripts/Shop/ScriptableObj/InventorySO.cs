using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventorySO", menuName = "Scriptable Objects/Shop/Inventory", order = 1)]
public class InventorySO : ScriptableObject
{
    public List<ItemDataSO> m_Inventory_Skill = new List<ItemDataSO>();
    public List<ItemDataSO> m_Inventory_Turret = new List<ItemDataSO>();
}
