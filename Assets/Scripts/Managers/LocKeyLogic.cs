using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocKeyLogic : MonoBehaviour
{
    [SerializeField]
    string m_key = null;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = LocalizationManager.Instance.GetLocalisedString(m_key);
        LocalizationManager.Instance.OnUpdateLanguage += UpdateText;
    }

    void UpdateText() {
        GetComponent<Text>().text = LocalizationManager.Instance.GetLocalisedString(m_key);
    }

    private void OnDestroy() {
        LocalizationManager.Instance.OnUpdateLanguage -= UpdateText;
    }
}
