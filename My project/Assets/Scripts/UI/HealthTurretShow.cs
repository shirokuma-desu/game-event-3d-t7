using UnityEngine;
using UnityEngine.UI;

public class HealthTurretShow : MonoBehaviour
{
    public Slider health;
    public Turret turretStat;

    private int maxHealth;
    private int currentHealth;

    private void Awake()
    {
        turretStat = GetComponentInParent<Turret>();
        health = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        maxHealth = turretStat.turretStat.health;
        currentHealth = turretStat.health;

        health.maxValue = maxHealth;
        health.minValue = 0f;

        health.value = currentHealth;

        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
