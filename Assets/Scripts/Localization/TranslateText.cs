using Playgama;
using UnityEngine;
using TMPro;

public class TranslateText : MonoBehaviour
{
    [SerializeField]
    TextMeshPro currentText;

    [SerializeField, TextArea(5, 1)]
    string ru;

    [SerializeField, TextArea(5, 1)]
    string en;


    private void Awake()
    {
        currentText = GetComponent<TextMeshPro>();
        SetEnglishText();
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
