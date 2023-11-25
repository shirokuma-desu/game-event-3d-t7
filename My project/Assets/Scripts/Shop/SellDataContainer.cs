using System.Collections;
using System.Collections.Generic;
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

    public SkillObjectSO m_datacontain;

    // Start is called before the first frame update
    private void Start()
    {
        LoadScriptableObject();
        this.RegisterListener(EventID.OnBuyingItem, (param) => LoadScriptableObject());
        this.RegisterListener(EventID.OnSellingItem, (param) => LoadScriptableObject());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void LoadScriptableObject()
    {
        image.sprite = m_datacontain.image;
        price.text = m_datacontain.sellprice.ToString();
    }

    public void Set(SkillObjectSO skillObject)
    {
        this.m_datacontain = skillObject;
    }

    public SkillObjectSO Get()
    {
        return m_datacontain;
    }
}