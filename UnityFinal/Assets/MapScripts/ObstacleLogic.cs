using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Obstacle Logic for those which cannot be destroyed
public class ObstacleLogic : MonoBehaviour
{
    [SerializeField]
    int Hp = -1; // the count of hit the obstacle required to be destroy, negative number means cannot destroyed by player
    [SerializeField]
    bool isDestroyable;
    Animator animator;
    bool m_isSteady; // Whether is animating or not

    [SerializeField]
    AudioClip DestroySounds;
    [SerializeField]
    AudioClip UndestroyableSounds;

    AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        m_AudioSource = GetComponent<AudioSource>();
        m_isSteady = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0 && m_isSteady && isDestroyable) {
            startDestroy();
        }
    }

    public void startDestroy() {
        animator.SetTrigger("Destroy");
        
        m_isSteady = false;
    }

    void ResetObstacle() {
        if (GetComponentInParent<BlockLogic>()) { 
            GetComponentInParent<BlockLogic>().resetObstacle();
        }
    }
    public void takeDamage(DamageSourceLogic damageSource) {
        if (damageSource.isMissile()) {            
            if (m_AudioSource && DestroySounds && !m_AudioSource.isPlaying) {
                m_AudioSource.PlayOneShot(DestroySounds, 0.4f);
                // Debug.Log("Sounds out");
            }  
            startDestroy();
        }  else {
            
            if (Hp > 0 && m_AudioSource && DestroySounds && !m_AudioSource.isPlaying) {
                m_AudioSource.PlayOneShot(DestroySounds);
                // Debug.Log("Sounds out");
            }  
            if (Hp < 0 && m_AudioSource && UndestroyableSounds && !m_AudioSource.isPlaying) {
                m_AudioSource.PlayOneShot(UndestroyableSounds);
                // Debug.Log("Sounds out");
            } 

            Hp -= damageSource.getDamage(); 
        }
    }

    void Destroy() {
        // Animation Event        
        Destroy(gameObject);
    }

    void Steady() {
        // Animaton Event
        m_isSteady = true;
    }

    public bool isSteady() {
        return m_isSteady;
    }
}
