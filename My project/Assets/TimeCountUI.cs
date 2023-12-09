using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCountUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_text;

    private void Update()
    {
        int _time = (int)GameManager.Instance.GameTime;
        int _minutes = _time / 60;
        int _second = _time - _minutes * 60;

        string _string = string.Format("{0:D2}:{1:D2}", 
                _minutes, 
                _second);

        m_text.text = _string;
    }
}
