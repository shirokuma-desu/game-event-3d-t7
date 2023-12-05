using UnityEngine;


[CreateAssetMenu(fileName = "TurretSO", menuName = "Scriptable Objects/Stat/TurretStat", order = 1)]
public class TurretStatSO : ScriptableObject
{
    public int health;
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;

    public float stunDuration;
    public float knockbackAmount;
    public float knockbackDuration;
}
