using System.Collections;
using TMPro;
using UnityEngine;

public class HandleFloatingText : MonoBehaviour
{
    private SkillVisualScript visualScript;

    [SerializeField] private GameObject multicastTextPrefab;
    [SerializeField] private GameObject xTextPrefab;

    private GameObject xText;
    private GameObject multicastText;

    private Animation xAnimation;
    private Animation multicastAnimation;

    private int multicastTimes;

    private void Awake()
    {
        visualScript = GetComponent<SkillVisualScript>();

        if (xText == null)
        {
            xText = Instantiate(xTextPrefab);
            xText.SetActive(false);
            xAnimation = xText.GetComponent<Animation>();
        }

        if (multicastText == null)
        {
            multicastText = Instantiate(multicastTextPrefab);
            multicastText.SetActive(false);
            multicastAnimation = multicastText.GetComponent<Animation>();
        }
    }

    public void ShowFloatingText()
    {
        multicastTimes = visualScript.MultiCastTimes;

        if (multicastTimes > 0 && multicastTimes <= 4)
        {

            StartCoroutine(ShowText());
        }
    }

    private IEnumerator ShowText()
    {
        if (multicastText.activeSelf || xText.activeSelf)
        {
            xAnimation.Play("x_exit");
            multicastAnimation.Play("text_exit");

            yield return new WaitForSeconds(0.2f);

            xText.SetActive(false);
            multicastText.SetActive(false);
        }

        multicastText.SetActive(true);
        multicastAnimation.Play("text_enter");

        xText.SetActive(true);

        for (int i = 1; i <= multicastTimes; i++)
        {
            if (xText.activeSelf)
            {
                xAnimation.Play("x_exit");
                yield return new WaitForSeconds(0.2f);
            }

            xText.GetComponent<TextMeshPro>().text = "x" + i.ToString();
            xAnimation.Play("x_enter");
            yield return new WaitForSeconds(0.2f);
        }

        //if (multicastTimes == 4)
        //{
        //    xText.GetComponent<TextMeshPro>().text = "x4";
        //    xAnimation.Play("x_enter");
        //    yield return new WaitForSeconds(0.3f);
        //}


        yield return new WaitForSeconds(1f);


        xAnimation.Play("x_exit");
        multicastAnimation.Play("text_exit");

        yield return new WaitForSeconds(0.2f);

        xText.SetActive(false);
        multicastText.SetActive(false);
    }
}