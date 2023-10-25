using UnityEngine;

public class DisableCasting : MonoBehaviour
{
    private SkillStat skillStat;

    private Vector3 targetPosition;

    private int damage;
    private float disableRange;
    private float disableTime;
    [SerializeField]
    private float speed;

    private void Awake()
    {
        skillStat = GetComponent<SkillStat>();
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
        damage = skillStat.skillDamage;
        disableRange = skillStat.SkillAffectRange;
        disableTime = skillStat.SkillAffectTime;
    }

    private void Update()
    {
        if (targetPosition != null)
        {
            Vector3 dir = (targetPosition - transform.position);
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                Stun();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
    }

    void Stun()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, disableRange);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().TakeDisable(disableTime);
                collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);

            }
        }

        Destroy(gameObject);
    }
}
