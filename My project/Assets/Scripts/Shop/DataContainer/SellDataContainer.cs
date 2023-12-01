using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellDataContainer : MonoBehaviour
{
    //component
    public Image image;
    public TextMeshProUGUI price;
    public Button button;

    //data

    public ItemDataSO m_datacontain;


    // Start is called before the first frame update
    private void Start()
    {
        LoadScriptableObject();
        this.RegisterListener(EventID.OnBuyingItem,  (param) => LoadScriptableObject());
        this.RegisterListener(EventID.OnSoldItem, (param) => LoadScriptableObject());
    }

    private void LoadScriptableObject()
    {
        image.sprite =   m_datacontain.image;
        price.text   =   m_datacontain.sellprice.ToString();
    }

    public void Set(ItemDataSO skillObject)
    {
        this.m_datacontain = skillObject;
    }

    public ItemDataSO Get()
    {
        return m_datacontain;
    }
}