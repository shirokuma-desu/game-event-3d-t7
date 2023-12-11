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
        float _time = 1f;
        if (GameManager.Instance.GameState == GameManager.State.Paused) _time *= .01f;
        delay = LeanTween.delayedCall(_time, () =>
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
