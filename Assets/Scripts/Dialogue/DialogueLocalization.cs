using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLocalization : MonoBehaviour
{
 
    public static DialogueLocalization Instance { get; private set; }
    [SerializeField] private CheckDevice _checkDevice;
    public enum Language { Ru, En }
    public Language currentLanguage;

    public bool IsRussian;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void OnEnable()
    {
        if(_checkDevice.IsRussianLanguage())
        {
            currentLanguage = Language.Ru;
        } 
        
        else currentLanguage = Language.En;
    }

    public string GetLocalizedText(string ruText, string enText)
    {
        return currentLanguage == Language.Ru ? ruText : enText;
    }
    
    public string GetLocalizedName(string nameRu, string nameEn)
    {
        return currentLanguage == Language.Ru ? nameRu : nameEn;
    }


    public AudioClip GetLocalizedAudio(AudioClip ruAudio, AudioClip enAudio)
    {
        return currentLanguage == Language.Ru ? ruAudio : enAudio;
    }
}
