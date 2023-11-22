using LeakyAbstraction;
using Unity.VisualScripting;
using UnityEngine;

public class ShadeCasting : MonoBehaviour
{
    private SkillStats stat;
    private Vector3 targetPos;
    private GameObject target;
    private bool isLockedTarget = false;

    private int damage;
    private float range;
    [SerializeField] private float speed;

    private void Awake()
    {
        stat = GetComponent<SkillStats>();
    }

    public void Settarget(Vector3 targetPos)
    {
        this.targetPos = targetPos;
        damage = stat.Damage;
        range = stat.Range;
    }

    private void Update()
    {
        if (!isLockedTarget)
        {
            target = FindTarget();
        }

        if (target != null)
        {
            Vector3 dir = target.transform.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                Explosion();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
    }

    private GameObject FindTarget()
    {
        if (target == null && targetPos != null)
        {
            Collider[] colliders = Physics.OverlapSphere(targetPos, range * 10f);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    isLockedTarget = true;
                    return collider.gameObject;
                }
            }
            return null;
        }
        else
        {
            return null;
        }
    }

    private void Explosion()
    {
        SoundManager.Instance.PlaySound(GameSound.ShadeImpact);

        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
