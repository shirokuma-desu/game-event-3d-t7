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
    public ItemDataSO m_datacontain;

    void Start()
    {
        //load first time
        LoadScriptableObject();
        this.RegisterListener(EventID.OnReroll,           (param) => LoadScriptableObject());
        this.RegisterListener(EventID.OnBuyingItem,       (param) => LoadScriptableObject());
        this.RegisterListener(EventID.OnBuyUpgradeTurret, (param) => LoadScriptableObject());    
        
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


    public void Set(ItemDataSO skillObject)
    {
        this.m_datacontain = skillObject;
    }

    public ItemDataSO Get()
    {
        return m_datacontain;
    }
}
 