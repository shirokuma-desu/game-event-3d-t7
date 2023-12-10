using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeakyAbstraction;

public class ClickSound : MonoBehaviour
{
    public void PlayClickSound()
    {
        SoundManager.Instance.PlaySound(GameSound.UIClick);
    }
}
