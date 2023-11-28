using LeakyAbstraction;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class HandleAudioScript : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlaySound(GameSound.BGM).loop = true;

        SoundManager.Instance.PlaySound(GameSound.Ambient).loop = true;
    }
}
