using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{

    [SerializeField] private int m_total_souls = 100;
    [SerializeField] private int m_reroll_price = 0;
    [SerializeField] private int m_price = 10;

    [SerializeField] public Sprite[] m_skill_sprites = null;
    [SerializeField] public Sprite[] m_skill_sprites_upgrade = null;

    private ShopUIManager shopUIManager;

    public int getTotalSouls()
    {
        return m_total_souls;
    }

    public int getRerollPrice()
    {
        return m_reroll_price;
    }

    private void Awake()
    {
        shopUIManager = GetComponent<ShopUIManager>();
    }
    private void Start()
    {
    }

    public int Sell(int soulToAdd)
    {
        m_total_souls += soulToAdd;
        return m_total_souls;
    }

    public int Buy(int soulToMinus)
    {
        m_total_souls -= soulToMinus;
        return m_total_souls;   
    }

    private int IncreasePrice(int priceToIncrease)
    {
        m_reroll_price += priceToIncrease;
        return m_reroll_price;
    }

    public void Reroll()
    {
        if (m_total_souls < m_reroll_price)
        {
            return;
        }
        m_total_souls = m_total_souls - m_reroll_price;
        IncreasePrice(m_price);
        shopUIManager.UpdateVisual();
    }

   
}
