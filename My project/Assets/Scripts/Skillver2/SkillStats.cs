using UnityEngine;

public class SkillStats : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private float cooldown;
    [SerializeField] private float duration;
    [SerializeField] private int numberOfPieces;
    [SerializeField] private float multicastRate;

    public int Damage { get { return damage; } }
    public float Range { get { return range; } }
    public float Cooldown { get {  return cooldown; } }
    public float Duration { get { return duration; } }
    public int NumberOfPieces { get { return numberOfPieces; } }
    public float MulticastRate { get { return multicastRate; } }
}