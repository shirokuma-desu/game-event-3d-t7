using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSystem : MonoBehaviour
{
    //data
    [SerializeField] private int m_total_souls          =   1000;
    [SerializeField] private int m_reroll_price         =   0;
    [SerializeField] private int m_price_to_increase    =   10;
    [SerializeField] private int m_price_turret         =   50;

    //scriptableobject
    public InventorySO  shop_inventory;
    public ItemData     emptySlot;
    public InventorySO  player_inventory_skillSO;
    public InventorySO  default_data_item_inventory;

    //datacontainer
    public List<DataContainer>      skills_container_buy    = new List<DataContainer>();
    public List<DataContainer>      turrets_container_buy   = new List<DataContainer>();
    public List<SellDataContainer>  skills_container_sell   = new List<SellDataContainer>();

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
        bindDataFromPlayerInventory();
    }

    #endregion init

    #region call event on UI

    public void doReroll()
    {
        if (m_total_souls < m_reroll_price)
        {
            return;
        }
        Buy(m_reroll_price);
        m_reroll_price += m_price_to_increase;
        bindRandomData();

        //call event reroll
        this.PostEvent(EventID.OnRerolledShop);
    }

    public void doBuySkill(DataContainer datacontainer)
    {
        ItemData skillObjectSO = datacontainer.Get();

        // player cant not buy empty slot 
        if (skillObjectSO.ID_Skill == 0)
        {
            Debug.Log("Cant allow to buy empty slot");
            return;
        }
        if(player_inventory_skillSO.m_Inventory_Skill.Count == 3)
        {
            Debug.Log("Cant allow to buy 4 skills");
            return;
        }
        // player can buy item in slot
        else
        {
            addOrUpdate(skillObjectSO);
            datacontainer.Set(emptySlot);
            bindDataFromPlayerInventory();
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

    public void sellSkill(SellDataContainer dataconainter) {
        ItemData skillObjectSO = dataconainter.Get();
        if(skillObjectSO.ID_Skill == 0)
        {
            Debug.Log("Cant allow to sell empty slot");
            return;
        }
        else
        {

            if(player_inventory_skillSO.m_Inventory_Skill.Count > 0)
            {
                ItemData delete_skill_object  =  player_inventory_skillSO.m_Inventory_Skill.Find(skill => skill.ID_Skill == skillObjectSO.ID_Skill);
                ItemData default_skill_object =  default_data_item_inventory.m_Inventory_Skill.Find(skill => skill.ID_Skill == skillObjectSO.ID_Skill);
                if (delete_skill_object != null && default_skill_object!= null)
                {
                    Sell(skillObjectSO.sellprice);
                    for(int i = 0; i < shop_inventory.m_Inventory_Skill.Count; i++)
                    {
                        if (shop_inventory.m_Inventory_Skill[i].ID_Skill == skillObjectSO.ID_Skill)
                        {
                            shop_inventory.m_Inventory_Skill[i].is_upgraded  =   default_skill_object.is_upgraded;
                            shop_inventory.m_Inventory_Skill[i].price        =   default_skill_object.price;
                            shop_inventory.m_Inventory_Skill[i].sellprice    =   default_skill_object.sellprice;
                            shop_inventory.m_Inventory_Skill[i].level_skill  =   default_skill_object.level_skill;
                        }
                    }
                    
                    int index = player_inventory_skillSO.m_Inventory_Skill.IndexOf(delete_skill_object);
                    player_inventory_skillSO.m_Inventory_Skill.RemoveAt(index);

                    dataconainter.Set(emptySlot);
                    //call event
                    this.PostEvent(EventID.OnSellingItem);
                }
            }
        }

    }
    #endregion

    #region logic method
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
    private void bindRandomData()
    {
        int[] randomindex = generateRandomNumbersArrays();
        for (int i = 0; i < skills_container_buy.Count; i++)
        {
            skills_container_buy[i]     .Set(shop_inventory    .m_Inventory_Skill[randomindex[i]]);
            turrets_container_buy[i]    .Set(shop_inventory    .m_Inventory_Turret[randomindex[i]]);
        }
    }

    private void addOrUpdate(ItemData skillObjectSO)
    {

        if (player_inventory_skillSO.m_Inventory_Skill.Count < 3)
        {
            ItemData existingSkill = player_inventory_skillSO.m_Inventory_Skill.Find(skill => skill.ID_Skill == skillObjectSO.ID_Skill);
            if (existingSkill != null)
            {
                int index = player_inventory_skillSO.m_Inventory_Skill.IndexOf(existingSkill);
                player_inventory_skillSO.m_Inventory_Skill[index] = skillObjectSO;   
                Debug.Log(skillObjectSO.name + " update");
                onUpdateSkillPrice(skillObjectSO);
            }
            else
            {
                player_inventory_skillSO.m_Inventory_Skill.Add(skillObjectSO);
                onUpdateSkillPrice(skillObjectSO);
                Debug.Log(skillObjectSO.name + " add new ");
            }
        }
        else
        {
            ItemData existingSkill = player_inventory_skillSO.m_Inventory_Skill.Find(skill => skill.ID_Skill == skillObjectSO.ID_Skill);
            if (existingSkill != null)
            {
                int index = player_inventory_skillSO.m_Inventory_Skill.IndexOf(existingSkill);
                player_inventory_skillSO.m_Inventory_Skill[index] = skillObjectSO;
                Debug.Log(skillObjectSO.name + " update ");
                onUpdateSkillPrice(skillObjectSO);
            }
            else
            {
                Debug.Log("Cannot add more skills. Already have 3 skills.");
            }
        }

    }

    private void bindDataFromPlayerInventory()
    {
        if (player_inventory_skillSO.m_Inventory_Skill.Count > 0)
        {
            for (int i = 0; i < player_inventory_skillSO.m_Inventory_Skill.Count; i++)
            {
                skills_container_sell[i].Set(player_inventory_skillSO.m_Inventory_Skill[i]);
            }

            HashSet<int> encounteredSkillIDs = new();

            for (int i = 0; i < skills_container_sell.Count; i++)
            {
                if (encounteredSkillIDs.Contains(skills_container_sell[i].m_datacontain.ID_Skill))
                {
                    skills_container_sell[i].Set(emptySlot);
                }
                else
                {
                    encounteredSkillIDs.Add(skills_container_sell[i].m_datacontain.ID_Skill);
                }
            }
        }
        else
        {
            foreach(var item in skills_container_sell)
            {
                item.Set(emptySlot);
            }
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
                randomNumber = random.Next(0, maxValue: shop_inventory.m_Inventory_Skill.Count); 
            } while (Array.IndexOf(randomNumbers, randomNumber) != -1);

            randomNumbers[i] = randomNumber;
        }

        return randomNumbers;
    }
    
    private void onUpdateSkillPrice(ItemData skillObjectSO)
    {
        int price = skillObjectSO.price;
        if (m_total_souls < price)
        {
            return;
        }
        m_total_souls -= price;
        skillObjectSO.price += skillObjectSO.price_increase;
        skillObjectSO.is_upgraded = true;
        skillObjectSO.level_skill++;
        skillObjectSO.sellprice += skillObjectSO.sell_price_increase;
    }
   

    #endregion method
}