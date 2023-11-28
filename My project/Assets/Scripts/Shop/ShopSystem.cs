using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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
    public ItemDataSO   emptySlot;
    public InventorySO  player_inventory_SO;
    public InventorySO  default_data_item_inventory;

    //datacontainer
    public List<DataContainer>      skills_container_buy    = new List<DataContainer>();
    public List<DataContainer>      turrets_container_buy   = new List<DataContainer>();
    public List<SellDataContainer>  skills_container_sell   = new List<SellDataContainer>();

    [Header("Game Events")]
    [SerializeField]
    private GameEvent m_buyASkill;
    [SerializeField]
    private GameEvent m_sellASkill;
    [SerializeField]
    private GameEvent m_buyATurret;

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
        bindDataFromPlayerInventorySO();
    }

    private void Start()
    {
        
        this.RegisterListener(EventID.OnSellingItem, (param) => bindDataFromPlayerInventorySO());
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
        this.PostEvent(EventID.OnReroll);
    }

    public void doBuySkill(DataContainer dataContainer)
    {
        ItemDataSO skillObjectSO = dataContainer.Get();
        // player cant not buy empty slot 
        if (skillObjectSO.ID_Skill == 0)
        {
            Debug.Log("Cant allow to buy empty slot");
            return;
        }
        // player can buy item in slot
        {
            addOrUpdate(player_inventory_SO.m_Inventory_Skill, skillObjectSO,dataContainer);
            bindDataFromPlayerInventorySO();
            this.PostEvent(EventID.OnBuyingItem);

        }
    }

    public void doBuyUpgrade(DataContainer dataContainer)
    {
        ItemDataSO itemData = dataContainer.Get();
        if(itemData.ID_Skill == 0)
        {
            Debug.Log("Cant allow to buy empty slot");
            return;
        }
        {
            ItemDataSO existingSkill = player_inventory_SO.m_Inventory_Turret.Find(skill => skill.ID_Skill == itemData.ID_Skill);
            int index = player_inventory_SO.m_Inventory_Turret.IndexOf(existingSkill);

            if(existingSkill != null)
            {
                player_inventory_SO.m_Inventory_Turret[index] = itemData;
                onUpdateSkillPrice(itemData);
                dataContainer.Set(emptySlot);
                this.PostEvent(EventID.OnBuyUpgradeTurret);
            }
            else
            {
                player_inventory_SO.m_Inventory_Turret.Add(itemData);
                onUpdateSkillPrice(itemData);
                dataContainer.Set(emptySlot);
                Debug.Log(itemData.name + " add new ");
                this.PostEvent(EventID.OnBuyUpgradeTurret);
            }
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
        m_buyATurret.RaiseEvent();
    }

    public void sellSkill(SellDataContainer dataconainter) {
        ItemDataSO skillObjectSO = dataconainter.Get();
        if(skillObjectSO.ID_Skill == 0)
        {
            Debug.Log("Cant allow to sell empty slot");
            return;
        }
        else
        {

            if(player_inventory_SO.m_Inventory_Skill.Count > 0)
            {
                ItemDataSO delete_skill_object  =  player_inventory_SO.m_Inventory_Skill.Find(skill => skill.ID_Skill == skillObjectSO.ID_Skill);
                ItemDataSO default_skill_object =  default_data_item_inventory.m_Inventory_Skill.Find(skill => skill.ID_Skill == skillObjectSO.ID_Skill);
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
                    
                    int index = player_inventory_SO.m_Inventory_Skill.IndexOf(delete_skill_object);
                    player_inventory_SO.m_Inventory_Skill.RemoveAt(index);

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
        int size_turrets    =   shop_inventory.m_Inventory_Turret.Count;
        int size_skills     =   shop_inventory.m_Inventory_Skill.Count;

        int[] arr_skills_index  = generateRandomNumbersArrays(size_skills);
        int[] arr_turrets_index = generateRandomNumbersArrays(size_turrets);

        for (int i = 0; i < skills_container_buy.Count; i++)
        {
            skills_container_buy[i]     .Set(shop_inventory    .m_Inventory_Skill[arr_skills_index[i]]);
            turrets_container_buy[i]    .Set(shop_inventory    .m_Inventory_Turret[arr_turrets_index[i]]);
        }
    }

    private void addOrUpdate(List<ItemDataSO> playerInventory,ItemDataSO itemData,DataContainer dataContainer)
    {
        ItemDataSO existingSkill = playerInventory.Find(skill => skill.ID_Skill == itemData.ID_Skill);
        int index = playerInventory.IndexOf(existingSkill);

        if (playerInventory.Count < 3)
        {
            if (existingSkill != null)
            {
                playerInventory[index] = itemData;
                onUpdateSkillPrice(itemData);
                dataContainer.Set(emptySlot);
                //call event
                Debug.Log(itemData.name + " update");
               
            }
            else
            {
                playerInventory.Add(itemData);
                onUpdateSkillPrice(itemData);
                dataContainer.Set(emptySlot);
                Debug.Log(itemData.name + " add new ");
             
            }
        }
        else
        {
            if (existingSkill != null)
            {
                playerInventory[index] = itemData;
                onUpdateSkillPrice(itemData);
                dataContainer.Set(emptySlot);

                Debug.Log(itemData.name + " update ");
               
            }
            else
            {
                this.PostEvent(EventID.OnBuyLimitSkill);
                Debug.Log("cant not add 4th skill");
            }
        }

    }

    private void bindDataFromPlayerInventorySO()
    {
        if (player_inventory_SO.m_Inventory_Skill.Count > 0)
        {
            for (int i = 0; i < player_inventory_SO.m_Inventory_Skill.Count; i++)
            {
                skills_container_sell[i].Set(player_inventory_SO.m_Inventory_Skill[i]);
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

    private void clearInventory(List<ItemDataSO> anyInventory)
    {
       anyInventory.Clear();
    }

    private int[] generateRandomNumbersArrays(int size)
    {
        System.Random random = new System.Random();
        int[] randomNumbers = new int[3];

        for (int i = 0; i < randomNumbers.Length; i++)
        {
            int randomNumber;
            do
            {
                randomNumber = random.Next(0, maxValue: size); 
            } while (Array.IndexOf(randomNumbers, randomNumber) != -1);

            randomNumbers[i] = randomNumber;
        }

        return randomNumbers;
    }
    
    private void onUpdateSkillPrice(ItemDataSO skillObjectSO)
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