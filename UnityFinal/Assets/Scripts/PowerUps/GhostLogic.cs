using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GhostLogic : MonoBehaviour
{
    [SerializeField]
    Material Player1Mat;
    [SerializeField]
    Material Player2Mat;
    [SerializeField]
    Material Player3Mat;

    Material userMat;
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
        if (GetComponentInParent<PlayerLogic>().getPlayerID() == PlayerID.Player1) {
            Color temp = Player1Mat.color;
            Player1Mat.color = new Color(temp.r, temp.g, temp.b, 0.3f);
            userMat = Player1Mat;
        } else if (GetComponentInParent<PlayerLogic>().getPlayerID() == PlayerID.Player2) {
            Color temp = Player2Mat.color;
            Player2Mat.color = new Color(temp.r, temp.g, temp.b, 0.3f);
            userMat = Player2Mat;
        } else {
            Debug.LogError("GhostLogic: No Mat Found");
        }
    }

    private void OnDestroy() {
        Color temp = userMat.color;
        userMat.color = new Color(temp.r, temp.g, temp.b, 1);
    }
}
