using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSummonLogic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject [] PickUpsPrefabList = null;
    [SerializeField]
    GameObject RandomItemPrefab = null;
    [SerializeField]
    bool CanSummonRandomItem;
    BlockLogic block;
    void Start()
    {
        block = GetComponent<BlockLogic>();
        CanSummonRandomItem = true;
    }

    public bool summonRandomItem() {
        if (block.isSummonable()) {            
            if (CanSummonRandomItem && Random.Range(0,3) == 0) {
                summonRandomUnknownItem();
            } else {
                summonRandomKnownItem();
            }
            return true;
        }
        return false;
    }

    void summonRandomKnownItem() {
        if (PickUpsPrefabList.Length > 0) {
            GameObject newItem = Instantiate(PickUpsPrefabList[Random.Range(0, PickUpsPrefabList.Length)], transform);                
            block.setItem(newItem);
        }                
    }

    void summonRandomUnknownItem() {
        if (PickUpsPrefabList.Length > 0) {
            PickUpLogic newItem = PickUpsPrefabList[Random.Range(0, PickUpsPrefabList.Length)].GetComponent<PickUpLogic>();
            PickUpLogic newRandomPickUp = Instantiate(RandomItemPrefab, transform).GetComponent<PickUpLogic>();
            newRandomPickUp.setSecretPickUp(newItem);
            block.setItem(newRandomPickUp.gameObject);
        }        
    }
}
