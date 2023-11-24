using UnityEngine;
using UnityEngine.UI;

public class HealthTurretShow : MonoBehaviour
{
    private Slider health;
    private Turret ts;

    private int maxHealth;
    private int currentHealth;

    private void Awake()
    {
        ts = GetComponentInParent<Turret>();
        health = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        maxHealth = ts.Health;
        currentHealth = ts.CurrentHealth;

        health.maxValue = maxHealth;
        health.minValue = 0f;

        health.value = currentHealth;

        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
