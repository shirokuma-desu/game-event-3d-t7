using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public void Load(string _name)
    {
        LoadAsync.Instance.LoadScene(_name);
    }
}
