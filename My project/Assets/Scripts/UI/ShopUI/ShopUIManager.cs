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
    public TextMeshProUGUI total_souls;
    public TextMeshProUGUI reroll_price;
    public GameObject open_button;

    private ShopSystem shopSystem;

    //const
    private const string PRICE_NAME = "Souls: ";

    [SerializeField] public Image[] skill_slot;


    private void Awake()
    {
        shopSystem = GetComponent<ShopSystem>();
        ShopUI.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateVisual();
        this.RegisterListener(EventID.OnRerolledShop, (param) => OnClickRerolled());
    }

    // Update is called once per frame
    void Update()
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

    private void UpdateVisual()
    {
        //update text
        total_souls.text = PRICE_NAME + shopSystem.getTotalSouls().ToString();
        reroll_price.text = shopSystem.getRerollPrice().ToString();
    }


    private void OnClickRerolled()
    {
        UpdateVisual();
    }


}
