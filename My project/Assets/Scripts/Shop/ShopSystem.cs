using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private int m_total_souls = 1000;
    [SerializeField] private int m_reroll_price = 0;
    [SerializeField] private int m_skill_price;
    [SerializeField] private int m_price_to_increase;
    [SerializeField] private int m_price_turret = 50;



    public InventorySO shopInventorySO;
    public InventorySO playerInventorySO;

    public List<DataContainer> dataContainersSkill = new List<DataContainer>();

    #region get

    public int getTotalSouls()
    {
        return m_total_souls;
    }

    public int getRerollPrice()
    {
        return m_reroll_price;
    }


    #endregion

    #region init
    private void Awake()
    {
    }

    private void Start()
    {
    }
    #endregion

    #region method
    private int Sell(int soulToAdd)
    {
        m_total_souls += soulToAdd;
        return m_total_souls;
    }

    private int Buy(int soulToMinus)
    {
        m_total_souls -= soulToMinus;
        return m_total_souls;
    }


    public void DoReroll()
    {
        if (m_total_souls < m_reroll_price)
        {
            return;
        }
        Buy(m_reroll_price);
        m_price_to_increase = 10;
        m_reroll_price += m_price_to_increase;
        BindRandomData();
        //call event reroll
        this.PostEvent(EventID.OnRerolledShop);
    }


    public void DoBuySkill(DataContainer datacontainer)
    {
        SkillObjectSO skillObjectSO = datacontainer.Get();
        int price = skillObjectSO.price;
        m_total_souls -= price;

        skillObjectSO.is_upgraded = true;
        Debug.Log(skillObjectSO.name);
        this.PostEvent(EventID.OnBuyingItem);
    }

    public void BuyTurret()
    {
        if (m_total_souls < m_price_turret)
        {
            return;
        }
        Buy(m_price_turret);
        m_price_to_increase = 20;
        m_price_turret += m_price_to_increase;

        //call event buying
        this.PostEvent(EventID.OnBuyingTurret);

    }

    private void BindRandomData()
    {
        int[] randomindex = GenerateRandomNumbersArrays();
        for (int i = 0; i < dataContainersSkill.Count; i++)
        {

            dataContainersSkill[i].Set(shopInventorySO.m_Inventory[randomindex[i]]);
        }
    }

    private int[] GenerateRandomNumbersArrays()
    {
        System.Random random = new System.Random();
        int[] randomNumbers = new int[3];

        for (int i = 0; i < randomNumbers.Length; i++)
        {
            int randomNumber;
            // Lặp cho đến khi có một số ngẫu nhiên không trùng
            do
            {
                randomNumber = random.Next(0, maxValue: shopInventorySO.m_Inventory.Count); // Điều chỉnh phạm vi theo nhu cầu của bạn
            } while (Array.IndexOf(randomNumbers, randomNumber) != -1);

            randomNumbers[i] = randomNumber;
        }

        return randomNumbers;
    }
    #endregion




}