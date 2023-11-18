using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public GameObject ShopUI;
    public TextMeshProUGUI totalsouls;
    public TextMeshProUGUI reroll_price;
    public Button rerollbutton;

    private ShopSystem shopSystem;

    [SerializeField] public Image[] skill_slot;

    private bool m_isOn ;

    private void Awake()
    {
        shopSystem = GetComponent<ShopSystem>();
        ShopUI.gameObject.SetActive(false);
        m_isOn = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateVisual();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Show()
    {
        if (m_isOn)
        {
            ShopUI.gameObject.SetActive(false);
            m_isOn = false;
        }
        else
        {
            ShopUI.gameObject.SetActive(true);
            m_isOn = true;
        }
    }

    public void UpdateVisual()
    {
        totalsouls.text = "Souls: " + shopSystem.getTotalSouls().ToString();
        reroll_price.text = shopSystem.getRerollPrice().ToString();
    }


}
