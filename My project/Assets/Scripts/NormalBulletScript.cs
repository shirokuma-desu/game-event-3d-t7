using System.Runtime.CompilerServices;
using UnityEngine;

public class NormalBulletScript : MonoBehaviour
{
    private GameObject target;

    private int damage;
    [SerializeField]
    private float speed = 5f;

    public void SetTarget(GameObject newTarget, int attackDamage)
    {
        target = newTarget;
        damage = attackDamage;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        dir.y = 0f;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget(target);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget(GameObject target)
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
