using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    private State m_gameState;
    public State GameState { get => m_gameState; }

    [SerializeField]
    private GameEvent m_gameOver;

    public void PauseGame()
    {
        if (m_gameState == State.Running) 
        {
            Debug.LogWarning("GameManager: The game is already paused");
        }

        m_gameState = State.Paused;
    }

    public void ResumeGame()
    {
        if (m_gameState == State.Running) 
        {
            Debug.LogWarning("GameManager: The game is already running");
        }

        m_gameState = State.Running;
    }
    
    public void GameOver()
    {
        m_gameState = State.GameOver;
    }
}