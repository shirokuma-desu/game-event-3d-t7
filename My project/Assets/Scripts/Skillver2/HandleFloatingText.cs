using System.Collections;
using TMPro;
using UnityEngine;

public class HandleFloatingText : MonoBehaviour
{
    private SkillVisualScript visualScript;

    [SerializeField] private GameObject multicastTextPrefab;
    [SerializeField] private GameObject xTextPrefab;

    [SerializeField]
    private Vector3 m_effectPosition;
    [SerializeField]
    private GameObject m_normalMulticastEffect;
    [SerializeField]
    private GameObject m_x3MulticastEffect;
    [SerializeField]
    private GameObject m_x4MulticastEffect;

    private GameObject xText;
    private GameObject multicastText;

    private Animation xAnimation;
    private Animation multicastAnimation;

    private int multicastTimes;

    private int m_currentMulticast;

    private void Start()
    {
        visualScript = GetComponent<SkillVisualScript>();

        if (xText == null)
        {
            xText = Instantiate(xTextPrefab, new Vector3(-6f, 10f, 0f), xTextPrefab.transform.rotation, transform);
            xText.SetActive(false);
            xAnimation = xText.GetComponent<Animation>();
        }

        if (multicastText == null)
        {
            multicastText = Instantiate(multicastTextPrefab, new Vector3(2f, 10f, 0f), multicastTextPrefab.transform.rotation, transform);
            multicastText.SetActive(false);
            multicastAnimation = multicastText.GetComponent<Animation>();
        }

        m_currentMulticast = 0;
    }

    public void ShowFloatingText()
    {
        multicastTimes = visualScript.MultiCastTimes;

        if (multicastTimes > 0 && multicastTimes <= 4)
        {
            IEnumerator _showTextCoroutine = ShowText();

            if (multicastTimes >= m_currentMulticast) StopAllCoroutines();
            StartCoroutine(_showTextCoroutine);
        }
    }

    private IEnumerator ShowText()
    {
        m_currentMulticast = multicastTimes;

        multicastText.SetActive(true);
        multicastAnimation.Play("text_enter");

        xText.SetActive(true);

        for (int i = 1; i <= multicastTimes; i++)
        {
            if (xText.activeSelf)
            {
                xAnimation.Play("x_exit");
                yield return new WaitForSeconds(0.13f);
            }

            xText.GetComponent<TextMeshPro>().text = "x" + (i + 1).ToString();
            xAnimation.Play("x_enter");

            if (i < multicastTimes)
            {
                Instantiate(m_normalMulticastEffect, m_effectPosition, Quaternion.identity);
            }
            else
            {
                if (i == 3) Instantiate(m_x3MulticastEffect, m_effectPosition, Quaternion.identity);
                else if (i == 4) Instantiate(m_x4MulticastEffect, m_effectPosition, Quaternion.identity);
                else Instantiate(m_normalMulticastEffect, m_effectPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(1f);
        if (m_currentMulticast >= 3) yield return new WaitForSeconds(.5f);
        if (m_currentMulticast >= 4) yield return new WaitForSeconds(.25f);

        xAnimation.Play("x_exit");
        multicastAnimation.Play("text_exit");

        yield return new WaitForSeconds(0.13f);
        
        xText.SetActive(false);
        multicastText.SetActive(false);

        m_currentMulticast = 0;
    }
}