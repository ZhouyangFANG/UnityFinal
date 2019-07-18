using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Handle the power up casting
// The power up is set as the child of the player
// transform.parent should be the player
public class PowerUpLogic : MonoBehaviour
{

    public delegate void CastEvent();
    public event CastEvent OnCastStart;
    public event CastEvent OnCastFinish;
    
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cast() {
        
        Debug.Log("Here");
        if (OnCastStart != null) {
            OnCastStart();
        }
    }

    public void finishCast(){
        if (OnCastFinish != null) {
            OnCastFinish();
        }
    }

}
