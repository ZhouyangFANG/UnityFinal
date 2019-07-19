using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Unimplemented
public class GameManager : MonoBehaviour
{

    [SerializeField]
    GameObject m_pauseMenu;

    [SerializeField]
    int m_player = 2;

    [SerializeField]
    GameObject m_eventSystem;

    public static GameManager Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(m_eventSystem);
        } else {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Escape") && m_pauseMenu)
        {
            if(m_pauseMenu.activeInHierarchy == false)
            {
                m_pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                m_pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void ChangeScece(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetPauseMenu(GameObject pauseMenu)
    {
        Debug.Log("Set Pause Menu");
        m_pauseMenu = pauseMenu;
        m_pauseMenu.SetActive(false);
    }

    public void SetPlayer(int player)
    {
        m_player = player;
    }
    
    public int GetPlayer()
    {
        return m_player;
    }
}
