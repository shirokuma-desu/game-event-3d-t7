using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchIntro : MonoBehaviour
{
    public float speed = 1.0f;
    public Color startColor;
    public Color endColor;
    public bool repeatable = false;
    float startTime;
    public float sec = 14f;

    public GameObject gameObject1;
    public GameObject gameObject2;


	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (!repeatable)
        {
            float t = (Time.time - startTime) * speed;
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        }
        else
        {
            float t = (Mathf.Sin(Time.time - startTime) * speed);
			GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        }
        Invoke("SetActive1", sec);
        Invoke("planeModifi", sec+1);
	}


    void SetActive1()
    {
        gameObject1.SetActive(true);
    }

    void SetActive2()
    {
        gameObject2.SetActive(true);
    }

    void planeModifi()
    {
        transform.position = new Vector3(0, 0, -18);
        Invoke("SetActive2", sec);
        Destroy(gameObject, sec+1);
    }
}
