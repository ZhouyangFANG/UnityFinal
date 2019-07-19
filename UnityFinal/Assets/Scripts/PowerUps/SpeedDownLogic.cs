using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpeedDownLogic : MonoBehaviour
{
    const float EffectTime = 5.0f;
    float m_effectTimer;
    bool isActivated;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<PowerUpLogic>().OnCastStart += Cast;        
    }

    // Update is called once per frame
    void Update()
    {        
        m_effectTimer += Time.deltaTime;
        if (m_effectTimer > EffectTime) {
            ResetEffect();
            Destroy(gameObject);
        }     
    }

    void Cast() {          
        m_effectTimer = 0;
        ApplySpeedDownEffect();        
    }

    void ApplySpeedDownEffect() {
        transform.parent.GetComponent<PlayerController>().applySpeedDown();
    }

    void ResetEffect() {
        transform.parent.GetComponent<PlayerController>().resetSpeed();
    }

    private void OnDestroy() {
        PlayerController player = transform.parent.GetComponent<PlayerController>();
        if (player) {
            player.resetSpeed();
        }    
    }
}
