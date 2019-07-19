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
    int playerID;

    [SerializeField]
    GameObject [] Player_HUD;
    GameObject [] hp;
    int m_hp;
    int m_weaponID;
    int m_powerUpID;

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
    void Start()
    {
        m_gameManager = GameObject.Find("GameManager");
        m_playernum = m_gameManager.GetComponent<GameManager>().GetPlayer();
        Player_HUD = new GameObject[4];
        for(int i = 0; i < 4; i++)
        {
            Player_HUD[i] = GameObject.Find("Canvas/Player"+(i+1).ToString()+"_HUD");
            if(i < m_playernum) Player_HUD[i].SetActive(true);
            else Player_HUD[i].SetActive(false);
        }
        m_player = GameObject.FindGameObjectsWithTag("Player");
        hp = new GameObject[3];

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
            if(m_player[i])
            {
                playerID = (int)m_player[i].GetComponent<PlayerLogic>().getPlayerID();
                m_hp = (int)m_player[i].GetComponent<PlayerLogic>().GetHP();
                m_weaponID = (int)m_player[i].GetComponent<PlayerLogic>().GetWeaponID();
                m_powerUpID = (int)m_player[i].GetComponent<PlayerLogic>().GetPowerUpID();
                UpdateHP(Player_HUD[playerID], m_hp);
                UpdateWeapon(Player_HUD[playerID], m_weaponID);
                UpdatePowerUp(Player_HUD[playerID], m_powerUpID);
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

    void UpdateWeapon(GameObject player_HUD, int weaponID)
    {
        m_weapon = player_HUD.transform.Find("Weapon").gameObject;
        switch(weaponID)
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

    void UpdatePowerUp(GameObject player_HUD, int powerUpI)
    {
        m_powerUp = player_HUD.transform.Find("PowerUp").gameObject;
        switch(powerUpI)
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
}
