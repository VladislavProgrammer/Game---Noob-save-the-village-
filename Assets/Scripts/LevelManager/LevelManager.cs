using UnityEngine;
using UnityEngine.SceneManagement;
using Playgama;
using Playgama.Modules.Platform;
using UnityEngine.Analytics;
using System.Collections.Generic;



public class LevelManager : MonoBehaviour
{
    public int levelIndex; // ������ ���� �����
    public GameObject LevelCompleteScreen;
    public CameraMove cameraMove;
    public bool isFinalLevel = false;

    private void Awake()
    {
        LevelCompleteScreen.SetActive(false);
       
    }


    private void OnEnable()
    {
        EventManager.LevelCompleted += ShowLvlCompleteScreen;
    }

    private void OnDisable()
    {
        EventManager.LevelCompleted -= ShowLvlCompleteScreen;

    }


   


    void ShowLvlCompleteScreen()
    {
        if (!isFinalLevel)
        {
            LevelCompleteScreen.SetActive(true);
            cameraMove.enabled = false;
            EventManager.CallUnlockCursor();
            EventManager.CallLockControls();
        }
        
        else
        {
            EventManager.CallGameCompleted();
        }
    }

    public void OnClickNextLevel()
    {
        GameData.levelIndex = levelIndex;
        Save(levelIndex);
        Bridge.storage.Delete("_currentWaweIndex");
        Bridge.storage.Delete("wawesComplete");

        // Player prefs логика
        PlayerPrefs.SetInt("levelIndex", levelIndex);
        PlayerPrefs.DeleteKey("_currentWaweIndex"); // �� ����� ������ - ����� �����
        PlayerPrefs.DeleteKey("wawesComplete");


        SceneManager.LoadScene(0); 
      // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnClickGameResetBTC()
    {
         Bridge.storage.Delete("levelIndex");
         Bridge.storage.Delete("score");
         Bridge.storage.Delete("_currentWaweIndex");
         Bridge.storage.Delete("wawesComplete");
         Bridge.storage.Delete("WeaponID");
         Bridge.storage.Delete("testBool");
         PlayerPrefs.DeleteAll();
         SceneManager.LoadScene(0);
        
    }


    void Save(int value)
    {
        var key = new List<string> { "levelIndex" };
        var data = new List<object> { value };
        Bridge.storage.Set(key, data, OnStorageSetCompleted);
        Debug.Log("Сохранен уровень: " + value);
    }

    private void OnStorageSetCompleted(bool success)
    {
        Debug.Log($"OnStorageSetCompleted, success: {success}");
    }
}
