using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Handle other logic for player rather than moving and attacking
public class PlayerLogic : MonoBehaviour
{
    PlayerID m_playerID;
    
    const int FullHp = 3;
    int m_hp;

    const float InvincibleAfterDamageTime = 2.0f;
    float m_invincibleAfterDamageTimer = 0;
        
    // Start is called before the first frame update
    void Start()
    {
        m_hp = FullHp;
        m_invincibleAfterDamageTimer = InvincibleAfterDamageTime;
    }

    public void InitInfo(PlayerID id) {
        m_playerID = id;
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
                m_hp -= damageSource.GetComponent<DamageSourceLogic>().getDamage();
                if (m_hp <= 0) {
                    Death();
                }
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
        WeaponLogic weaponLogic = Instantiate(weapon, transform).GetComponent<WeaponLogic>();
        weaponLogic.OnAttackStart += AttackStarted;
        weaponLogic.OnAttackFinish += AttackFinished;
    }

    public void takePickUp(PickUpLogic item) {
        if (item.isWeapon()) {
            TakeWeapon(item.getItemPrefab());
        } else if (item.isPowerUp()) {

        } else if (item.isHp()) {

        }
        Destroy(item.gameObject);
    }

    void TakeHp() {
        if (m_hp < FullHp) {
            m_hp += 1;
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

}
