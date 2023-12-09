using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="TimeScore", menuName ="Scriptable Objects/TimeScore", order = 1)]
public class TimeScore : ScriptableObject
{
    public float BestTime;
    public float CurrentTime;
} 