using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{

    private static TooltipSystem instance;

    public ToolTip tooltip;
    // Start is called before the first frame update

    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        instance.tooltip.gameObject.SetActive(false);
    }

    public static void Show(DataContainer dataContainer)
    {
        Cursor.visible = false;
        instance.tooltip.gameObject.SetActive(true);
        instance.tooltip.SetText(dataContainer);
    }

    public static void Show(SellDataContainer selldDataContainer)
    {
        Cursor.visible = false;
        instance.tooltip.gameObject.SetActive(true);
        instance.tooltip.SetText(selldDataContainer);
    }

    public static void Hide()
    {
        instance.tooltip.gameObject.SetActive(false);
        Cursor.visible = true;
    }
    
}
