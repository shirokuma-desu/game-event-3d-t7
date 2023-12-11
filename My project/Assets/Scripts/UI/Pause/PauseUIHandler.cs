using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIHandler : MonoBehaviour
{

    [SerializeField] GameObject pop_up_canvas;
    [SerializeField] GameObject pause_button;
    [SerializeField] GameObject resume_button;


    // Start is called before the first frame update
    void Start()
    {
        pop_up_canvas.SetActive(false);
        resume_button.SetActive(false);
        pause_button.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPause()
    {
        pause_button.SetActive(false);
        resume_button.SetActive(true);
        Time.timeScale = 0f;

    }

    public void onClickExit()
    {
        pop_up_canvas.SetActive(true);
    }

    public void OnClickYes()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void onClickNo() {
        pop_up_canvas.SetActive(false);
    }
    public void OnclickResume()
    {
        resume_button.SetActive(false);
        pause_button.SetActive(true);
        Time.timeScale = 1.0f;
    }
    
}
