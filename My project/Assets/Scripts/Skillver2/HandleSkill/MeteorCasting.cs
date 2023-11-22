using LeakyAbstraction;
using UnityEngine;

public class MeteorCasting : MonoBehaviour
{
    private SkillStats stat;
    private Vector3 targetPos;

    private int damage;
    private float range;
    [SerializeField] private int speed;

    private void Awake()
    {
        stat = GetComponent<SkillStats>();
    }

    public void SetTarget(Vector3 targetPos)
    {
        this.targetPos = targetPos;
        damage = stat.Damage;
        range = stat.Range;
    }

    private void Update()
    {
        if (targetPos != null)
        {
            Vector3 dir = targetPos - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;
            
            if (dir.magnitude <= distanceThisFrame )
            {
                Explosion();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        } 
    }

    private void Explosion()
    {
        SoundManager.Instance.PlaySound(GameSound.MeteorImpact);

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}