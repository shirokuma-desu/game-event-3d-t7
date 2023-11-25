using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    //data
    [SerializeField] private int m_total_souls = 1000;
    [SerializeField] private int m_reroll_price = 0;
    [SerializeField] private int m_price_to_increase;
    [SerializeField] private int m_price_turret = 50;

    //scriptableobject
    public InventorySO shopInventorySO;

    public InventorySO playerInventorySO;

    //datacontainer
    public List<DataContainer> dataContainersSkill = new List<DataContainer>();

    public List<SellDataContainer> dataContainerSellItem = new List<SellDataContainer>();

    #region get

    public int getTotalSouls()
    {
        return m_total_souls;
    }

    public int getRerollPrice()
    {
        return m_reroll_price;
    }

    #endregion get

    #region init

    private void Awake()
    {
    }

    private void Start()
    {
        if(playerInventorySO.m_Inventory.Count > 0)
        {
            loadDataFromPlayerInventory();
        }
    }

    #endregion init

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

    public void doReroll()
    {
        if (m_total_souls < m_reroll_price)
        {
            return;
        }
        Buy(m_reroll_price);
        m_price_to_increase = 10;
        m_reroll_price += m_price_to_increase;
        bindRandomData();
        //call event reroll
        this.PostEvent(EventID.OnRerolledShop);
    }

    public void doBuySkill(DataContainer datacontainer)
    {
        SkillObjectSO skillObjectSO = datacontainer.Get();

        // player cant not buy empty slot 
        if (skillObjectSO.ID_Skill == 0)
        {
            Debug.Log("Cant allow to buy empty slot");
            return;
        }
        // player can buy item in slot
        else
        {
            addOrUpdate(skillObjectSO);
            loadDataFromPlayerInventory();
            this.PostEvent(EventID.OnBuyingItem);
        }
    }

    public void buyTurret()
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

    private void bindRandomData()
    {
        int[] randomindex = generateRandomNumbersArrays();
        for (int i = 0; i < dataContainersSkill.Count; i++)
        {
            dataContainersSkill[i].Set(shopInventorySO.m_Inventory[randomindex[i]]);
        }
    }

    private void addOrUpdate(SkillObjectSO skillObjectSO)
    {

        if (playerInventorySO.m_Inventory.Count < 3)
        {
            SkillObjectSO existingSkill = playerInventorySO.m_Inventory.Find(skill => skill.ID_Skill == skillObjectSO.ID_Skill);
            if (existingSkill != null)
            {
                int index = playerInventorySO.m_Inventory.IndexOf(existingSkill);
                playerInventorySO.m_Inventory[index] = skillObjectSO;   
                Debug.Log(skillObjectSO.name + " update");
                onUpdateSkillPrice(skillObjectSO);
            }
            else
            {
                playerInventorySO.m_Inventory.Add(skillObjectSO);
                onUpdateSkillPrice(skillObjectSO);
                Debug.Log(skillObjectSO.name + " add new ");
            }
        }
        else
        {
            SkillObjectSO existingSkill = playerInventorySO.m_Inventory.Find(skill => skill.ID_Skill == skillObjectSO.ID_Skill);
            if (existingSkill != null)
            {
                int index = playerInventorySO.m_Inventory.IndexOf(existingSkill);
                playerInventorySO.m_Inventory[index] = skillObjectSO;
                Debug.Log(skillObjectSO.name + " update ");
                onUpdateSkillPrice(skillObjectSO);
            }
            else
            {
                Debug.Log("Cannot add more skills. Already have 3 skills.");
            }
        }

    }

    private void loadDataFromPlayerInventory()
    {
      for(int i = 0; i < playerInventorySO.m_Inventory.Count; i++)
        {
                dataContainerSellItem[i].Set(playerInventorySO.m_Inventory[i]);
        }
    }

    private int[] generateRandomNumbersArrays()
    {
        System.Random random = new System.Random();
        int[] randomNumbers = new int[3];

        for (int i = 0; i < randomNumbers.Length; i++)
        {
            int randomNumber;
            do
            {
                randomNumber = random.Next(0, maxValue: shopInventorySO.m_Inventory.Count); 
            } while (Array.IndexOf(randomNumbers, randomNumber) != -1);

            randomNumbers[i] = randomNumber;
        }

        return randomNumbers;
    }
    
    private void onUpdateSkillPrice(SkillObjectSO skillObjectSO)
    {
        int price = skillObjectSO.price;
        if (m_total_souls < price)
        {
            return;
        }
        m_total_souls -= price;
        skillObjectSO.price += skillObjectSO.price_increasement;
        skillObjectSO.is_upgraded = true;
        skillObjectSO.level_skill++;
        skillObjectSO.sellprice += 2;
    }
   

    #endregion method
}