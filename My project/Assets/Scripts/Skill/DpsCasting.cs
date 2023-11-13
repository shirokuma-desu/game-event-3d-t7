using UnityEngine;

public class DpsCasting : MonoBehaviour
{
    private SkillStat skillStat;

    private Vector3 targetDirection;

    [SerializeField]
    private Vector3 basePos;

    private int damage;
    private float dpsRange;
    [SerializeField]
    private float speed;

    private void Awake()
    {
        skillStat = GetComponent<SkillStat>();
        basePos = transform.position;
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetDirection = position - basePos;
        damage = skillStat.skillDamage;
        dpsRange = skillStat.SkillAffectRange;
    }

    private void Update()
    {
        if (targetDirection != null)
        {
            transform.Translate(targetDirection.normalized * speed * Time.deltaTime);
        }

        SetDamage();
    }

    void SetDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }

        Vector3 distance = new Vector3(transform.position.x - basePos.x, 0f, transform.position.z - basePos.z);
        if (distance.magnitude >= dpsRange)
        {
            Destroy(gameObject);
        }
    }
}
