using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon = 0,    
    PowerUps = 1,
}

// Attached to pickupable items
// Handle the pickup event
public class ItemLogic : MonoBehaviour
{
    [SerializeField]
    public GameObject objectPrefab = null;
    [SerializeField]
    ItemType m_itemType = ItemType.Weapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent) {
            PlayerLogic player = transform.parent.GetComponent<PlayerLogic>(); // Check if player is on the same block
            if (player) {
                if (m_itemType == ItemType.Weapon) { 
                    player.takeWeapon(objectPrefab);
                }
            }
        }
    }

    // Player pick up the item    

    private void OnTriggerEnter(Collider other) {
        // Used for test
        if (other.gameObject.tag == "Player") {
            if (m_itemType == ItemType.Weapon) {
                other.gameObject.GetComponent<PlayerLogic>().takeWeapon(objectPrefab);
            }
        }
        Destroy(gameObject);
    }

}
