using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PoisonTrailLogic : MonoBehaviour
{
    const float EffectTime = 2.0f;
    float m_effectTimer;
    PlayerLogic player;
    // Start is called before the first frame update
    [SerializeField]
    GameObject slowDownTrapPrefab = null;
    void Awake()
    {
        GetComponent<PowerUpLogic>().OnCastStart += Cast;
        m_effectTimer = 0;        
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        m_effectTimer += Time.deltaTime;
        if (m_effectTimer > EffectTime) {            
            Destroy(gameObject);
        }
        
    }

    // See resetPlayer()
    public void summonSlowDownTrail(BlockLogic block) {
        if (block.isSummonable()) {
            TrapLogic trap = Instantiate(slowDownTrapPrefab, block.gameObject.transform).GetComponent<TrapLogic>();
            trap.setLifeTime(2.0f);
            block.setTrap(trap.gameObject);
        }
    } 
    void Cast() {         
        m_effectTimer = 0;
    }

    private void OnDestroy() {
        
    }
}
