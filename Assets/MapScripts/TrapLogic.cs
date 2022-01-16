using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapType {
    Hurt = 0,
    Slow = 1,
}
public class TrapLogic : MonoBehaviour
{
    [SerializeField]
    TrapType Type;
    [SerializeField]
    GameObject slowDownEffector;
    [SerializeField]
    AudioClip TriggerSound;

    AudioSource audio;
    PlayerID m_sourcePlayerId = PlayerID.Nobody;
    float LifeTime = 20.0f;
    float m_lifeTimer = 0.0f;
    Animator animator;
    bool m_isSteady;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        m_isSteady = false;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (m_isSteady) {
            m_lifeTimer += Time.deltaTime;
            if (m_lifeTimer >= LifeTime) {
                startDestroy();                
            }
        }
    }
    public void setSourcePlayer(PlayerID id) {
        m_sourcePlayerId = id;
    }
    public void setLifeTime(float time) {
        LifeTime = time;
    }
    public void startDestroy() {
        animator.SetTrigger("Destroy");
        m_isSteady = false;
    }

    void Steady() {
        // Animation Event
        m_isSteady = true;
    }
    void Destroy() {
        // Animation Event
        Destroy(gameObject);
    }

    public bool isSteady() {
        return m_isSteady;
    }

    public void hitPlayer(PlayerLogic player) {
        if (player.getPlayerID() != m_sourcePlayerId) {
            if (Type == TrapType.Hurt) {
                player.takeDamage(1);
                animator.SetTrigger("Trigger");
                m_isSteady = false;       
            } else if (Type == TrapType.Slow) {
                player.takeEffector(slowDownEffector);
                startDestroy();
            }        
            if (!audio.isPlaying && TriggerSound) {
                audio.PlayOneShot(TriggerSound);
            }
        }
    }
}
