using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIHandler : MonoBehaviour
{

    [SerializeField] GameObject pop_up_canvas;

    // Start is called before the first frame update
    void Start()
    {
        pop_up_canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPause()
    {
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
    
}
