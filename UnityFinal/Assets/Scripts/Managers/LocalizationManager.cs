using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


public enum Language
{
    English,
    Chinese
}

// Unimplemented
public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance = null;

    Dictionary<Language, TextAsset> m_LocalizationFiles = new Dictionary<Language, TextAsset>();

    Dictionary<string, string> m_LocalizationText = new Dictionary<string, string>();

    [SerializeField]
    Language m_currentLanguage;

    public delegate void UpdateLanguage();
    public event UpdateLanguage OnUpdateLanguage;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        SetupLocalisationFiles();
        SetupLocalisation(m_currentLanguage);
        DontDestroyOnLoad(this.gameObject);
    }

    void SetupLocalisationFiles()
    {
        foreach(Language language in Language.GetValues(typeof(Language)))
        {
            string textAssetPath = "Localization/" + language.ToString();
            TextAsset textAsset = (TextAsset)Resources.Load(textAssetPath);

            if(textAsset)
            {
                m_LocalizationFiles[language] = textAsset;
                Debug.Log("Text Asset: " + textAsset.name);
                
            }
            else
            {
                Debug.LogError("TextAssetPath not found for: " + textAssetPath);
            }
        }
    }

    public void SetupLocalisation(Language language)
    {
        m_currentLanguage = language;

        // 1. Find the file
        TextAsset textAsset;
        if(m_LocalizationFiles.ContainsKey(language))
        {
            textAsset = m_LocalizationFiles[language];
        }
        else
        {
            Debug.LogError("Can't find selected language: " + language);
            textAsset = m_LocalizationFiles[Language.English];
        }

        // 2. Load the XML document
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(textAsset.text);

        // 3. Get all elements called "Entry"
        XmlNodeList entryList = xmlDocument.GetElementsByTagName("Entry");

        // 4. Iterate over each element and store them in a dictionary
        foreach(XmlNode entry in entryList)
        {
            m_LocalizationText[entry.SelectSingleNode("Key").InnerText] = entry.SelectSingleNode("Value").InnerText;
        }

        if (OnUpdateLanguage != null) {
            OnUpdateLanguage(); 
        }
    }

    public string GetLocalisedString(string key)
    {
        string localisedString = "";
        if(!m_LocalizationText.TryGetValue(key, out localisedString))
        {
            localisedString = "Loc Key Not Found: " + key;
        }

        return localisedString;
    }
}
