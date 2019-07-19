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
    Animator animator;
    bool m_isSteady;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        m_isSteady = false;
    }

    // Update is called once per frame
    
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

    public void hitPlayer(GameObject player) {
        if (Type == TrapType.Hurt) {
            player.GetComponent<PlayerLogic>().takeDamage(1);
            animator.SetTrigger("Trigger");
            m_isSteady = false;
        } else if (Type == TrapType.Slow) {
            player.GetComponent<PlayerLogic>().takeEffector(slowDownEffector);
            startDestroy();
        }        
    }
}
