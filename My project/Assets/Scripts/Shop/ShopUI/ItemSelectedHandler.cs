using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSelectedHandler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private float _verticalMoveAmount = 30f;
    [SerializeField] private float _moveTime = 0.1f;
    [Range(0f, 2f), SerializeField] private float _scaleAmount = 1.1f;

    private Vector3 _startPos;
    private Vector3 _startScale;
    
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
        _startScale = transform.localScale;
    }

    private IEnumerator Selected(bool startingAnimation)
    {
        Vector3 endPost =  new Vector3();
        Vector3 endScale = new Vector3();

        float elapsedTime = 0f;
        while(elapsedTime < _moveTime)
        {
            elapsedTime += Time.deltaTime;
            if(startingAnimation)
            {
                endPost  =  _startPos   +   new Vector3(0f, _verticalMoveAmount, 0f);
                endScale =  _startScale *  _scaleAmount;
            }

            else
            {
                endPost = _startPos;
                endScale = _startScale;
            }
        }

        Vector3 lerpedPos = Vector3.Lerp(transform.position, endPost, (elapsedTime / _moveTime));
        Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / _moveTime));

        transform.position = lerpedPos;
        transform.localScale = lerpedScale;

        yield return null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(Selected(true));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(Selected(false));
    }
}
