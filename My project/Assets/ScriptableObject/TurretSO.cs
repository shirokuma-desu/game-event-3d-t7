using UnityEngine;

[CreateAssetMenu(fileName = "TurretSO", menuName = "Scriptable Objects/Stat/TurretStatSO", order = 1)]
public class TurretSO : ScriptableObject
{
    public int health;
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;
}
