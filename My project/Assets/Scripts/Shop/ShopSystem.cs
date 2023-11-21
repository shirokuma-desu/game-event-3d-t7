using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private int m_total_souls = 1000;
    [SerializeField] private int m_reroll_price = 0;
    [SerializeField] private int m_skill_price;
    [SerializeField] private int m_price_to_increase;
    [SerializeField] private int m_price_turret = 50;

 


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
        //call event reroll
        this.PostEvent(EventID.OnRerolledShop);
    }


    public void DoBuySkill(SkillObjectSO skillObjectSO)
    {
        Buy(skillObjectSO.price);
        skillObjectSO.price++;
        skillObjectSO.is_upgraded = true;
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
    #endregion


    

}