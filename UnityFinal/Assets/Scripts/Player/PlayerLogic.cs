using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            // Prevent damage self (if sourcePlayerId is set)
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

    public void takeWeapon(GameObject weapon) {
        // weapon is a prefab
        if (GetComponentsInChildren<WeaponLogic>().Length != 0) {
            foreach(WeaponLogic currentWeapon in GetComponentsInChildren<WeaponLogic>()) {
                Destroy(currentWeapon.gameObject);
            }
        }
        Instantiate(weapon, transform);
    }

    public PlayerID getPlayerID() {
        return m_playerID;
    }

}
