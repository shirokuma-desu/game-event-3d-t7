using JetBrains.Annotations;
using UnityEngine;

public class MiniSpreadBulletScript : MonoBehaviour
{
    [SerializeField]
    private float m_speed;
    private int damage;

    private Vector3 distance = Vector3.zero;
    private Vector3 basePos;
    private Vector3 dir;

    private void Awake()
    {
        basePos = transform.position;
    }

    public void SetDamage(Vector3 _direction, int _damage)
    {
        damage = _damage;
        dir = _direction;
    }

    private void Update()
    {
        DestroyThis();

        transform.Translate(dir.normalized * m_speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void DestroyThis()
    {
        distance = transform.position - basePos;
        if (distance.magnitude > 10f )
        {
            Destroy(gameObject);
        }
    }
}
