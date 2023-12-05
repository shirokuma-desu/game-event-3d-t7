using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string playScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScenePlay()
    {
        SceneManager.LoadScene(playScene);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
