using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LavaCasting : MonoBehaviour
{
    private SkillStats stat;
    private Vector3 targetPos;

    private int damage;
    private float range;
    private float duration;

    private float skillStartT;
    private bool canDamaging = true;

    private void Awake()
    {
        stat = GetComponent<SkillStats>();
        skillStartT = Time.time;
    }

    public void SetTarget(Vector3 targetPos)
    {
        this.targetPos = targetPos;
        damage = stat.Damage;
        range = stat.Range;
        duration = stat.Duration;
    }

    private void Update()
    {
        if (targetPos != null)
        {
            if (Time.time - skillStartT < duration)
            {
                if (canDamaging)
                {
                    StartCoroutine(LavaDamaging());
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator LavaDamaging()
    {
        Collider[] colliders = Physics.OverlapSphere(targetPos, range);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        canDamaging = false;
        yield return new WaitForSeconds(1);
        canDamaging = true;
    }
}
