using TMPro;
using Playgama.Modules.Social;
using Playgama;
using UnityEngine;

public class UITranslate : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI currentText;

    [SerializeField, TextArea(5, 1)]
    string ru;

    [SerializeField, TextArea(5, 1)]
    string en;


    private void Awake()
    {
        currentText = GetComponent<TextMeshProUGUI>();
        SetEnglishText(); // по умолчанию
        CheckLanguage();
    }

    private void OnEnable()
    {
        EventManager.SetLocalizationRU += SetRussianText;
        EventManager.SetLocalizationEN += SetEnglishText;
    }

    void OnDisable()
    {
        EventManager.SetLocalizationRU -= SetRussianText;
        EventManager.SetLocalizationEN -= SetEnglishText;


    }

    void CheckLanguage()
    {

        Debug.Log(Bridge.platform.language);

        if (Bridge.platform.language == "ru")
        {
            SetRussianText();
        }

        else
        {
            SetEnglishText();
        }
    }

    void SetEnglishText()
    {
        currentText.text = en;

    }

    void SetRussianText()
    {
        currentText.text = ru;
    }
}
