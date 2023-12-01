using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI headerField;

    public TextMeshProUGUI contentField;

    public TextMeshProUGUI statsField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    public RectTransform rectTransform;


    private const string PREFIX_LEVEL = " Current Lv.";
    private const string PREFIX_TITLE = "Upgrade ";

    private void Awake()
    {
       rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.isEditor)
        {
            int headerLength = headerField.text.Length;
            int contentLength = contentField.text.Length;
            int statsLength = statsField.text.Length;

            layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit || statsLength > characterWrapLimit) ? true : false;
        }

        Vector2 position = Input.mousePosition;

        transform.position = position;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;

    }
        

    public void SetText(DataContainer dataContainer)
    {
        ItemDataSO itemdata = dataContainer.Get();

        if (itemdata.ID_Skill == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }


        headerField.text  = itemdata.is_upgraded? PREFIX_TITLE+itemdata.skill_name + PREFIX_LEVEL + itemdata.level_skill+"+" : "Buy "+itemdata.skill_name;
        contentField.text = itemdata.description;
        
    }

    public void SetText(SellDataContainer sellDataContainer)
    {
        ItemDataSO itemdata = sellDataContainer.Get();

        if (itemdata.ID_Skill == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }


        headerField.text = itemdata.is_upgraded ? PREFIX_TITLE + itemdata.skill_name + PREFIX_LEVEL + itemdata.level_skill + "+" : "Buy " + itemdata.skill_name;
        contentField.text = itemdata.description;
    }
}
