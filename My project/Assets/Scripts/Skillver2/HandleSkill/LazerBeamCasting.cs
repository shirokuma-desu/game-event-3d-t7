using System.Collections;
using UnityEngine;

public class LazerBeamCasting : MonoBehaviour
{
    private SkillStats stat;
    private Vector3 targetPos;
    private Vector3 basePos;
    private Vector3 skillCenterPos;

    private int damage;
    private float range;

    private void Awake()
    {
        stat = GetComponent<SkillStats>();
        basePos = transform.position;
    }

    public void SetTarget(Vector3 targetPos)
    {
        this.targetPos = targetPos;
        damage = stat.Damage;
        range = stat.Range;
        skillCenterPos = (targetPos - basePos).normalized * (range / 2f);
    }

    private void Update()
    {
        if (targetPos != null)
        {
            Collider[] colliders = Physics.OverlapBox(skillCenterPos, new Vector3(0.25f, 0.25f, range / 2f));
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
        }

        StartCoroutine(DelayDestroy());
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
