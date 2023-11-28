using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBaseTower : MonoBehaviour
{
    public TextMeshProUGUI m_healthText;
    public TextMeshProUGUI m_armorText;

    public void SetHealthText(string _text)
    {
        m_healthText.text = _text;
    }

    public void SetArmorText(string _text)
    {
        m_armorText.text = _text;
    }
}
