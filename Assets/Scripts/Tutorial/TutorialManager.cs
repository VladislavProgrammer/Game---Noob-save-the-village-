using UnityEngine;
using System.Collections;
using TMPro;
using Playgama;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject _solderTriggerObject;
    [SerializeField] private WawesManager _wawesManager;
    [SerializeField] private List<GameObject> _allGirlsTutorial = new List<GameObject>(100);

    [SerializeField] private bool tutorialComplete = false;
    [SerializeField] private Effects _effects;
    

    private void OnEnable()
    {
        tutorialComplete = GameData.tutorialComplete;
        
        if (tutorialComplete)
        {
            if(_wawesManager) _wawesManager.gameObject.SetActive(true);
            _effects.ShowFragsCounterBG();
            DeactiveAllGirls();

        }

        else
        {
           if(_wawesManager) _wawesManager.gameObject.SetActive(false);
        }
    }



    public void ActivateWawes()
    {
        if(_wawesManager) _wawesManager.gameObject.SetActive(true);
        tutorialComplete = true;
        GameData.tutorialComplete = tutorialComplete; 
        Save("tutorialComplete", tutorialComplete ? 1 : 0);
        PlayerPrefs.SetInt("tutorialComplete", tutorialComplete ? 1 : 0);
        _effects.ShowFragsCounterBG();
        Debug.Log("Туториал пройден");
        DeactiveAllGirls();
        
    }

    void DeactiveAllGirls()
    {
        _allGirlsTutorial.ForEach(obj => obj.SetActive(false));
    }
    
    
    void Save(string name, int value)
    {
        var key = new List<string> { name };
        var data = new List<object> { value };
        Bridge.storage.Set(key, data, OnStorageSetCompleted);
    }
    
    private void OnStorageSetCompleted(bool success)
    {
        Debug.Log($"OnStorageSetCompleted, success: {success}");
    }
}
