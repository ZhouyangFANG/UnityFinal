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

    GameObject m_Item;

    float lifetime;

    void Start()
    {
        block = GetComponent<BlockLogic>();        
    }

    void FixedUpdate()
    {
        if(block.getPickUp())
        {
            lifetime -= Time.deltaTime;
            if(lifetime < 0 && m_Item)
            {
                block.resetItem();
                Destroy(m_Item);
                m_Item = null;
            }
        }
    }

    public bool summonRandomItem() {
        if (block.isSummonable()) {            
            if (CanSummonRandomItem && Random.Range(0,5) == 0) {
                summonRandomUnknownItem();
            } else {
                summonRandomKnownItem();
            }
            lifetime = Random.Range(5.0f, 10.0f);
            return true;
        }
        return false;
    }

    void summonRandomKnownItem() {
        if (PickUpsPrefabList.Length > 0) {
            m_Item = Instantiate(PickUpsPrefabList[Random.Range(0, PickUpsPrefabList.Length)], transform);            
            block.setItem(m_Item);
        }
    }

    void summonRandomUnknownItem() {
        if (PickUpsPrefabList.Length > 0) {
            PickUpLogic newItem = PickUpsPrefabList[Random.Range(0, PickUpsPrefabList.Length)].GetComponent<PickUpLogic>();
            PickUpLogic newRandomPickUp = Instantiate(RandomItemPrefab, transform).GetComponent<PickUpLogic>();
            newRandomPickUp.setSecretPickUp(newItem);
            m_Item = newRandomPickUp.gameObject;
            block.setItem(newRandomPickUp.gameObject);
        }        
    }
}
