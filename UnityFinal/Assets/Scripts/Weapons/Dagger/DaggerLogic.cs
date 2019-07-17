using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerLogic : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    int Damage = 1;
    [SerializeField]
    DamageSourceLogic m_damageSource = null;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<WeaponLogic>().OnAttackStart += Attack;
        m_damageSource.gameObject.SetActive(false);
    }

    void Attack() {
        animator.SetTrigger("Attack");
    }

    void activateDamageSource() {
        // Animation Events
        m_damageSource.gameObject.SetActive(true);
        m_damageSource.InitDamageSourceInfo(GetComponentInParent<PlayerLogic>().getPlayerID(), Damage);        
    }

    void deactivateDamageSource() {
        // Animation Events
        m_damageSource.gameObject.SetActive(false);             
    }


}
