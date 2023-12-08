using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string playScene;

    public void changeScenePlay()
    {
        LoadAsync.Instance.LoadScene(playScene);
    }

    public void ChangeScenePlayRaw()
    {
        SceneManager.LoadScene(playScene);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
