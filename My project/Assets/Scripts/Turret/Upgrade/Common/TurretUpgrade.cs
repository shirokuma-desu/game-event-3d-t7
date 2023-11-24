using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretUpgrade : ScriptableObject
{
    [SerializeField]
    private string m_name;

    [SerializeField]
    private float m_cost; 

    public abstract void Apply(Turret _turret);
}
