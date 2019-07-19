using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponID {
    None = 0,
    Dagger = 1,
    Claymore = 2,
    Spear = 3,
    Pistol = 4,
    Bow = 5,
    Wand = 6
}

// The basic logic of a weapon
// Provide convenient logic between caller and different weapon
// All attack logic are handled in OnAttack
// All weapons should have extra script to bind certain attack logic to OnAttack
// Attack is creating DamageSource 
public class WeaponLogic : MonoBehaviour
{
    [SerializeField]
    WeaponID m_weaponID;

    [SerializeField]
    float AttackCoolDownTime = 1.0f;
    float m_attackCoolDownTimer = 0;
    // Start is called before the first frame update

    public delegate void AttackEvent();
    public event AttackEvent OnAttackStart;
    public event AttackEvent OnAttackFinish;

    [SerializeField]
    public Transform RightHandTarget;

    [SerializeField]
    public Transform LeftHandTarget;

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

    public bool tryAttack()
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
    }

    void FinishAttack() {
        // Usually Animation Events
        // After that the player can move see PlayerLogic
        if(OnAttackFinish != null) {            
            OnAttackFinish();
        }
    }

    public WeaponID GetWeaponID()
    {
        return m_weaponID;
    }
}

