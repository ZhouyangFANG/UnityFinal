using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolLogic : MonoBehaviour
{
    Animator animator;
    
    [SerializeField]
    int Damage = 1;
    [SerializeField]
    int BulletSpeed = 1;
    [SerializeField]
    Transform shootPos = null;

    
    [SerializeField]
    GameObject bulletPrefab = null;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<WeaponLogic>().OnAttackStart += Attack;
        // WeaponLogic event   
    }

    void Attack() {
        animator.SetTrigger("Attack");
        GameObject bullet = Instantiate(bulletPrefab, shootPos.position, shootPos.rotation); // Generate a bullet at the shootPos
        bullet.GetComponent<BulletLogic>().InitBulletInfo(GetComponentInParent<PlayerLogic>().getPlayerID(), Damage, BulletSpeed); // Initialize the bullet information
    }
    
}
