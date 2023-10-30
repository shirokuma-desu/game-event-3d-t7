using UnityEngine;

public class Turret : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public bool isSettle = false;
    public bool isUpgrade = false;
    public bool isDead = false;

    public int attackDamage = 20;
    public float attackSpeed = 1f;
    public float attackRange = 15f;

    public int spotIndex;

    public Vector3 lockPos;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        isDead = currentHealth <= 0;
        if (isDead)
        {
            Die();
        }

        if (lockPos != Vector3.zero && isSettle)
        {
            transform.position = lockPos;
        }
    }

    public void TakeDamage(int health)
    {
        if (isSettle)
        {
            currentHealth -= health;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
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