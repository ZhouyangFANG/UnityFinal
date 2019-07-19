using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSummonLogic : MonoBehaviour
{
    [SerializeField]
    GameObject hurtTrapPrefab = null;
    [SerializeField]
    GameObject slowTrapPrefab = null;
    // Start is called before the first frame update
    BlockLogic block;
    void Start()
    {
        block = GetComponent<BlockLogic>();
    }

    // Update is called once per frame
        
    public void summonTrap() {        
        if (block.isSummonable()) {
            if (Random.Range(0,2) == 0) {                
                GameObject newTrap = Instantiate(hurtTrapPrefab, transform);                
                block.setTrap(newTrap);                
            } else {
                GameObject newTrap = Instantiate(slowTrapPrefab, transform);                
                block.setTrap(newTrap);                
            }
        }
    }    

    public void destroyTrap() {
        // start to destroy the obstacle
        
        if (block.getTrap()) {
            block.getTrap().GetComponent<TrapLogic>().startDestroy();                     
            // only when the animation is done will the block got really destroyed
        }
    }

    public void swapRandomTrapState() {
        // Swap between State, Used in RandomEventManager
        if (block.getTrap()) {
            if (block.getTrap().GetComponent<TrapLogic>().isSteady()) {
                destroyTrap();
            }
        } else {
            summonTrap();
        }
    }
}
