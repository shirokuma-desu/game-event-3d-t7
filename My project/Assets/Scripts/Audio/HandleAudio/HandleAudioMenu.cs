using LeakyAbstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAudioMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayingMenuBGM();
    }

    private void PlayingMenuBGM()
    {
        SoundManager.Instance.PlaySound(GameSound.MenuBGM, (GameSound _loop) => PlayingMenuBGM());
    }
}
