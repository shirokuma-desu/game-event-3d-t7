using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    private TurretUpgradedStat tus;

    [SerializeField]
    private GameObject[] m_turretSpots;
    [SerializeField]
    private bool[] m_isSpotsOccupied;
    [SerializeField]
    private bool m_emptySpotAvai = true;

    [SerializeField]
    private GameEvent m_buildANewTurret;
    [SerializeField]
    private GameEvent m_ATurretDestroyed;

    public GameObject[] TurretSpots { get { return m_turretSpots; } }
    public bool[] IsSpotsOccupied { get { return m_isSpotsOccupied; } }
    public bool EmptySpotAvai { get { return m_emptySpotAvai; } }
    public GameEvent BuildANewTurret { get { return m_buildANewTurret; } }
    public GameEvent ATurretDestroyed { get { return m_ATurretDestroyed; } }

    private void Awake()
    {
        tus = GetComponent<TurretUpgradedStat>();

        m_isSpotsOccupied = new bool[m_turretSpots.Length];

        for (int i = 0; i < m_isSpotsOccupied.Length; i++)
        {
            m_isSpotsOccupied[i] = false;
        }
    }

    private void Update()
    {
        int count = 0;
        for (int i = 0; i < m_isSpotsOccupied.Length; i++)
        {
            if (m_isSpotsOccupied[i])
            {
                count++;
            }
        }

        if (count == m_isSpotsOccupied.Length)
        {
            m_emptySpotAvai = false;
        }
        else
        {
            m_emptySpotAvai = true;
        }
    }

    public void IncreaseStat(string statName, float value)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Turret"))
            {
                Turret stat = collider.GetComponent<Turret>();
                switch (statName.ToLower())
                {
                    case "health":
                        float percentChange = (float)stat.CurrentHealth / (float)stat.Health;
                        stat.Health += (int)value;
                        stat.CurrentHealth = (int)(percentChange * stat.Health);
                        break;
                    case "attack damage":
                        stat.AttackDamage += (int)value;
                        break;
                    case "attack speed":
                        stat.AttackSpeed += value;
                        break;
                    case "attack range":
                        stat.AttackRange += value;
                        break;
                }
            }
        }

        switch (statName.ToLower())
        {
            case "health":
                tus.BonusHealth += (int)value;
                break;
            case "attack damage":
                tus.BonusAttackDamage += (int)value;
                break;
            case "attack speed":
                tus.BonusAttackSpeed += value;
                break;
            case "attack range":
                tus.BonusAttackRange += value;
                break;
        }
    }

    public void ChangeShootType(GameObject target, int type)
    {
        if (target != null)
        {
            target.GetComponent<HandleShooting>().ShootType = type;
        }
    }

    public void RestoreTurretStat(GameObject target)
    {
        if (target != null)
        {
            Turret stat = target.GetComponent<Turret>();

            stat.Health = tus.BonusHealth;
            float percentChange = (float)stat.CurrentHealth / (float)stat.Health;
            stat.CurrentHealth = (int)(stat.Health * percentChange);
            stat.AttackDamage = tus.BonusAttackDamage;
            stat.AttackSpeed = tus.BonusAttackSpeed;
            stat.AttackRange = tus.BonusAttackRange;
        }
        else
        {
            return;
        }
    }

    public void SetOccupied(int spotIndex)
    {
        m_isSpotsOccupied[spotIndex] = true;
    }

    public void DeleteOccupied(int spotIndex)
    {
        m_isSpotsOccupied[spotIndex] = false;
    }




    #region Bullet Spawner Manager
    [SerializeField]
    private List<BulletSpawner> m_spawners;

    public void StartAllSpawner(Vector3 _position)
    {
        foreach (BulletSpawner _spawner in m_spawners)
        {
            _spawner.SpawnBullet(_position);
        }
    }

    public Bullet StartSpawner(int _index, Vector3 _position)
    {
        return m_spawners[_index].SpawnBullet(_position);
    }

    private void Start()
    {
        foreach (BulletSpawner _spawner in m_spawners)
        {
            _spawner.Manager = this;
        }
    }
    #endregion
}
