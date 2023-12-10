using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    public int maxHealth;
    public int maxArmor;

    public int currentHealth;
    public int currentArmor;

    public float armorRecoveryTime = 20f;
    public int armorRecoveryAmount = 5;
    [SerializeField]
    private float currentArmorRecoveryTime;

    public bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        currentArmor = maxArmor;

        currentArmorRecoveryTime = armorRecoveryTime;
    }

    private void Update()
    {
        isDead = currentHealth <= 0f;

        if (currentArmor < maxArmor)
        {
            RecoveryArmor();
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentArmor > 0)
        {
            if (damage > currentArmor)
            {
                int leftDmg = damage - currentArmor;
                currentArmor = 0;
                currentHealth -= leftDmg;
            }
            else
            {
                currentArmor -= damage;
            }
        }
        else
        {
            if (damage > currentHealth)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth -= damage;
            }
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void RecoveryArmor()
    {
        if (currentArmorRecoveryTime <= 0f)
        {
            currentArmor += armorRecoveryAmount;
            currentArmorRecoveryTime = armorRecoveryTime;
        }
        else
        {
            currentArmorRecoveryTime -= Time.deltaTime;
        }
    }
}
