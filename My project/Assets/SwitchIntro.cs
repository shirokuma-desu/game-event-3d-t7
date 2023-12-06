using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class SwitchIntro : MonoBehaviour
{
    private bool isTransitioning = false;
    private float currentTime = 0f;
    public GameObject UIcanvas;
    public VideoPlayer videoPlayer;

    public float transitionDuration = 3.0f;
    // Use this for initialization
    void Start()
    {
        UIcanvas.SetActive(false);
        videoPlayer.targetCameraAlpha = 0f;
        StartTransition();
    }
    // Update is called once per frame
    void Update()
    {
        if (isTransitioning)
        {
            // Check if the transition is still in progress
            if (currentTime < transitionDuration)
            {
                currentTime += Time.deltaTime;
                videoPlayer.targetCameraAlpha = Mathf.Lerp(0f, 1f, currentTime / transitionDuration);
            }
            else
            {
                // The transition is complete
                videoPlayer.targetCameraAlpha = 1f;
                isTransitioning = false;
                // Turn on the UI canvas
                UIcanvas.SetActive(true);
            }
        }
    }
    void SetActive()
    {
        UIcanvas.SetActive(true);
    }
    void StartTransition()
    {
        isTransitioning = true;
        currentTime = 0f;
    }
}

