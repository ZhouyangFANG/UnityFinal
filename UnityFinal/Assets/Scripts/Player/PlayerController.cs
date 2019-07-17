using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Control the player movement
// Only can handle the input directly here
// All animations about movement are implemented here
// Notice that we need two player.
// Save the playerLocation as two index[x][z] (which block the player is currently land on)

public enum PlayerID {
    Player1 = 0,
    Player2 = 1,
    // Player3 = 2,
    // Player4 = 3,
}

public class PlayerController : MonoBehaviour
{
    float MoveCoolDownTime = 0.15f;    
    float m_moveCoolDownTimer = 0;
    
    PlayerID m_playerID;

    private int xIndex;
    private int zIndex;
    public bool m_isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        m_isAttacking = false;
        m_moveCoolDownTimer = MoveCoolDownTime;
    }

    public void InitInfo(PlayerID id, int x, int z) {
        m_playerID = id;
        xIndex = x;
        zIndex = z;
        GetComponent<PlayerLogic>().InitInfo(id);
    }

    void Update() {   
        if (m_moveCoolDownTimer < MoveCoolDownTime) {
            m_moveCoolDownTimer += Time.deltaTime;
        }
        
        float m_horizontalMoveInput = Input.GetAxisRaw(m_playerID.ToString() + "_HorizontalMove");                
        float m_verticalMoveInput = Input.GetAxisRaw(m_playerID.ToString() + "_VerticalMove");
        bool isHorizontalMoving = Input.GetButton(m_playerID.ToString() + "_HorizontalMove");
        bool isVerticalMoving = Input.GetButton(m_playerID.ToString() + "_VerticalMove");
        bool isMoved = false;        
        

        if (!m_isAttacking && !(isHorizontalMoving && isVerticalMoving)) { // Prevent Moving Diagonally
            if ( isHorizontalMoving && m_moveCoolDownTimer >= MoveCoolDownTime) {
                
                if (m_horizontalMoveInput > 0) {
                    isMoved = TryMove(Direction.Right);
                } else {
                    isMoved = TryMove(Direction.Left);
                }

                if (isMoved) {
                    m_moveCoolDownTimer = 0;
                }
            }

            if ( isVerticalMoving && m_moveCoolDownTimer >= MoveCoolDownTime) {
                
                if (m_verticalMoveInput > 0) {
                    isMoved = TryMove(Direction.Up);
                } else {
                    isMoved = TryMove(Direction.Down);
                }

                if (isMoved) {
                    m_moveCoolDownTimer = 0;
                }
            }
        }       

        float m_horizontalFireInput = Input.GetAxisRaw(m_playerID.ToString() + "_HorizontalFire");                
        float m_verticalFireInput = Input.GetAxisRaw(m_playerID.ToString() + "_VerticalFire");
        bool isHorizontalFiring = Input.GetButton(m_playerID.ToString() + "_HorizontalFire");
        bool isVerticalFiring = Input.GetButton(m_playerID.ToString() + "_VerticalFire");


        if (!(isHorizontalFiring && isVerticalFiring)) { // Prevent Vertical Fire            
            if (isHorizontalFiring) {
                if (m_horizontalFireInput > 0) {
                    TryAttack(Direction.Right);
                } else {
                    TryAttack(Direction.Left);
                }        
            }

            if (isVerticalFiring) {
                if (m_verticalFireInput > 0) {
                    TryAttack(Direction.Up);
                } else {
                    TryAttack(Direction.Down);
                }
            }
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    
    bool TryMove(Direction direction) {
        // return true if it successfully moved
        if (transform.parent.gameObject.GetComponent<BlockLogic>().isEdge[(int)direction]) {
            return false;
        }
        int new_x_index = xIndex;
        int new_z_index = zIndex;
        switch(direction) {
            case Direction.XPlus:
                transform.rotation = Quaternion.LookRotation(Vector3.right);
                // Below should have something todo with Animations
                new_x_index += 1;
            break;
            case Direction.XMinus:
                transform.rotation = Quaternion.LookRotation(Vector3.left);       
                new_x_index -= 1;
            break;
            case Direction.ZPlus:
                transform.rotation = Quaternion.LookRotation(Vector3.forward);
                new_z_index += 1;
            break;
            case Direction.ZMinus:
                transform.rotation = Quaternion.LookRotation(Vector3.back);                
                new_z_index -= 1;
            break;
        }

        if (MapLogic.Instance.getBlock(new_x_index, new_z_index).GetComponent<BlockLogic>().isWalkable()) {
            xIndex = new_x_index;
            zIndex = new_z_index;
            transform.SetParent(MapLogic.Instance.getBlock(xIndex, zIndex).transform, false);
            return true;
        }
        return false;
    }

    bool TryAttack(Direction direction) {
        switch(direction) {
            case Direction.XPlus:
                transform.rotation = Quaternion.LookRotation(Vector3.right);
                // Below should have something todo with Animations
            break;
            case Direction.XMinus:
                transform.rotation = Quaternion.LookRotation(Vector3.left);       
            break;
            case Direction.ZPlus:
                transform.rotation = Quaternion.LookRotation(Vector3.forward);                
            break;
            case Direction.ZMinus:
                transform.rotation = Quaternion.LookRotation(Vector3.back);                                
            break;
        }

        if (GetComponentInChildren<WeaponLogic>()) {
            
            return GetComponentInChildren<WeaponLogic>().TryAttack();
        }
        return false;
    }
    
}
