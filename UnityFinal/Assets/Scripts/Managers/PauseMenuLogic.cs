using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIElement
{
    public GameObject m_GameObject;
}

public class PauseMenuLogic : MonoBehaviour
{
    GameObject m_gameManager;

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
        m_gameManager.GetComponent<GameManager>().SetPauseMenu(this.gameObject);
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

    public void OnBackClicked()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void OnRestartClicked()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        m_gameManager.GetComponent<GameManager>().ChangeScece("GameScene");
    }

    public void OnMainManuClicked()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        m_gameManager.GetComponent<GameManager>().ChangeScece("MainMenuScene");
    }
}
