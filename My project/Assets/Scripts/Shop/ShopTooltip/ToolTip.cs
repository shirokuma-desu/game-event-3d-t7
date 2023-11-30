using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;

    }
        

    public void SetText(DataContainer dataContainer)
    {
        if (dataContainer.Get().ID_Skill == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        headerField.text = dataContainer.Get().skill_name +" " + dataContainer.Get().level_skill;
        
    }
}
