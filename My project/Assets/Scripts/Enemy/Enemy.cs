using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public MoneySystem moneySystem;
    public EnemySO enemyStat;
    private Rigidbody rb;
    public BoxCollider boxCollider;

    public GameObject target;

    [SerializeField]
    private int health;
    private int damage;
    private float speed;
    private int moneyDrop;

    private bool isDyingAnimation = false;

    [Header("Disable Affects")]
    [SerializeField]
    private bool isStunning = false;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void TakeDisable(float disableTime)
    {
        StartCoroutine(CooldownDisable(disableTime));
    }

    IEnumerator CooldownDisable(float disableTime)
    {
        isStunning = true;

        yield return new WaitForSeconds(disableTime);

        isStunning = false;
    }

    private void Awake()
    {
        moneySystem = GameObject.Find("GameManager").GetComponent<MoneySystem>();
        rb = GetComponent<Rigidbody>();

        health = enemyStat.health;
        damage = enemyStat.attackDamage;
        speed = enemyStat.speed;
        moneyDrop = enemyStat.moneyDrop;
    }

    private void Update()
    {
        if (isDyingAnimation)
        {
            boxCollider.enabled = false;
            damage = 0;
            rb.isKinematic = true;
        }

        if (health <= 0)
        {
            //Die();
        }

        target = FindTarget();
        if (target != null && !isStunning && !isDyingAnimation)
        {
            Vector3 dir = target.transform.position - transform.position;
            dir.Normalize();
            dir.y = transform.position.y;

            rb.velocity = dir * speed;
            transform.forward = dir;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BaseTower"))
        {
            BaseTurret baseTurret = collision.gameObject.GetComponent<BaseTurret>();
            if (baseTurret != null)
            {
                baseTurret.TakeDamage(damage);
            }
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Turret"))
        {
            Turret turret = collision.gameObject.GetComponent<Turret>();
            if (turret != null)
            {
                turret.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    GameObject FindTarget()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        GameObject tower = GameObject.FindGameObjectWithTag("BaseTower");

        float closestDistance = float.MaxValue;
        GameObject closestTarget = null;

        foreach (GameObject t in turrets)
        {
            if (t.GetComponent<Turret>().isSettle)
            {
                float distance = Vector3.Distance(transform.position, t.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = t;
                }
            }
        }

        float distance2 = Vector3.Distance(transform.position, tower.transform.position);
        if (distance2 < closestDistance)
        {
            closestDistance = distance2;
            closestTarget = tower;
        }

        return closestTarget;
    }

    //void Die()
    //{
    //    moneySystem.money += moneyDrop;
    //    isDyingAnimation = true;
    //    animator.SetTrigger("Die");
    //    Invoke("DestroyGO", .5f);
    //}

    void DestroyGO()
    {
        Destroy(gameObject);
    }

    void Attack(int damage)
    {
        if (target.CompareTag("Turret"))
        {
            target.GetComponent<Turret>().TakeDamage(damage);
        }
        if (target.CompareTag("BaseTower"))
        {
            target.GetComponent<BaseTurret>().TakeDamage(damage);
        }
    }
}
