using UnityEngine;

public class StarFallCasting : MonoBehaviour
{
    private SkillStats stat;

    private int damage;
    private int numberOfPieces;

    private void Awake()
    {
        stat = GetComponent<SkillStats>();
    }

    public void SetDamage()
    {
        damage = stat.Damage;
        numberOfPieces = stat.NumberOfPieces;
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(.5f, 1.5f, .5f));
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Explosion();
                break;
            }
        }
    }

    private void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.5f);

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
