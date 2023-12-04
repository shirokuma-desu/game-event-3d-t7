using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObstacle : MonoBehaviour
{
    protected int m_skillID;
    public int SkillID { get => m_skillID; }
    protected float m_damage;
    public float Damage { get => m_damage; }
    [SerializeField]
    protected float m_expireTime;
    public float ExpireTime { get => m_expireTime; }

    public virtual void SetupProperties(int _id, float _damage)
    {
        m_skillID = _id;
        m_damage = _damage;
    }

    protected virtual void Expire()
    {
        StartCoroutine(Expiring());
    }
    protected virtual IEnumerator Expiring()
    {
        yield return new WaitForSeconds(m_expireTime);

        Destroy(gameObject);
    }
}
