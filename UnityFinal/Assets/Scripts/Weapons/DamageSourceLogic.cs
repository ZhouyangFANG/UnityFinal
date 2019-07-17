using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script is used to describe a basic damage souce
// Damage Source should be generated with Animation Events
// Be deleted by Animation Events or Other Logic
// Moving, and deleting is controlled by other scripts
// Generating is handled in WeaponLogic.OnAttack()
public class DamageSourceLogic : MonoBehaviour
{
    PlayerID m_sourcePlayerId;
    int m_damage = 1;    

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
        if (OnDisableEvent != null) {
            OnDisableEvent(); // Reset the block apperence for only once
        }
        OnDisableEvent = null;
    }

    public int getDamage() {
        return m_damage;
    }

    public PlayerID getSourcePlayerID() {
        return m_sourcePlayerId;
    }



}
