using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] private int m_total_souls = 100;
    [SerializeField] private int m_reroll_price = 0;
    [SerializeField] private int m_price = 10;
    [SerializeField] private int m_skill_price;

 


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

    private int IncreasePrice(int priceToIncrease)
    {
        m_reroll_price += priceToIncrease;
        return m_reroll_price;
    }
   

    public void DoReroll()
    {
        if (m_total_souls < m_reroll_price)
        {
            return;
        }
        Buy(m_reroll_price);
        IncreasePrice(m_price);

        //call event reroll
        this.PostEvent(EventID.OnRerolledShop);
    }

    #endregion
    

}