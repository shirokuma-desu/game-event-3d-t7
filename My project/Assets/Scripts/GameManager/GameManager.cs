using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    public enum State
    {
        Running,
        Paused,
        GameOver,
    }

    [Header("References")]
    [SerializeField]
    private EnemyManager m_enemyManager;
    public EnemyManager EnemyManager { get => m_enemyManager; }
    [SerializeField]
    private ShopSystem m_shop;
    public ShopSystem Shop { get => m_shop; }

    [SerializeField]
    private Image m_gameOverCover;
    
    private State m_gameState;
    public State GameState { get => m_gameState; }

    [SerializeField]
    private GameEvent m_gameOver;

    private float m_time;
    public float GameTime { get => m_time; }

    public void Start()
    {
        m_time = 0f;
    }

    public void Update()
    {
        m_time += Time.deltaTime;
    }

    public void PauseGame()
    {
        if (m_gameState == State.GameOver) 
        {
            Debug.LogWarning("GameManager: The game is over");
            return;
        }

        if (m_gameState == State.Running) 
        {
            Debug.LogWarning("GameManager: The game is already paused");
        }

        Time.timeScale = .1f;

        m_gameState = State.Paused;
    }

    public void ResumeGame()
    {
        if (m_gameState == State.GameOver) 
        {
            Debug.LogWarning("GameManager: The game is over");
            return;
        }

        if (m_gameState == State.Running) 
        {
            Debug.LogWarning("GameManager: The game is already running");
        }

        Time.timeScale = 1f;

        m_gameState = State.Running;
    }
    
    public void GameOver()
    {
        m_gameState = State.GameOver;

        Time.timeScale = .1f;

        StartCoroutine(BlackCoverScreen());
        StartCoroutine(GameOverZoomScreen());

        m_gameOver.RaiseEvent();
    }

    private IEnumerator BlackCoverScreen()
    {
        float _alpha = m_gameOverCover.color.a;
        while (_alpha < 1f)
        {
            _alpha += 1f / 30f;
            
            m_gameOverCover.color = new Color(0, 0, 0, _alpha);

            yield return new WaitForSeconds(.2f / 30f);
        }
    }
    private IEnumerator GameOverZoomScreen()
    {
        float _targetfov = 28f;
        float _orifov = Camera.main.fieldOfView;
        while (Camera.main.fieldOfView > _targetfov)
        {
            Camera.main.fieldOfView -= (_orifov - _targetfov) / 30f;

            yield return new WaitForSeconds(.2f / 30f);
        }

        yield return new WaitForSeconds(.1f);
        Time.timeScale = 1f;

        SceneManager.LoadScene("GameOverScene");
    }
}