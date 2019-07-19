using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Special Weapon Script For dagger
public class SpearLogic : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    int Damage = 1;
    [SerializeField]
    DamageSourceLogic[] m_damageSource = null;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<WeaponLogic>().OnAttackStart += Attack; // When OnAttackStart is called, Attack is called
        for (int index = 0; index < m_damageSource.Length; ++ index) {
            m_damageSource[index].gameObject.SetActive(false); // Deactivate the DamageSource at default
        }
        
    }

    void Attack() {
        animator.SetTrigger("Attack");
    }

    void activateDamageSource() {
        // Animation Events
        for (int index = 0; index < m_damageSource.Length; ++ index) {
            m_damageSource[index].gameObject.SetActive(true);
            m_damageSource[index].InitDamageSourceInfo(GetComponentInParent<PlayerLogic>().getPlayerID(), Damage); 
            // Initialize the damage information after the DamageSource is activated
        }
    }

    void deactivateDamageSource() {
        // Animation Events
        for (int index = 0; index < m_damageSource.Length; ++ index) {
            m_damageSource[index].gameObject.SetActive(false);  
        }           
    }


}
