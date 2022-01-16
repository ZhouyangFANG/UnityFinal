using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{    
    float bulletSpeed = 0f;
    [SerializeField]
    DamageSourceLogic[] damageSource = null;
    [SerializeField]
    Transform m_forwardDirection;
    // Start is called before the first frame update
    void Start()
    {
   
    }

    public void InitBulletInfo(PlayerID sourcePlayer, int damage, float bullet_speed) {    
        bulletSpeed = bullet_speed;
        for (int index = 0; index < damageSource.Length; ++ index) {
            damageSource[index].InitDamageSourceInfo(sourcePlayer, damage);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position + m_forwardDirection.forward * Time.deltaTime * bulletSpeed; // Move Forward
    }
}
