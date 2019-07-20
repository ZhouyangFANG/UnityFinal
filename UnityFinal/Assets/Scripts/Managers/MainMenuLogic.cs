using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuLogic : MonoBehaviour
{
    GameObject m_gameManager;

    GameObject m_mainMenu;
    GameObject m_optionMenu;
    GameObject m_helpMenu;
    GameObject m_weaponPage;
    GameObject m_itemPage;
    GameObject m_operatingPage;

    [SerializeField]
    GameObject m_LocalizationManager = null;

    [SerializeField]
    List<UIElement> m_UIElements = new List<UIElement>();

    List<Button> m_UIButtons = new List<Button>();

    int m_selectionIndex;
    const float MAX_SELECTION_COOLDOWN = 0.25f;
    float m_selectionCooldown;

    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameObject.Find("GameManager");
        m_mainMenu = transform.Find("MainMenu").gameObject;
        m_optionMenu = transform.Find("OptionMenu").gameObject;
        m_helpMenu = transform.Find("HelpMenu").gameObject;
        m_weaponPage = transform.Find("WeaponPage").gameObject;
        m_itemPage = transform.Find("ItemPage").gameObject;
        m_operatingPage = transform.Find("OperatingPage").gameObject;

        for(int index = 0; index < m_UIElements.Count; ++index)
        {
            Button buttonObj = m_UIElements[index].m_GameObject.GetComponent<Button>();
            if(buttonObj)
            {
                m_UIButtons.Add(buttonObj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSelection();
    }

    void UpdateSelection()
    {
        if (m_selectionCooldown < 0.0f)
        {
            float verticalMovement = Input.GetAxis("Vertical");
            bool selectionChanged = false;
            if (verticalMovement > 0.1f)
            {
                --m_selectionIndex;
                selectionChanged = true;
            }
            else if (verticalMovement < -0.1f)
            {
                ++m_selectionIndex;
                selectionChanged = true;
            }

            if (selectionChanged)
            {
                m_selectionIndex = Mathf.Clamp(m_selectionIndex, 0, m_UIButtons.Count - 1);
                m_UIButtons[m_selectionIndex].Select();
                m_selectionCooldown = MAX_SELECTION_COOLDOWN;
            }
        }
        else
        {
            m_selectionCooldown -= Time.deltaTime;
        }
    }

    public void OnStartClicked()
    {
        Debug.Log("Start has been clicked");
        m_gameManager.GetComponent<GameManager>().changeScece("GameScene");
    }

    public void OnOptionClicked()
    {
        m_mainMenu.SetActive(false);
        m_optionMenu.SetActive(true);
    }

    public void OnHelpClicked()
    {
        m_mainMenu.SetActive(false);
        m_helpMenu.SetActive(true);
    }

    public void OnQuitClicked()
    {
        Debug.Log("Quit has been clicked");
        Application.Quit();
    }

    public void OnEnglishClicked()
    {
        LocalizationManager.Instance.SetupLocalisation(Language.English);
    }

    public void OnChineseClicked()
    {
        LocalizationManager.Instance.SetupLocalisation(Language.Chinese);
    }

    public void On2PlayerClicked()
    {
        m_gameManager.GetComponent<GameManager>().setPlayer(2);
    }

    public void On3PlayerClicked()
    {
        m_gameManager.GetComponent<GameManager>().setPlayer(3);
    }

    public void On4PlayerClicked()
    {
        m_gameManager.GetComponent<GameManager>().setPlayer(4);
    }

    public void OnBackMainMenuClicked()
    {
        m_mainMenu.SetActive(true);
        m_optionMenu.SetActive(false);
        m_helpMenu.SetActive(false);
    }

    public void OnWeaponClicked()
    {
        m_weaponPage.SetActive(true);
        m_helpMenu.SetActive(false);
    }

    public void OnItemClicked()
    {
        m_itemPage.SetActive(true);
        m_helpMenu.SetActive(false);
    }

    public void OnOperatingClicked()
    {
        m_operatingPage.SetActive(true);
        m_helpMenu.SetActive(false);
    }

    public void OnBackHelpMenuClicked()
    {
        m_helpMenu.SetActive(true);
        m_weaponPage.SetActive(false);
        m_itemPage.SetActive(false);
        m_operatingPage.SetActive(false);
    }
}
