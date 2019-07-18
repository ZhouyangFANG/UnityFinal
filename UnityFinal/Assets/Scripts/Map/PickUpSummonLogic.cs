using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSummonLogic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject [] PickUpsPrefabList = null;
    BlockLogic block;
    void Start()
    {
        block = GetComponent<BlockLogic>();
    }

    public bool summonRandomItem() {        
        if (block.isSummonable()) {
            if (PickUpsPrefabList.Length > 0) {
                GameObject newItem = Instantiate(PickUpsPrefabList[Random.Range(0, PickUpsPrefabList.Length)], transform);                
                block.setItem(newItem);
            }
            return true;
        }
        return false;
    }
}
