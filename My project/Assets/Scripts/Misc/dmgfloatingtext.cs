using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmgfloatingtext : MonoBehaviour
{
    private float destroyTime = 3f;
    private Vector3 offset = new Vector3(0, 4f, 0);
    private Vector3 sa = new Vector3(0.5f, 0, 0);


    void Start()
    {
        Destroy(gameObject, destroyTime);

        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-sa.x, sa.x), Random.Range(-sa.y, sa.y), Random.Range(-sa.z, sa.z));
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
