using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataContainer : MonoBehaviour
{
   
    //component
    public Image image;
    public TextMeshProUGUI price;
    public Button button;

    //data
    private int m_skill_id = 0;
    public SkillObjectSO m_datacontain;

    void Start()
    {
        //load first time
        LoadScriptableObject();
        this.RegisterListener(EventID.OnRerolledShop, (param) => LoadScriptableObject());
        
    }

    private void LoadScriptableObject()
    {
        if(m_datacontain.is_upgraded == false)
        {
            image.sprite = m_datacontain.image;
        }
        else
        {
            image.sprite = m_datacontain.image_upgrade;
        }
        price.text = m_datacontain.price.ToString();
        m_skill_id = m_datacontain.ID_Skill;
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
 