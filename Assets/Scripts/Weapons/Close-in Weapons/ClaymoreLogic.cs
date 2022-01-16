using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Special Weapon Script For dagger
public class ClaymoreLogic : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    int Damage = 1;
    [SerializeField]
    DamageSourceLogic[] m_damageSource = null;
    [SerializeField]
    AudioClip SlashSounds;

    AudioSource m_AudioSource;


    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
        GetComponent<WeaponLogic>().OnAttackStart += Attack; // When OnAttackStart is called, Attack is called
        for (int index = 0; index < m_damageSource.Length; ++ index) {
            m_damageSource[index].gameObject.SetActive(false); // Deactivate the DamageSource at default
        }
        
    }

    void Attack() {
        animator.SetTrigger("Attack");
        if (m_AudioSource && SlashSounds) {
            m_AudioSource.PlayOneShot(SlashSounds);
            // Debug.Log("Sounds out");
        }
        // Debug.Log("Attack");
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
