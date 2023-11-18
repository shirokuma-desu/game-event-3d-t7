using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    public GameObject ShopUI;

    private bool m_isOn ;

    // Start is called before the first frame update
    void Start()
    {
       ShopUI.gameObject.SetActive(false);
        m_isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Show()
    {
        if (m_isOn)
        {
            ShopUI.gameObject.SetActive(false);
            m_isOn = false;
        }
        else
        {
            ShopUI.gameObject.SetActive(true);
            m_isOn = true;
        }
    }


}
