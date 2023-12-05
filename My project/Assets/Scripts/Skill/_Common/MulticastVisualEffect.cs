using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MulticastVisualEffect : MonoBehaviour
{
    [SerializeField]
    private SkillManager m_skillManager;

    [SerializeField]
    private GameObject m_multicastText;
    private Animation m_multicastAnimation;

    [SerializeField]
    private GameObject m_multicastTimeText;
    private Animation m_multicastTimeAnimation;

    [SerializeField]
    private Vector3 m_effectPosition;
    [SerializeField]
    private GameObject m_normalMulticastEffect;
    [SerializeField]
    private GameObject m_x3MulticastEffect;
    [SerializeField]
    private GameObject m_x4MulticastEffect;

    private int m_currentMulticast;

    private void Start()
    {
        m_multicastAnimation = m_multicastText.GetComponent<Animation>();
        m_multicastTimeAnimation = m_multicastTimeText.GetComponent<Animation>();

        m_multicastText.SetActive(false);
        m_multicastTimeText.SetActive(false);
    }

    public void ShowFloatingText()
    {
        int _multicastTimes = m_skillManager.MulticastTime;

        if (_multicastTimes > 0)
        {
            IEnumerator _showTextCoroutine = ShowText(_multicastTimes);

            if (_multicastTimes >= m_currentMulticast) StopAllCoroutines();
            StartCoroutine(_showTextCoroutine);
        }
    }

    private IEnumerator ShowText(int _multicastTimes)
    {
        m_currentMulticast = _multicastTimes;

        m_multicastText.SetActive(true);
        m_multicastAnimation.Play("text_enter");

        m_multicastTimeText.SetActive(true);

        for (int i = 1; i <= _multicastTimes; i++)
        {
            if (m_multicastTimeText.activeSelf)
            {
                m_multicastTimeAnimation.Play("x_exit");
                yield return new WaitForSeconds(0.13f);
            }

            m_multicastTimeText.GetComponent<TextMeshPro>().text = "x" + (i + 1).ToString();
            m_multicastTimeAnimation.Play("x_enter");

            if (i < _multicastTimes)
            {
                Instantiate(m_normalMulticastEffect, m_effectPosition, Quaternion.identity);
            }
            else
            {
                if (i >= 4) Instantiate(m_x4MulticastEffect, m_effectPosition, Quaternion.identity);
                else if (i >= 3) Instantiate(m_x3MulticastEffect, m_effectPosition, Quaternion.identity);
                else Instantiate(m_normalMulticastEffect, m_effectPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(1f);
        if (m_currentMulticast >= 3) yield return new WaitForSeconds(.5f);
        if (m_currentMulticast >= 4) yield return new WaitForSeconds(.25f);

        m_multicastTimeAnimation.Play("x_exit");
        m_multicastAnimation.Play("text_exit");

        yield return new WaitForSeconds(0.13f);
        
        m_multicastTimeText.SetActive(false);
        m_multicastText.SetActive(false);

        m_currentMulticast = 0;
    }
}