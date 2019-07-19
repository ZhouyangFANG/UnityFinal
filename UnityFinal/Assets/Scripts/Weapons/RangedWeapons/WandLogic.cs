using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandLogic : MonoBehaviour
{
    Animator animator;
    
    [SerializeField]
    int Damage = 1;
    [SerializeField]
    int BulletSpeed = 1;
    [SerializeField]
    Transform shootPosLeft = null;
    [SerializeField]
    Transform shootPosMid = null;
    [SerializeField]
    Transform shootPosRight = null;

    
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
        GameObject bullet1 = Instantiate(bulletPrefab, shootPosLeft.position, shootPosLeft.rotation); // Generate a bullet at the shootPos
        bullet1.GetComponent<BulletLogic>().InitBulletInfo(GetComponentInParent<PlayerLogic>().getPlayerID(), Damage, BulletSpeed); // Initialize the bullet information

        GameObject bullet2 = Instantiate(bulletPrefab, shootPosMid.position, shootPosMid.rotation); // Generate a bullet at the shootPos
        bullet2.GetComponent<BulletLogic>().InitBulletInfo(GetComponentInParent<PlayerLogic>().getPlayerID(), Damage, BulletSpeed); // Initialize the bullet information

        GameObject bullet3 = Instantiate(bulletPrefab, shootPosRight.position, shootPosRight.rotation); // Generate a bullet at the shootPos
        bullet3.GetComponent<BulletLogic>().InitBulletInfo(GetComponentInParent<PlayerLogic>().getPlayerID(), Damage, BulletSpeed); // Initialize the bullet information
    }
    
}
