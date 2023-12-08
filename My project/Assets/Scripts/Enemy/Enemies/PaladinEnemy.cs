using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinEnemy : Enemy
{
    [Header("Paladin")]
    [SerializeField]
    private float m_walkDuration;
    [SerializeField]
    private float m_walkDelay;
    private bool m_canMoving;

    protected override void SetupProperties()
    {
        base.SetupProperties();

        m_canMoving = true;
        StartCoroutine(WalkingCircle());
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (GameManager.Instance.GameState != GameManager.State.GameOver)
        {
            if (m_canMoving)
            {
                Move();
            } 
        }
    }

    private IEnumerator WalkingCircle()
    {
        if (m_canMoving) 
        {
            yield return new WaitForSeconds(m_walkDuration);
            m_canMoving = false;
        }
        else 
        {
            yield return new WaitForSeconds(m_walkDelay);
            m_canMoving = true;
        }

        StartCoroutine(WalkingCircle());
    }

    public override void TakeSlowEffect(float _ammount, float _duration) { }
    public override void TakeStunEffect(float _duration) { }
    public override void TakeKnockbackEffect(float _ammount, float _duration) { }
}
