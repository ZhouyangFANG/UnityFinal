using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use to generate the blocks given size from the origin
// Reference all blocks in a 2 by 2 array
// Pass the edge information to other scripts
public class MapLogic : MonoBehaviour
{

    // Singleton as only one map can be activate at one time
    public static MapLogic Instance;
    void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        } else {
            Instance = this;
        }
    }


    [SerializeField]
    public int SizeOfBlock = 5; // Better to be integer, this should corresponding to the size block prefab


    // This two parameters decide how big is the map;
    [SerializeField]
    public int MapXBlockNum = 16;

    [SerializeField]
    public int MapZBlockNum = 12;
    int m_playerNum;

    [SerializeField]
    private GameObject basicBlockPrefab = null; // Prefab of a block With BlockLogic Script Attach to it
    [SerializeField]
    private GameObject player1Prefab = null;
    [SerializeField]    
    private GameObject player2Prefab = null;
    [SerializeField]    
    private GameObject player3Prefab = null;
    private GameObject [][] m_blocks;
    public GameObject [] m_players;
    // Start is called before the first frame update
    void Start()
    {
        m_playerNum = GameManager.Instance.getPlayer();
        GenerateMap();
        SummonPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void GenerateMap() {
        // Initialize m_blocks and Generate the Map
        // Call at Awake
        const float mapDepth = -5.0f;
        m_blocks = new GameObject[MapXBlockNum][];
        int x = 0, z = 0, x_index = 0, z_index = 0;
        for (x = 0; x < SizeOfBlock * MapXBlockNum; x += SizeOfBlock) {
            m_blocks[x_index] = new GameObject[MapZBlockNum];
            
            for (z = 0; z < SizeOfBlock * MapZBlockNum; z+= SizeOfBlock) {
                GameObject newBlock = Instantiate(basicBlockPrefab, transform.position + new Vector3(x, mapDepth, z), transform.rotation);
                newBlock.name = "BasicBlock" + "(" + x_index + "," + z_index + ")";
                newBlock.transform.parent = this.transform;
                newBlock.GetComponent<BlockLogic>().InitBlockInfo(x_index, z_index, x_index == MapXBlockNum - 1, x_index == 0, z_index == MapZBlockNum - 1, z_index == 0);
                m_blocks[x_index][z_index] = newBlock;
                z_index += 1;
            }

            x_index += 1;
            z_index = 0;
        }        
    }

    void SummonPlayer() {        
        // Generate two for now
        m_players = new GameObject [m_playerNum];
        
        const float playerHeight = 0;
        Transform Trans1 = m_blocks[0][0].transform;
        
        GameObject Player1 = Instantiate(player1Prefab, Trans1.position + new Vector3(0, playerHeight, 0), Trans1.rotation);
        Player1.transform.parent = Trans1;
        Player1.name = "Player1";
        Player1.tag = "Player";
        Player1.GetComponent<PlayerController>().InitInfo(PlayerID.Player1, 0, 0);
        m_players[(int)PlayerID.Player1] = Player1;
        Trans1.gameObject.GetComponent<BlockLogic>().setPlayer(Player1);

        Transform Trans2 = m_blocks[MapXBlockNum - 1][MapZBlockNum - 1].transform;
        GameObject Player2 = Instantiate(player2Prefab, Trans2.position + new Vector3(0, playerHeight, 0), Trans2.rotation);
        Player2.transform.parent = Trans2;
        Player2.name = "Player2";
        Player2.tag = "Player";
        Player2.GetComponent<PlayerController>().InitInfo(PlayerID.Player2, MapXBlockNum - 1, MapZBlockNum - 1);
        m_players[(int)PlayerID.Player2] = Player2;
        Trans2.gameObject.GetComponent<BlockLogic>().setPlayer(Player2);

        if (m_playerNum >= 3) {
            Transform Trans3 = m_blocks[0][MapZBlockNum - 1].transform;
            GameObject Player3 = Instantiate(player3Prefab, Trans3.position + new Vector3(0, playerHeight, 0), Trans3.rotation);
            Player3.transform.parent = Trans3;
            Player3.name = "Player3";
            Player3.tag = "Player";
            Player3.GetComponent<PlayerController>().InitInfo(PlayerID.Player3, 0, MapZBlockNum - 1);
            m_players[(int)PlayerID.Player3] = Player3;
            Trans3.gameObject.GetComponent<BlockLogic>().setPlayer(Player3);

        }
    }

    public GameObject getBlock(int x, int z) {
        // Return the block
        if (x >= 0 && x <= MapXBlockNum - 1) {
            if (z >= 0 && z <= MapZBlockNum - 1) {
                if (m_blocks[x][z]) {
                    return m_blocks[x][z];
                }
            }
        }
        Debug.LogError("MapLogic: Invalid access to block("+ x + "," + z + ")");
        return null;
    }

    public GameObject getPlayer(PlayerID id) {
        // Quickly return other player by id
        if (PlayerID.IsDefined(typeof(PlayerID),id))  {
            if (m_players[(int)id]) {
                return m_players[(int)id];
            }
        }
        Debug.LogError(("MapLogic: Invalid access to player " + id));
        return null;
    }
}
