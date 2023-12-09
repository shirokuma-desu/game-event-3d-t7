using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalTimeScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_currentTimeText;
    [SerializeField]
    private TextMeshProUGUI m_bestTimeText;

    [SerializeField]
    private TimeScore m_scoreData;

    private void Start()
    {
        SetScore(m_currentTimeText, (int)m_scoreData.CurrentTime);
        SetScore(m_bestTimeText, (int)m_scoreData.BestTime);
    }

    private void SetScore(TextMeshProUGUI _text, int _time)
    {
        int _minutes = _time / 60;
        int _second = _time - _minutes * 60;

        string _string = string.Format("{0:D2}:{1:D2}", 
                _minutes, 
                _second);

        _text.text = _string;
    }
}
