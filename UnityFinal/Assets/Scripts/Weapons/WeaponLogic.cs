using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The basic logic of a weapon
// All attack logic are handled in OnAttack
// All weapons should have extra script to bind certain attack logic to OnAttack
// Attack is creating DamageSource 
public class WeaponLogic : MonoBehaviour
{
    [SerializeField]
    float AttackCoolDownTime = 1.0f;
    float m_attackCoolDownTimer = 0;
    // Start is called before the first frame update

    public delegate void AttackEvent();
    public event AttackEvent OnAttackStart;
    public event AttackEvent OnAttackFinish;
    void Start()
    {
        m_attackCoolDownTimer = AttackCoolDownTime;
    }

    public void InitInfo(float attackCdTime) {
        AttackCoolDownTime = attackCdTime;
    }

    // Update is called once per frame
    void Update()
    {
        m_attackCoolDownTimer += Time.deltaTime;
    }

    public bool TryAttack()
    {
        // Try attack, return true if player successfully attack
        if (m_attackCoolDownTimer > AttackCoolDownTime) {
            m_attackCoolDownTimer = 0;
            StartAttack();
            return true;
        }
        return false;
        
    }

    void StartAttack() { 
        if (OnAttackStart != null) {       
            OnAttackStart();
        }
        GetComponentInParent<PlayerController>().m_isAttacking = true;
    }

    public void FinishAttack() {
        // Usually Animation Events
        if(OnAttackFinish != null) {
            OnAttackFinish();
        }
        GetComponentInParent<PlayerController>().m_isAttacking = false;
    }
}

