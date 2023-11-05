using System.Collections;
using UnityEngine;

public class EnemyDebuff
{
    public enum DebuffType
    {
        Slow,
    }

    public DebuffType Type;
    public float Ammount;
    public float Duration;

    public EnemyDebuff(DebuffType _type, float _ammount, float _duration)
    {
        Type = _type;
        Ammount = _ammount;
        Duration = _duration;
    }
}