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

    private void Start()
    {
        stat = GetComponent<SkillStats>();
        basePos = transform.position;
    }

    public void SetUp()
    {
        stat = GetComponent<SkillStats>();
        basePos = transform.position;

        float scaleSkill = stat.Range;
        transform.localScale = new Vector3(0.5f, 0.5f, scaleSkill);
    }

    public void SetTarget(Vector3 targetPos)
    {
        this.targetPos = targetPos;
        transform.forward = (targetPos - transform.position).normalized;
        damage = stat.Damage;
        range = stat.Range;
        skillCenterPos = (targetPos - basePos).normalized * (range / 2f);
        skillCenterPos.y = 0f;
    }

    private void Update()
    {
        if (targetPos != null)
        {
            RaycastHit[] _hits = Physics.RaycastAll(transform.position, transform.forward, range);
            foreach (RaycastHit _hit in _hits)
            {
                Collider collider = _hit.collider;
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
