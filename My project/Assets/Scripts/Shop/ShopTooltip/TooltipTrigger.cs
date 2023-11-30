using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public DataContainer dataContainer;
    private IEnumerator tooltipCoroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipCoroutine != null)
        {
            StopCoroutine(tooltipCoroutine);
        }

        tooltipCoroutine = ShowTooltipAfterDelay(1.0f);
        StartCoroutine(tooltipCoroutine);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipCoroutine != null)
        {
            StopCoroutine(tooltipCoroutine);
        }
        TooltipSystem.Hide();
    }

    private IEnumerator ShowTooltipAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        TooltipSystem.Show(dataContainer);
    }


}
