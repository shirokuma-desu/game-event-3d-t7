using UnityEngine;

public class Turret : MonoBehaviour
{
    public TurretSO turretStat;

    public bool isSettle = false;
    public bool isUpgrade = false;
    public bool isDead = false;

    public int health;
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;

    public int spotIndex;

    public Vector3 lockPos;

    private void Awake()
    {
        health = turretStat.health;
        attackDamage = turretStat.attackDamage;
        attackSpeed = turretStat.attackSpeed;
        attackRange = turretStat.attackRange;
    }

    private void Update()
    {
        isDead = health <= 0;
        if (isDead)
        {
            Die();
        }

        if (lockPos != Vector3.zero && isSettle)
        {
            transform.position = lockPos;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isSettle)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
            }

            if (health > turretStat.health)
            {
                health = turretStat.health;
            }
        }
    }

    void Die()
    {
        GameObject.Find("GameManager").GetComponent<TurretManager>().DeleteOccupied(spotIndex);
        GameObject.Find("GameManager").GetComponent<TurretManager>().turretSpots[spotIndex].GetComponent<TurretSpot>().isSettle = false;
        Destroy(gameObject);
    }
}