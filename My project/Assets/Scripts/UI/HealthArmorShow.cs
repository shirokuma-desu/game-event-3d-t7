using UnityEditor.Rendering;
using UnityEngine;

public class HealthArmorShow : MonoBehaviour
{
    private int maxHealth;
    private int maxArmor;
    private int currentHealth;
    private int currentArmor;

    private UIManager m_UIMananger;

    private void Start()
    {
        m_UIMananger = UIManager.Instance;
    }

    private void Update()
    {
        maxHealth = GetComponent<BaseTurret>().maxHealth;
        maxArmor = GetComponent<BaseTurret>().maxArmor;

        currentHealth = GetComponent<BaseTurret>().currentHealth;
        currentArmor = GetComponent<BaseTurret>().currentArmor;

        UpdateHealthShow();
        UpdateArmorShow();
    }

    void UpdateHealthShow()
    {
        m_UIMananger.BaseTowerUI.SetHealthText(currentHealth.ToString() + "/" + maxHealth.ToString());
        m_UIMananger.BaseTowerUI.SetHealthImage((float)currentHealth/100);
    }

    void UpdateArmorShow()
    {
        m_UIMananger.BaseTowerUI.SetArmorText(currentArmor.ToString() + "/" + maxArmor.ToString());
        m_UIMananger.BaseTowerUI.SetArmorImage((float)currentArmor /100);
    }
}
