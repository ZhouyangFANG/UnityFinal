using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Handle All Random Event on the map
// Summon or destroy obstacle, item, 
public class RandomEventLogic : MonoBehaviour
{    
    MapLogic map;
    // Start is called before the first frame update
    void Start()
    {
        map = MapLogic.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 500 == 100) {
            RandomChangeObstacleState();
            RandomGenerateItem();
            RandomChangeTrapState();
        }
    }

    void RandomChangeObstacleState() {
        
        int num = Random.Range(5, 10);
        for(int i = 0; i < num; i++) {
            GameObject block = map.getBlock(Random.Range(0, map.MapXBlockNum), Random.Range(0, map.MapZBlockNum));
            block.GetComponent<ObstacleSummonLogic>().swapRandomObstacleState();
        }
    }

    void RandomGenerateItem() {
        bool successFlag = false;
        
        while(!successFlag) {
            GameObject block = map.getBlock(Random.Range(0, map.MapXBlockNum), Random.Range(0, map.MapZBlockNum));
            successFlag = block.GetComponent<PickUpSummonLogic>().summonRandomItem();
        }
    }

    void RandomChangeTrapState() {
        int num = Random.Range(5, 10);
        for(int i = 0; i < num; i++) {
            GameObject block = map.getBlock(Random.Range(0, map.MapXBlockNum), Random.Range(0, map.MapZBlockNum));
            block.GetComponent<TrapSummonLogic>().swapRandomTrapState();
        }        
    }
}
