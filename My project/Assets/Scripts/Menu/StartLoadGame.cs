using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLoadGame : MonoBehaviour
{
    private void Start()
    {
        LoadAsync.Instance.LoadScene("StartMenu");
    }
}
