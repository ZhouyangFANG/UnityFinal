using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpeedUpLogic : MonoBehaviour
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
            Debug.Log("PowerUpEffectorDestroyed");
            Destroy(gameObject);
        }     
    }

    void Cast() {          
        m_effectTimer = 0;
        ApplySpeedUpEffect();        
    }


    void ApplySpeedUpEffect() {            
        transform.parent.GetComponent<PlayerController>().applySpeedUp();
    }

    void ResetEffect() {
        transform.parent.GetComponent<PlayerController>().resetSpeed();
    }
}
