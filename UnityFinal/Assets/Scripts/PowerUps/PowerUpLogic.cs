using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpID {
    None = 0,
    INKY = 1,
    Lambert = 2,
    Mine = 3,
    Missile = 4,
    Poison = 5,
    PowerUp = 6,
    Shield = 7,
    SpeedUp = 8,
    Wall = 9,
}

// Handle the power up casting
// The power up is set as the child of the player
// transform.parent should be the player
public class PowerUpLogic : MonoBehaviour
{
    [SerializeField]
    PowerUpID m_powerUpID;

    public delegate void CastEvent();
    public event CastEvent OnCastStart;
    public event CastEvent OnCastFinish;
    
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cast() {                
        if (OnCastStart != null) {
            OnCastStart();
        }
    }

    public void finishCast(){
        if (OnCastFinish != null) {
            OnCastFinish();
        }
    }

    public PowerUpID GetPowerUpID()
    {        
        return m_powerUpID;
    }
}
