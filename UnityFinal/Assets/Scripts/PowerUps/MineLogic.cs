using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MineLogic : MonoBehaviour
{
    [SerializeField]
    GameObject MinePrefab = null;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<PowerUpLogic>().OnCastStart += Cast;     
    }

    // Update is called once per frame

    void Cast() {
        PlayerID playerId = GetComponentInParent<PlayerLogic>().getPlayerID(); 
        BlockLogic block = GetComponentInParent<PlayerLogic>().GetComponentInParent<BlockLogic>();
        TrapLogic mine = Instantiate(MinePrefab, block.gameObject.transform).GetComponent<TrapLogic>();
        mine.setSourcePlayer(playerId);
        block.setTrap(mine.gameObject);
        Destroy(gameObject);
    }

    private void OnDestroy() {

    }
}
