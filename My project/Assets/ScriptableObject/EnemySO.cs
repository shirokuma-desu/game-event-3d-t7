using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/Stat/EnemyStatSO", order = 1)]
public class EnemySO : ScriptableObject
{
    public int health;
    public int attackDamage;
    public float speed;
    public int moneyDrop;
}
