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
    public GameObject ShopUI;
    public TextMeshProUGUI total_souls;
    public TextMeshProUGUI reroll_price;
    public GameObject open_button;

    public ShopSystem shopSystem;

    public  InventorySO shopInventorySO;

    //const
    private const string PRICE_NAME = "Souls: ";

    [SerializeField] public TextMeshProUGUI[] price_slots;
    [SerializeField] public Button[] skill_buttons;

    private void Awake()
    {
        ShopUI.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    private void Start()
    {
        UpdatePrice();

        this.RegisterListener(EventID.OnBuyingTurret, (param) => OnClickBuyTurret());
        this.RegisterListener(EventID.OnBuyingItem, (param) => OnClickBuyItem());
        this.RegisterListener(EventID.OnRerolledShop, (param) => OnClickRerolled());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Show()
    {
        ShopUI.gameObject.SetActive(true);
        open_button.gameObject.SetActive(false);
    }

    public void Close()
    {
        ShopUI.gameObject.SetActive(false);
        open_button.gameObject.SetActive(true);
    }

    private void UpdatePrice()
    {
        total_souls.text = PRICE_NAME + shopSystem.getTotalSouls().ToString();
        reroll_price.text = shopSystem.getRerollPrice().ToString();
    }

    private void OnClickBuyTurret()
    {
        UpdatePrice();
    }

    private void OnClickBuyItem()
    {

        foreach(var item in skill_buttons)
        {
            item.interactable = false;
        }
        UpdatePrice();
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