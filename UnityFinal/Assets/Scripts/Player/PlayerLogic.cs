using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Handle other logic for player rather than moving and attacking
public class PlayerLogic : MonoBehaviour
{
    PlayerID m_playerID;
    [SerializeField]
    GameObject [] meshRenderers = null;
    const int FullHp = 3;
    int m_hp;
    Animator m_animator;

    // [SerializeField]
    Transform m_leftHandTarget;

    // [SerializeField]
    Transform m_rightHandTarget;

    [SerializeField]
    bool m_isRightHandIKActive = true;

    [SerializeField]
    bool m_isLeftHandIKActive = true;
    bool m_isInvincible;
    const float InvincibleAfterDamageTime = 1.3f;
    float m_invincibleAfterDamageTimer = 0;

    GameObject m_takingPowerUp; // Ready to be cast
    WeaponLogic m_takingWeapon;
    // Start is called before the first frame update

    [SerializeField]
    AudioClip HPSounds;

    [SerializeField]
    AudioClip GetItemSounds;

    AudioSource m_AudioSource;


    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
        m_hp = FullHp;
        m_invincibleAfterDamageTimer = InvincibleAfterDamageTime;        
    }

    public void InitInfo(PlayerID id) {
        m_playerID = id;
    }
    void UpdateHandIK(bool isActive, Transform target, AvatarIKGoal avatarIKGoal)
    {
        if(isActive && target)
        {
            m_animator.SetIKPositionWeight(avatarIKGoal, 1);
            m_animator.SetIKRotationWeight(avatarIKGoal, 1);
            
            m_animator.SetIKPosition(avatarIKGoal, target.position);
            m_animator.SetIKRotation(avatarIKGoal, target.rotation);
        }
        else
        {
            m_animator.SetIKPositionWeight(avatarIKGoal, 0);
            m_animator.SetIKRotationWeight(avatarIKGoal, 0);
        }
    }
    void OnAnimatorIK()
    {
        if (m_animator)
        {
            UpdateHandIK(m_isRightHandIKActive,m_rightHandTarget, AvatarIKGoal.RightHand);
            UpdateHandIK(m_isLeftHandIKActive, m_leftHandTarget, AvatarIKGoal.LeftHand);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_invincibleAfterDamageTimer < InvincibleAfterDamageTime) {
            m_invincibleAfterDamageTimer += Time.deltaTime;
            if (Time.frameCount % 4 == 0) {
                for (int i = 0; i < meshRenderers.Length; i++) {
                    meshRenderers[i].GetComponent<SkinnedMeshRenderer>().enabled = !meshRenderers[i].GetComponent<Renderer>().enabled;
                }
            }
        } else {
            for (int i = 0; i < meshRenderers.Length; i++) {
                meshRenderers[i].GetComponent<SkinnedMeshRenderer>().enabled = true;
            }
        }
    }

    void UpdateInvincibleAppearance() {        
             
    }

    // Receive the damage from the block logic
    public void takeDamage(DamageSourceLogic damageSource) {
        if (damageSource.getSourcePlayerID() != m_playerID) {
            // Prevent damage to self (if sourcePlayerId is set)
            if (m_invincibleAfterDamageTimer >= InvincibleAfterDamageTime) {
                m_invincibleAfterDamageTimer = 0.0f;
                // Update the player appearance here (invincible)
                Debug.Log(m_playerID.ToString() + " is damaged for 1 hp");
                damageSource.gameObject.SetActive(false);
                m_hp -= damageSource.getDamage();
                if (m_hp <= 0) {
                    Death();
                }
            }
        }
    }

    public void takeDamage(int damage) {
        // Prevent damage to self (if sourcePlayerId is set)
        ShieldLogic shield = GetComponentInChildren<ShieldLogic>();
        
        if (shield) {
            Destroy(shield.gameObject);
            return;
        }

        if (m_invincibleAfterDamageTimer >= InvincibleAfterDamageTime) {
            m_invincibleAfterDamageTimer = 0.0f;
            // Update the player appearance here (invincible)
            Debug.Log(m_playerID.ToString() + " is damaged for 1 hp");                
            m_hp -= damage;
            if (m_hp <= 0) {
                Death();
            }
        }
    }

    void Death() {
        GameObject.Find("Canvas").GetComponent<HUDManager>().DestoryPlayerHUD((int)getPlayerID());
        Destroy(gameObject);
    }

    void TakeWeapon(GameObject weapon) {
        // weapon is a prefab

        if (m_AudioSource && GetItemSounds) {
            m_AudioSource.PlayOneShot(GetItemSounds);
            // Debug.Log("Sounds out");
        }
        
        if (GetComponentsInChildren<WeaponLogic>().Length != 0) {
            // the player is holding other weapon
            // destroy them before get new weapon
            foreach(WeaponLogic currentWeapon in GetComponentsInChildren<WeaponLogic>()) {
                Destroy(currentWeapon.gameObject);
            }
        }

        m_takingWeapon = Instantiate(weapon, transform).GetComponent<WeaponLogic>();
        m_leftHandTarget = m_takingWeapon.LeftHandTarget;
        m_rightHandTarget = m_takingWeapon.RightHandTarget;
        m_takingWeapon.OnAttackStart += AttackStarted;
        m_takingWeapon.OnAttackFinish += AttackFinished;
    }

    void TakePowerUp(GameObject powerUp) {
        // Save the pickup currently
        if (m_AudioSource && GetItemSounds) {
            m_AudioSource.PlayOneShot(GetItemSounds, 0.7f);
            // Debug.Log("Sounds out");
        }
        m_takingPowerUp = powerUp;
        
    }

    public void takePickUp(PickUpLogic item) {        
        if (item.isWeapon()) {
            TakeWeapon(item.getItemPrefab());
        } else if (item.isPowerUp()) {
            TakePowerUp(item.getItemPrefab());
        } else if (item.isHp()) {
            TakeHp();
        }
        Destroy(item.gameObject);
    }

    void TakeHp() {
        if (m_hp < FullHp) {
            ParticleEffectManager.Instance.playPlayerPickUpHeartEffect(transform.position + new Vector3(0, 4.0f, 0));
            if (m_AudioSource && HPSounds) {
                m_AudioSource.PlayOneShot(HPSounds);
                // Debug.Log("Sounds out");
            }
            m_hp += 1;
            Debug.Log(m_playerID.ToString() + " is Recovered for 1 hp");
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

    public void castCurrentTakingPowerUp() {
        if (m_takingPowerUp) {
            takeEffector(m_takingPowerUp);
            m_takingPowerUp = null;
        }
    }

    public void takeEffector(GameObject powerUpEffector) {
        GameObject powerUp = Instantiate(powerUpEffector, transform);
        powerUp.GetComponent<PowerUpLogic>().cast();
    }

    public int GetHP()
    {
        return m_hp;
    }

    public WeaponID GetWeaponID()
    {
        if(m_takingWeapon)
        {
            return m_takingWeapon.GetWeaponID();
        }
        else
            return 0;
    }

    public PowerUpID GetPowerUpID()
    {
        if(m_takingPowerUp)
        {
            return m_takingPowerUp.GetComponent<PowerUpLogic>().GetPowerUpID();
        }
        else
            return 0;
    }
}
