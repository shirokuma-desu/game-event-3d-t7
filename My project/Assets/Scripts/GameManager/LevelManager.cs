using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Serializable]
    private class EnemySpawnData
    {
        public string Name;
        public EnemySpawner Spawner;
        public float StartSpawnTime;
        public float StartInterval;
        public float IntervalDecrease;
        public float IntervalTerminal;
        public float SpawnProbability;
        public float ProbabilityIncrease;
        public float ProbabilityTerminal;
    }
    
    [SerializeField]
    private EnemySpawnData[] m_spawmData;

    private bool[] m_isSpawning;

    private void Start()
    {
        m_isSpawning = new bool[m_spawmData.Length];
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < m_spawmData.Length; i++)
        {
            EnemySpawnData _spawnData = m_spawmData[i];
            if (!m_isSpawning[i] && Time.time >= _spawnData.StartSpawnTime)
            {
                m_isSpawning[i] = true;

                StartSpawning(_spawnData);

                StartCoroutine(DeceaseIntervalCountdown(_spawnData, 60f));
                StartCoroutine(InceaseProbabilityCountdown(_spawnData, 60f));
            }
        }             
    }

    private void StartSpawning(EnemySpawnData _spawnData)
    {
        _spawnData.Spawner.StartSpawning();
        _spawnData.Spawner.SetSpawningInterval(_spawnData.StartInterval); 
        _spawnData.Spawner.SetSpawningProbability(_spawnData.SpawnProbability); 
    }

    private IEnumerator DeceaseIntervalCountdown(EnemySpawnData _spawnData, float _time)
    {
        yield return new WaitForSeconds(_time);

        _spawnData.Spawner.ChangeSpawningInterval(-_spawnData.IntervalDecrease);

        if (_spawnData.Spawner.SpawningInterval < _spawnData.IntervalTerminal)
        {
            _spawnData.Spawner.SetSpawningInterval(_spawnData.IntervalTerminal);

            yield return null;
        }

        StartCoroutine(DeceaseIntervalCountdown(_spawnData, 60f));
    }

    private IEnumerator InceaseProbabilityCountdown(EnemySpawnData _spawnData, float _time)
    {
        yield return new WaitForSeconds(_time);
        
        _spawnData.Spawner.ChangeSpawningProbability(_spawnData.ProbabilityIncrease);

        if (_spawnData.Spawner.SpawningProbability < _spawnData.ProbabilityTerminal)
        {
            _spawnData.Spawner.SetSpawningProbability(_spawnData.ProbabilityTerminal);

            yield return null;
        }

        StartCoroutine(InceaseProbabilityCountdown(_spawnData, 60f));
    }
}
