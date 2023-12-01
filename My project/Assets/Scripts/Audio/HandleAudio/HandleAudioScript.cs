using LeakyAbstraction;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class HandleAudioScript : MonoBehaviour
{
    private void Start()
    {
        PlayingBGM();
        PlayingAmbient();
    }

    private void PlayingBGM()
    {
        SoundManager.Instance.PlaySound(GameSound.BGM, (GameSound _loop) => PlayingBGM());
    }

    private void PlayingAmbient()
    {
        SoundManager.Instance.PlaySound(GameSound.Ambient, (GameSound _loop) => PlayingAmbient());
    }
}
