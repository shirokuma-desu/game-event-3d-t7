using UnityEngine;

public class AoeCasting : MonoBehaviour
{
    private SkillStat skillStat;

    private Vector3 targetPosition;

    private int damage;
    private float explosionRange;
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
        explosionRange = skillStat.SkillAffectRange;
    }

    private void Update()
    {
        if (targetPosition != null)
        {
            Vector3 dir = (targetPosition - transform.position);
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                Explosion();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
    }

    void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);

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
