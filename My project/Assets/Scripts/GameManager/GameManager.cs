using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LeakyAbstraction;

public class GameManager : GenericSingleton<GameManager>
{
    public enum State
    {
        Running,
        Paused,
        GameOver,
        Victory,
    }

    [Header("References")]
    [SerializeField]
    private EnemyManager m_enemyManager;
    public EnemyManager EnemyManager { get => m_enemyManager; }
    [SerializeField]
    private ShopSystem m_shop;
    public ShopSystem Shop { get => m_shop; }
    [SerializeField]
    private TurretManager m_turretManager;
    public TurretManager TurretManager { get => m_turretManager; }

    [SerializeField]
    private Image m_gameOverCover;
    
    private State m_gameState;
    public State GameState { get => m_gameState; }

    [SerializeField]
    private GameEvent m_gameOver;

    private float m_time;
    public float GameTime { get => m_time; }

    [SerializeField]
    private TimeScore m_timeScoreSave;

    [SerializeField]
    private GameObject m_endShockwave;

    [SerializeField]
    private int m_maxTime;
    public int MaxTime { get => m_maxTime; }

    public void Start()
    {
        m_time = 0f;

        ResumeGame();

        m_shop.resetShop();
        UIManager.Instance.SkillUI.UpdateInventorySkillUI();
        m_shop.ResetInventoryUIEmpty();
    }

    public void Update()
    {
        m_time += Time.deltaTime;

        if (m_time > m_maxTime)
        {
            Victory();
        }
    }

    public void PauseGame()
    {
        if (m_gameState == State.GameOver || m_gameState == State.Victory) 
        {
            Debug.LogWarning("GameManager: The game is over");
            return;
        }

        if (m_gameState == State.Running) 
        {
            Debug.LogWarning("GameManager: The game is already paused");
        }

        Time.timeScale = .01f;

        m_gameState = State.Paused;
    }

    public void ResumeGame()
    {
        if (m_gameState == State.GameOver || m_gameState == State.Victory) 
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
        if (m_gameState == State.GameOver) return;
        if (m_gameState == State.Victory) return;

        m_gameState = State.GameOver;

        Time.timeScale = .1f;

        SoundManager.Instance.PlaySound(GameSound.Rizz);

        SaveScore();

        m_gameOver.RaiseEvent();

        StartCoroutine(BlackCoverScreen());
        StartCoroutine(GameOverZoomScreen());
    }

    public void Victory()
    {
        if (m_gameState == State.GameOver) return;
        if (m_gameState == State.Victory) return;

        m_gameState = State.Victory;

        Time.timeScale = .1f;

        Instantiate(m_endShockwave, Vector3.up * .1f, Quaternion.identity);

        EnemyManager.StopAllSpawner();
        foreach (Enemy _enemy in EnemyManager.GetEnemies())
        {
            _enemy.Kill();
        }

        UIManager.Instance.ShopUI.Close();

        SoundManager.Instance.PlaySound(GameSound.MeteorImpact);

        SaveScore();

        StartCoroutine(BlackCoverScreen());
        StartCoroutine(GameOverZoomScreen());
    }

    private void SaveScore()
    {
        m_timeScoreSave.CurrentTime = MaxTime - GameTime;
        if (m_timeScoreSave.BestTime > m_timeScoreSave.CurrentTime) 
            m_timeScoreSave.BestTime = m_timeScoreSave.CurrentTime;
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