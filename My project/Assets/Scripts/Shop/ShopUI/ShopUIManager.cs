using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public GameObject shop_ui;
    public TextMeshProUGUI total_souls;
    public TextMeshProUGUI reroll_price;
    public TextMeshProUGUI buy_turret_price;
    public TextMeshProUGUI open_button_text;
    public GameObject open_button;
   
    public ShopSystem shopSystem;

    [Header("Game Event")]
    [SerializeField]
    private GameEvent m_openShop;
    [SerializeField]
    private GameEvent m_closeShop;

    [SerializeField] public Button[] skill_buttons;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
        shop_ui.gameObject.SetActive(false);
        UpdatePrice();

        this.RegisterListener(EventID.OnBuyingTurret, (param) => OnClickBuyTurret());
        this.RegisterListener(EventID.OnBuyingItem, (param) => OnClickBuyItem());
        this.RegisterListener(EventID.OnReroll, (param) => OnClickRerolled());
        this.RegisterListener(EventID.OnSellingItem, (param) => UpdatePrice());
        this.RegisterListener(EventID.OnBuyLimitSkill, (param) => showButton());
        this.RegisterListener(EventID.OnBuyUpgradeTurret, (param) => UpdatePrice());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Show()
    {
        shop_ui.gameObject.SetActive(true);
        open_button.gameObject.SetActive(false);

        m_openShop.RaiseEvent();
    }

    public void Close()
    {
        shop_ui.gameObject.SetActive(false);
        open_button.gameObject.SetActive(true);

        m_closeShop.RaiseEvent();
    }

    public void UpdatePrice()
    {
        total_souls.text        =  shopSystem.getTotalSouls().ToString();
        reroll_price.text       =  shopSystem.getRerollPrice().ToString();
        open_button_text.text   =  shopSystem.getTotalSouls().ToString();
        buy_turret_price.text   =  shopSystem.getTotalTurretPrice().ToString();
    }

    private void OnClickBuyTurret()
    {
        UpdatePrice();
    }

    private void OnClickBuyItem()
    {
        foreach (var item in skill_buttons)
        {
            item.interactable = false;
        }
        UpdatePrice();
    }

    private void showButton()
    {
        foreach (var item in skill_buttons)
        {
            item.interactable = true;
        }
    }

    private void OnClickRerolled()
    {
        UpdatePrice();
        foreach (var item in skill_buttons)
        {
            item.interactable = true;
        }
    }
}