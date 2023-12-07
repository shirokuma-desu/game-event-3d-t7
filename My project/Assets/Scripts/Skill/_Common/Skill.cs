using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    protected SkillVisual m_visual;
    public SkillManager Manager { get; set; }

    [Header("Basic Stats")]
    [SerializeField]
    protected ItemDataSO m_statData;

    [SerializeField]
    protected int m_ID;
    public int ID { get => m_ID; }

    protected int m_level;
    public int Level { get => m_level; }

    protected float m_damage;
    public float Damage { get => m_damage; }
    protected float m_range;
    public float Range { get => m_range; }
    protected float m_multicastRate;
    public float MulticastRate { get => m_multicastRate; }
    protected float m_cooldown;
    public float Cooldown { get => m_cooldown; }

    [Header("Casting")]
    [SerializeField]
    protected float m_castDelay;
    public float CastDelay { get => m_castDelay; }
    [SerializeField]
    protected float m_expireTime;
    public float ExpireTime { get => m_expireTime; }

    [Header("Multicast")]
    [SerializeField]
    protected float m_multicastOffset;
    public float MulticastOffset { get => m_multicastOffset; }
    [SerializeField]
    protected float m_multicastDelay;
    public float MulticastDelay { get => m_multicastDelay; }

    protected Vector3 m_castPosition;
    public Vector3 CastPosition { get => m_castPosition; }

    enum SkillState 
    {
        Preview,
        CastRaw,
        CastInit,
        Impact,
        Expire
    } 
    private SkillState m_state;

    public virtual void SetUp()
    {
        // m_ID = m_statData.ID_Skill;
        m_level = m_statData.level_skill;

        m_damage = m_statData.damage;
        m_range = m_statData.radius;
        m_multicastRate = m_statData.multi_cast_chance;
        m_cooldown = m_statData.cooldown;

        // m_damage += m_statData.damage_increase * m_level;
        // m_range += m_statData.radius_increase * m_level;
        // m_cooldown -= m_statData.cooldown_decrease * m_level;
    }

    public virtual void Preview()
    {
        m_visual.SetUp();

        m_state = SkillState.Preview;
    }

    public virtual void Discard()
    {
        Destroy(gameObject);
    }

    public virtual void CastRaw(Vector3 _position)
    {
        if (m_state == SkillState.CastInit)
        {
            Debug.LogWarning("Skill: This skill is cast to be 'INIT', cannot cast raw");
            return;
        }

        m_castPosition = _position;

        m_state = SkillState.CastRaw;

        StartCoroutine(Casting());
    }
    public virtual void CastInit(Vector3 _position, int _multicastTime)
    {
        if (m_state == SkillState.CastRaw)
        {
            Debug.LogWarning("Skill: This skill is cast to be 'RAW', cannot cast init");
            return;
        }

        m_expireTime += m_multicastDelay * _multicastTime;
        StartCoroutine(HandleMulticast(_multicastTime, _position));

        m_castPosition = _position;

        m_state = SkillState.CastInit;

        StartCoroutine(Casting());
    }
    protected virtual IEnumerator HandleMulticast(int _num, Vector3 _position)
    {
        for (int i = 0; i < _num; i++)
        {
            Vector2 _random = Random.insideUnitCircle;
            Vector3 _target = _position;
            _target.x += _random.x * m_multicastOffset;
            _target.z += _random.y * m_multicastOffset;

            yield return new WaitForSeconds(m_multicastDelay);
            Manager.CastSkillRaw(m_ID, _target);
        }
    }
    protected virtual IEnumerator Casting()
    {
        m_visual.PrepareCastVisual();

        yield return new WaitForSeconds(m_castDelay);

        Impact();
    }

    protected virtual void Impact()
    {
        m_state = SkillState.Impact;

        m_visual.ImpactVisual();
    } 

    protected virtual void Expire()
    {
        m_state = SkillState.Expire;

        StartCoroutine(Expiring());
    }
    protected virtual IEnumerator Expiring()
    {
        yield return new WaitForSeconds(m_expireTime);

        Destroy(gameObject);
    }

    protected virtual void Update()
    {
        switch (m_state)
        {
            case (SkillState.Preview):
                m_visual.PreviewVisual();
                break;
            case (SkillState.CastRaw):
            case (SkillState.CastInit):
                m_visual.CastVisual();
                break;
            case (SkillState.Impact):
                break;
            case (SkillState.Expire):
                m_visual.ExpireVisual();
                break;
            default:
                break;
        }
    }
}
