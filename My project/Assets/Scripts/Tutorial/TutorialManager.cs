using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LeakyAbstraction;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private Image m_imageShow;

    [SerializeField]
    private Sprite[] m_images;

    [SerializeField]
    private string m_playSceneName;

    private int m_currentImage;
    private int m_imageNumber;

    private void Start()
    {
        m_imageNumber = m_images.Length;
        m_currentImage = 0;

        m_imageShow.sprite = m_images[m_currentImage];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            m_currentImage++;

            if (m_currentImage >= m_imageNumber)
            {
                PlayGame();

                return;
            }

            m_imageShow.sprite = m_images[m_currentImage];

            SoundManager.Instance.PlaySound(GameSound.UIClick);
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (m_currentImage > 0) m_currentImage--;

            m_imageShow.sprite = m_images[m_currentImage];

            SoundManager.Instance.PlaySound(GameSound.UIClick);

        }
    }

    private void PlayGame()
    {
        SoundManager.Instance.PlaySound(GameSound.UIClick);

        LoadAsync.Instance.LoadScene(m_playSceneName);
    }
}
