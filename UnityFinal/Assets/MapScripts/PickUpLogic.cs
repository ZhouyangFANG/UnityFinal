using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickUpType
{
    Weapon = 0,    
    PowerUp = 1,
    Hp = 2,
}

// Attached to pickupable items
// Handle the pickup event
public class PickUpLogic : MonoBehaviour
{
    [SerializeField]
    GameObject objectPrefab = null;
    [SerializeField]
    PickUpType m_itemType = PickUpType.Weapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // May have better choice to handle
        // if (transform.parent) {
        //     PlayerLogic player = transform.parent.GetComponent<PlayerLogic>(); // Check if player is on the same block
        //     if (player) {
        //         if (m_itemType == ItemType.Weapon) { 
        //             player.takeWeapon(objectPrefab);
        //         }
        //     }
        // }
    }

    // Player pick up the item    
    public bool isWeapon() {
        return m_itemType == PickUpType.Weapon;
    }

    public bool isPowerUp() {
        return m_itemType == PickUpType.PowerUp;
    }

    public bool isHp() {
        return m_itemType == PickUpType.Hp;
    }

    public GameObject getItemPrefab() {
        return objectPrefab;
    }

    public void setSecretPickUp(PickUpLogic pickUp) {
        objectPrefab = pickUp.getItemPrefab();
        m_itemType = pickUp.getItemType();
    }

    private PickUpType getItemType() {
        return m_itemType;
    }

    private void OnTriggerEnter(Collider other) {
        // // Used for test
        // if (other.gameObject.tag == "Player") {
        //     if (m_itemType == ItemType.Weapon) {
        //         other.gameObject.GetComponent<PlayerLogic>().takeWeapon(objectPrefab);
        //     }
        // }
        // Destroy(gameObject);
    }

}
