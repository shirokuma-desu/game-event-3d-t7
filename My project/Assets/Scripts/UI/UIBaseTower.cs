using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIBaseTower : MonoBehaviour
{
    public TextMeshProUGUI m_healthText;
    public TextMeshProUGUI m_armorText;
    public Image image_Health;
    public Image image_Armor;
    public void SetHealthText(string _text)
    {
        m_healthText.text = _text;
    }

    public void SetArmorText(string _text)
    {
        m_armorText.text = _text;
    }

    public void SetHealthImage(float value){
        image_Health.fillAmount = value;
    }

    public void SetArmorImage(float value)
    {
        image_Armor.fillAmount = value;
    }

}
