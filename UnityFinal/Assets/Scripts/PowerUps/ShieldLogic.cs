using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShieldLogic : MonoBehaviour
{
    const float EffectTime = 5.0f;
    float m_effectTimer;    
    // Start is called before the first frame update
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

    void Cast() {         
        m_effectTimer = 0;
    }

    private void OnDestroy() {

    }
}
