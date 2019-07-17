using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The basic logic of a block (where player can land)
// Each block got this scripts
// All animation on the block are implemented here
// Pass damage, available information of the block
// Save the location(which is also the index to access from maplogic)
public enum Direction {
    // From 0 to 3
    XPlus = 0,
    XMinus = 1,
    ZPlus = 2,
    ZMinus = 3,
    Left = 1,
    Right = 0,
    Up = 2,
    Down = 3
}

public class BlockLogic : MonoBehaviour
{
    // Start is called before the first frame update
    int x_index;
    int z_index;
    MeshFilter meshFilter;
    bool m_walkable;
    [SerializeField]
    Mesh onAttackMesh;
    Mesh defaultMesh;
    public bool [] isEdge = new bool [4];
    
    void Start()
    {
        x_index = 0;
        z_index = 0;
        m_walkable = true;
        meshFilter = GetComponent<MeshFilter>();
        defaultMesh = meshFilter.mesh;

    }
    
    public void InitBlockInfo(int x, int z, bool isEdgeXPlus, bool isEdgeXMinus, bool isEdgeZPlus, bool isEdgeZMinus) {
        // Save the index and edge info that can be accessed from map
        // Only be called at the time when the block is generate
        x_index = x;
        z_index = z;
        isEdge[(int)Direction.XPlus] = isEdgeXPlus;
        isEdge[(int)Direction.XMinus] = isEdgeXMinus;
        isEdge[(int)Direction.ZPlus] = isEdgeZPlus;
        isEdge[(int)Direction.ZMinus] = isEdgeZMinus;        
    }


    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponentInChildren<PlayerLogic>() != null) {
            m_walkable = false;
        } else {
            m_walkable = true;
        }
    }

    void HandleDamageSource(GameObject damageSource) 
    {
        // Update the appearance of the block
        // Pass the damage source to player
        PlayerLogic player = GetComponentInChildren<PlayerLogic>();
        if (player) {
            player.takeDamage(damageSource);
        }
    }


    public bool isWalkable() {
        return m_walkable;
    }


    void ChangeBlockAppearanceOnAttack() {
        if(onAttackMesh) {
            meshFilter.mesh = onAttackMesh;
        }
    }

    void ResetBlockAppearace() {
        if (meshFilter) {
            meshFilter.mesh = defaultMesh;
        }
    }

    private void OnTriggerEnter(Collider other) {        
        if (other.tag == "DamageSource") {            
            // Change the block appearence if there is damageSource in the trigger of the block
            ChangeBlockAppearanceOnAttack();
            other.gameObject.GetComponent<DamageSourceLogic>().OnDisableEvent += ResetBlockAppearace;            
        } 
    }

    private void OnTriggerStay(Collider other) {        
        if (other.tag == "DamageSource") {
            // if there is damageSource in the trigger of the block
            HandleDamageSource(other.gameObject);
        } 
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "DamageSource") {
            // if there is damageSource in the trigger of the block
            ResetBlockAppearace();
        }  
    }
}
