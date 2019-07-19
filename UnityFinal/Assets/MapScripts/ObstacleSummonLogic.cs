using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach to the basic block to handle the obstacle
public class ObstacleSummonLogic : MonoBehaviour
{
    [SerializeField]
    GameObject obstacleDestroyablePrefab = null;    
    [SerializeField]
    GameObject obstacleNotDestroyablePrefab = null;    
    BlockLogic block;
    // Start is called before the first frame update
    void Start()
    {
         block = GetComponentInParent<BlockLogic>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void summonObstacle(GameObject obstacle) {
        // summon the obstacle with given prefab
        // the prefab should be animated itself
        if (block.isSummonable()) {            
            block.setObstacle(Instantiate(obstacle, transform)); // The prefab should play the summon animation automatically
        }
        
    }

    public void destroyObstacle() {
        // start to destroy the obstacle
        
        if (block.getObstacle()) {            
            block.getObstacle().GetComponent<ObstacleLogic>().startDestroy();                     
            // only when the animation is done will the block got really destroyed
        }
    }

    public void swapRandomObstacleState() {
        // Swap between State, Used in RandomEventManager
        if (block.getObstacle()) {
            if (block.getObstacle().GetComponent<ObstacleLogic>().isSteady()) {
                destroyObstacle();
            }
        } else {
            if (Random.Range(0, 3) == 0) {
                summonObstacle(obstacleNotDestroyablePrefab);
            } else {
                summonObstacle(obstacleDestroyablePrefab);
            }
        }
    }
}
