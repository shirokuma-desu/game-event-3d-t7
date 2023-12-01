using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public DataContainer dataContainer ;

    public SellDataContainer selldataContainer ;

    private static LTDescr delay;

    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = LeanTween.delayedCall(1.0f, () =>
        {
            if (dataContainer is not null)
            {
                TooltipSystem.Show(dataContainer);
            }
            if(selldataContainer is not null)
            {
                TooltipSystem.Show(selldataContainer);
            }
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }



}
