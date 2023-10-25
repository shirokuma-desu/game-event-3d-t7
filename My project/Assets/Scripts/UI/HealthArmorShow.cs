using TMPro;
using UnityEngine;

public class HealthArmorShow : MonoBehaviour
{
    public GameObject baseTower;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI armorText;

    private int maxHealth;
    private int maxArmor;
    private int currentHealth;
    private int currentArmor;

    private void Update()
    {
        maxHealth = baseTower.GetComponent<BaseTurret>().maxHealth;
        maxArmor = baseTower.GetComponent<BaseTurret>().maxArmor;

        currentHealth = baseTower.GetComponent<BaseTurret>().currentHealth;
        currentArmor = baseTower.GetComponent<BaseTurret>().currentArmor;

        UpdateHealthShow();
        UpdateArmorShow();
    }

    void UpdateHealthShow()
    {
        healthText.text = "Health: " + currentHealth.ToString() + "/" + maxArmor.ToString();
    }

    void UpdateArmorShow()
    {
        armorText.text = "Armor: " + currentArmor.ToString() + "/" + maxArmor.ToString();
    }
}
