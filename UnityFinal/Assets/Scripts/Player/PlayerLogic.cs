using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Handle other logic for player rather than moving and attacking
public class PlayerLogic : MonoBehaviour
{
    PlayerID m_playerID;
    
    const int FullHp = 3;
    int m_hp;
    Animator m_animator;

    // [SerializeField]
    Transform m_leftHandTarget;

    // [SerializeField]
    Transform m_rightHandTarget;

    [SerializeField]
    bool m_isRightHandIKActive = true;

    [SerializeField]
    bool m_isLeftHandIKActive = true;

    const float InvincibleAfterDamageTime = 2.0f;
    float m_invincibleAfterDamageTimer = 0;

    GameObject m_takingPowerUp; // Ready to be cast
    WeaponLogic m_takingWeapon;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_hp = FullHp;
        m_invincibleAfterDamageTimer = InvincibleAfterDamageTime;
    }

    public void InitInfo(PlayerID id) {
        m_playerID = id;
    }
    void UpdateHandIK(bool isActive, Transform target, AvatarIKGoal avatarIKGoal)
    {
        if(isActive && target)
        {
            m_animator.SetIKPositionWeight(avatarIKGoal, 1);
            m_animator.SetIKRotationWeight(avatarIKGoal, 1);
            
            m_animator.SetIKPosition(avatarIKGoal, target.position);
            m_animator.SetIKRotation(avatarIKGoal, target.rotation);
        }
        else
        {
            m_animator.SetIKPositionWeight(avatarIKGoal, 0);
            m_animator.SetIKRotationWeight(avatarIKGoal, 0);
        }
    }
    void OnAnimatorIK()
    {
        if (m_animator)
        {
            UpdateHandIK(m_isRightHandIKActive,m_rightHandTarget, AvatarIKGoal.RightHand);
            UpdateHandIK(m_isLeftHandIKActive, m_leftHandTarget, AvatarIKGoal.LeftHand);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (m_invincibleAfterDamageTimer < InvincibleAfterDamageTime) {
            m_invincibleAfterDamageTimer += Time.deltaTime;
        } else {
            // reset appearence here
        }
    }

    // Receive the damage from the block logic
    public void takeDamage(GameObject damageSource) {
        if (damageSource.GetComponent<DamageSourceLogic>().getSourcePlayerID() != m_playerID) {
            // Prevent damage to self (if sourcePlayerId is set)
            if (m_invincibleAfterDamageTimer >= InvincibleAfterDamageTime) {
                m_invincibleAfterDamageTimer = 0.0f;
                // Update the player appearance here (invincible)
                Debug.Log(m_playerID.ToString() + " is damaged for 1 hp");
                damageSource.SetActive(false);
                m_hp -= damageSource.GetComponent<DamageSourceLogic>().getDamage();
                if (m_hp <= 0) {
                    Death();
                }
            }
        }
    }

    public void takeDamage(int damage) {
        // Prevent damage to self (if sourcePlayerId is set)
        ShieldLogic shield = GetComponentInChildren<ShieldLogic>();
        
        if (shield) {
            Destroy(shield.gameObject);
            return;
        }

        if (m_invincibleAfterDamageTimer >= InvincibleAfterDamageTime) {
            m_invincibleAfterDamageTimer = 0.0f;
            // Update the player appearance here (invincible)
            Debug.Log(m_playerID.ToString() + " is damaged for 1 hp");                
            m_hp -= damage;
            if (m_hp <= 0) {
                Death();
            }
        }
    }

    void Death() {
        Destroy(gameObject);
    }

    void TakeWeapon(GameObject weapon) {
        // weapon is a prefab
        
        if (GetComponentsInChildren<WeaponLogic>().Length != 0) {
            // the player is holding other weapon
            // destroy them before get new weapon
            foreach(WeaponLogic currentWeapon in GetComponentsInChildren<WeaponLogic>()) {
                Destroy(currentWeapon.gameObject);
            }
        }

        m_takingWeapon = Instantiate(weapon, transform).GetComponent<WeaponLogic>();
        m_leftHandTarget = m_takingWeapon.LeftHandTarget;
        m_rightHandTarget = m_takingWeapon.RightHandTarget;
        m_takingWeapon.OnAttackStart += AttackStarted;
        m_takingWeapon.OnAttackFinish += AttackFinished;
    }

    void TakePowerUp(GameObject powerUp) {
        // Save the pickup currently

        m_takingPowerUp = powerUp;
        
    }

    public void takePickUp(PickUpLogic item) {        
        if (item.isWeapon()) {
            TakeWeapon(item.getItemPrefab());
        } else if (item.isPowerUp()) {
            TakePowerUp(item.getItemPrefab());
        } else if (item.isHp()) {
            TakeHp();
        }
        Destroy(item.gameObject);
    }

    void TakeHp() {
        if (m_hp < FullHp) {
            m_hp += 1;
            Debug.Log(m_playerID.ToString() + " is Recovered for 1 hp");
        }
    }

    public PlayerID getPlayerID() {
        return m_playerID;
    }

    void AttackStarted() {
        GetComponent<PlayerController>().m_isAttacking = true;
    }

    void AttackFinished() {
        GetComponent<PlayerController>().m_isAttacking = false;
    }

    public void castCurrentTakingPowerUp() {
        if (m_takingPowerUp) {
            takeEffector(m_takingPowerUp);
            m_takingPowerUp = null;
        }
    }

    public void takeEffector(GameObject powerUpEffector) {
        GameObject powerUp = Instantiate(powerUpEffector, transform);
        powerUp.GetComponent<PowerUpLogic>().cast();
    }

}
