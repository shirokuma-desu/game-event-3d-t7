using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellCooldown : MonoBehaviour
{
    [Header("UI items for Spell Cooldown")]
    [Tooltip("Tooltip example")]
    [SerializeField]
    private Image imageCooldown;
    [SerializeField]
    private TMP_Text textCooldown;
    [SerializeField]
    private Image imageEdge;

    //variable for looking after the cooldown
    private bool isCoolDown = false;
    private float cooldownTime = 10.0f;
    private float cooldownTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        textCooldown.gameObject.SetActive(false);
        imageEdge.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCoolDown && cooldownTimer <= 0f)
        {
            isCoolDown = false;

            textCooldown.gameObject.SetActive(false);
            imageEdge.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0.0f;
        }
        if (!isCoolDown && cooldownTimer > 0f)
        {
            isCoolDown = true;

            textCooldown.gameObject.SetActive(true);
            imageEdge.gameObject.SetActive(true);
        }

        ApplyCooldown();
    }

    public void SetCooldownTime(float _value)
    {
        cooldownTime = _value;
    }
    public void SetCooldownTimer(float _value)
    {
        cooldownTimer = _value;
    }

    void ApplyCooldown()
    {
        if (isCoolDown)
        {
            textCooldown.text = Mathf.RoundToInt(cooldownTimer).ToString();
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;

            imageEdge.transform.localEulerAngles = new Vector3(0, 0, 360.0f * (cooldownTimer / cooldownTime));
        }

    }
}
