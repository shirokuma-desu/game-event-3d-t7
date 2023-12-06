using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    [SerializeField]
    private ShopSystem m_shop;
    public ShopSystem Shop { get => m_shop; }
}
