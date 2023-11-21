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

    public  ShopInventorySO shopInventorySO;

    //const
    private const string PRICE_NAME = "Souls: ";

    [SerializeField] public Image[] skill_slots;
    [SerializeField] public TextMeshProUGUI[] price_slots;

    private void Awake()
    {
        ShopUI.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    private void Start()
    {
        DisplayRandomSkills();
        UpdatePrice();
        this.RegisterListener(EventID.OnRerolledShop, (param) => OnClickRerolled());
        this.RegisterListener(EventID.OnBuyingTurret, (param) => UpdatePrice());
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

    }

    private void OnClickRerolled()
    {
    
        DisplayRandomSkills();
        UpdatePrice();
    }

    private void DisplayRandomSkills()
    {
        List<int> selectedIndexes = new List<int>();
        int maxSlots = 3;

        for (int i = 0; i < maxSlots; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = UnityEngine.Random.Range(0, shopInventorySO.m_Inventory.Count);
            } while (selectedIndexes.Contains(randomIndex));

            selectedIndexes.Add(randomIndex);
            SkillObjectSO skillObject = shopInventorySO.m_Inventory[randomIndex];

            if (i < skill_slots.Length)
            {
                skill_slots[i].sprite = skillObject.image;
            }

            if (i < price_slots.Length)
            {
                price_slots[i].text = PRICE_NAME + skillObject.price.ToString();
            }
        }
    }
}