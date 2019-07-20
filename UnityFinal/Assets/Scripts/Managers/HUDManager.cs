using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    GameObject m_gameManager;

    [SerializeField]
    int m_playernum;
    [SerializeField]
    GameObject [] m_player;
    PlayerID playerID;

    [SerializeField]
    GameObject [] Player_HUD;
    GameObject [] hp;
    int m_hp;
    WeaponID m_weaponID;
    PowerUpID m_powerUpID;

    GameObject m_weapon;
    Texture2D Transparent;
    Texture2D Dagger;
    Texture2D Claymore;
    Texture2D Spear;
    Texture2D Pistol;
    Texture2D Bow;
    Texture2D Wand;
    
    GameObject m_powerUp;
    Texture2D Inky;
    Texture2D Lambert;
    Texture2D Mine;
    Texture2D Missile;
    Texture2D Poison;
    Texture2D Shield;
    Texture2D SpeedUp;
    Texture2D Wall;

    // Start is called before the first frame update
    private void Awake() {
        Transparent = (Texture2D)Resources.Load("Transparent");
        Dagger = (Texture2D)Resources.Load("Dagger");
        Claymore = (Texture2D)Resources.Load("Claymore");
        Spear = (Texture2D)Resources.Load("Spear");
        Pistol = (Texture2D)Resources.Load("Pistol");
        Bow = (Texture2D)Resources.Load("Bow");
        Wand = (Texture2D)Resources.Load("Wand");

        Inky = (Texture2D)Resources.Load("Inky");
        Lambert = (Texture2D)Resources.Load("Lambert");
        Mine = (Texture2D)Resources.Load("Mine");
        Missile = (Texture2D)Resources.Load("Missile");
        Poison = (Texture2D)Resources.Load("Poison");
        Shield = (Texture2D)Resources.Load("Shield");
        SpeedUp = (Texture2D)Resources.Load("SpeedUp");
        Wall = (Texture2D)Resources.Load("Wall");    
    }

    void Start()
    {
        m_playernum = GameManager.Instance.getPlayer();
        Player_HUD = new GameObject[4];
        for(int i = 0; i < 4; i++)
        {
            Player_HUD[i] = GameObject.Find("Canvas/Player"+(i+1).ToString()+"_HUD");
            if(i < m_playernum) Player_HUD[i].SetActive(true);
            else Player_HUD[i].SetActive(false);
        }
        hp = new GameObject[3];
        m_player = MapLogic.Instance.m_players;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        for(int i = 0; i < m_playernum; i++)
        {
            UpdateHP(Player_HUD[i], 0);
        }
        for(int i = 0; i < m_playernum; i++)
        {
            GameObject player = MapLogic.Instance.getPlayer((PlayerID)i);
            if(player)
            {
                playerID = player.GetComponent<PlayerLogic>().getPlayerID();
                m_hp = player.GetComponent<PlayerLogic>().GetHP();
                m_weaponID = player.GetComponent<PlayerLogic>().GetWeaponID();
                m_powerUpID = player.GetComponent<PlayerLogic>().GetPowerUpID();
                UpdateHP(Player_HUD[(int)playerID], m_hp);
                UpdateWeapon(Player_HUD[(int)playerID], m_weaponID);
                UpdatePowerUp(Player_HUD[(int)playerID], m_powerUpID);
            }
        }
    }

    void UpdateHP(GameObject player_HUD, int player_hp)
    {
        for(int i = 0; i < 3; i++)
        {
            hp[i] = player_HUD.transform.Find("hp_"+i.ToString()).gameObject;
            if(i < player_hp)
            {
                hp[i].SetActive(true);
            }
            else
            {
                hp[i].SetActive(false);
            }
        }
    }

    void UpdateWeapon(GameObject player_HUD, WeaponID weaponID)
    {
        m_weapon = player_HUD.transform.Find("Weapon").gameObject;
        switch((int)weaponID)
        {
            case 0:
                m_weapon.GetComponent<RawImage>().texture = Transparent;
                break;
            case 1:
                m_weapon.GetComponent<RawImage>().texture = Dagger;
                break;
            case 2:
                m_weapon.GetComponent<RawImage>().texture = Claymore;
                break;
            case 3:
                m_weapon.GetComponent<RawImage>().texture = Spear;
                break;
            case 4:
                m_weapon.GetComponent<RawImage>().texture = Pistol;
                break;
            case 5:
                m_weapon.GetComponent<RawImage>().texture = Bow;
                break;
            case 6:
                m_weapon.GetComponent<RawImage>().texture = Wand;
                break;
        }
    }

    void UpdatePowerUp(GameObject player_HUD, PowerUpID powerUpI)
    {
        m_powerUp = player_HUD.transform.Find("PowerUp").gameObject;
        switch((int)powerUpI)
        {
            case 0:
                m_powerUp.GetComponent<RawImage>().texture = Transparent;
                break;
            case 1:
                m_powerUp.GetComponent<RawImage>().texture = Inky;
                break;
            case 2:
                m_powerUp.GetComponent<RawImage>().texture = Lambert;
                break;
            case 3:
                m_powerUp.GetComponent<RawImage>().texture = Mine;
                break;
            case 4:
                m_powerUp.GetComponent<RawImage>().texture = Missile;
                break;
            case 5:
                m_powerUp.GetComponent<RawImage>().texture = Poison;
                break;
            case 7:
                m_powerUp.GetComponent<RawImage>().texture = Shield;
                break;
            case 8:
                m_powerUp.GetComponent<RawImage>().texture = SpeedUp;
                break;
            case 9:
                m_powerUp.GetComponent<RawImage>().texture = Wall;
                break;
        }
    }

    public void DestoryPlayerHUD(int playerID)
    {
        if(Player_HUD[playerID])
        {
            Player_HUD[playerID].SetActive(false);
        }
    }

}
