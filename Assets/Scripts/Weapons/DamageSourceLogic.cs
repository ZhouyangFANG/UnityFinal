using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is used to describe a basic damage souce
// Damage Source should be activated and deactivated with Animation Events
// Be deleted by Animation Events or Other Logic
// Moving, and deleting is controlled by other scripts
// Generating is handled in WeaponLogic.OnAttack()

// DamageSource will trigger the BlockLogic to pass damage to the player on it
public class DamageSourceLogic : MonoBehaviour
{
    PlayerID m_sourcePlayerId;
    int m_damage = 1;
    [SerializeField]
    bool m_isMissile = false;

    public delegate void DisableEvent();
    public event DisableEvent OnDisableEvent;

    void Start() {
                
    }

    public void InitDamageSourceInfo(PlayerID source, int damage) {
        m_sourcePlayerId = source;
        m_damage = damage;        
    }

    void Update() {

    }
    
    private void OnDisable() {
        // Used to reset block appearance since OnTriggerExit is not work for disabling
        if (OnDisableEvent != null) {
            OnDisableEvent(); // Reset the block apperance for only once
        }
        OnDisableEvent = null;
    }

    public int getDamage() {
        return m_damage;
    }

    public PlayerID getSourcePlayerID() {
        return m_sourcePlayerId;
    }

    public bool isMissile() {
        return m_isMissile;
    }

    public void setMissile() {
        m_isMissile = true;
    }


}
