using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SkipIntro : MonoBehaviour
{

    [SerializeField] private double time;
    [SerializeField] private double currentTime;
    [SerializeField] private VideoPlayer introvideo;
    [SerializeField] private GameObject text_popup;

    // Start is called before the first frame update
    void Start()
    {
        text_popup.SetActive(false);
        time = introvideo.clip.length - 3.25f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = introvideo.time;
        if (currentTime >= time)
        {
            SceneManager.LoadScene("StartMenu");
            Debug.Log("//do change scene");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartMenu");
        }
        if (Input.anyKeyDown)
        {
            text_popup.SetActive(true);
        }
    }


}
