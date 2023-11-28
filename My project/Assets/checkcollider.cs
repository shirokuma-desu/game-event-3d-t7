using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkcollider : MonoBehaviour
{
    public GameObject target;

    GameObject[] listEnemy = new GameObject[1000];
    int index = 0;

    private void Start()
    {
        listEnemy[index] = gameObject;
        index++;
    }

    private void Update()
    {
        target = FindNearestEnemy(gameObject);
    }

    private GameObject FindNearestEnemy(GameObject _target)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f);
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy") && collider.gameObject.activeSelf && hasSetted(collider.gameObject))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = collider.gameObject;
                }
            }
        }

        return nearestEnemy;
    }

    private bool hasSetted(GameObject target)
    {
        for (int i = 0; i < listEnemy.Length; i++)
        {
            if (target == listEnemy[i])
            {
                return false;
            }
        }

        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 15f);
    }
}
