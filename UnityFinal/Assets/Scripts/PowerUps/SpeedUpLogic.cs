using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpeedUpLogic : MonoBehaviour
{
    const float EffectTime = 5.0f;
    float m_effectTimer;
    bool isActivated;
    PlayerController player;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<PowerUpLogic>().OnCastStart += Cast;
        player = transform.parent.GetComponent<PlayerController>();
        SpeedUpLogic [] list1 = player.GetComponentsInChildren<SpeedUpLogic>();
        SpeedDownLogic [] list2 = player.GetComponentsInChildren<SpeedDownLogic>();
        for (int i = 0; i < list1.Length; i++) {
            if (list1[i] != this) {
                Destroy(list1[i].gameObject);
            }
        }
        
        for (int i = 0; i < list2.Length; i++) {
            if (list2[i] != this) {
                Destroy(list2[i].gameObject);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        m_effectTimer += Time.deltaTime;
        if (m_effectTimer > EffectTime) {
            ResetEffect();
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

    private void OnDestroy() {
         
    }
}
