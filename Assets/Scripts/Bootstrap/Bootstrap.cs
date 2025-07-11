using UnityEngine.SceneManagement;
using UnityEngine;
using Playgama;
using Playgama.Modules.Platform;
using Playgama.Modules.Advertisement;
using System.Collections.Generic;

public class Bootstrap : MonoBehaviour
{

   public int levelIndex;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        LoadPlaygamaModules();
        LoadPlayerPrefs();
        LoadPlaygamaSaves();
        LoadCurrentSceneIndex();  


    }



    public void LoadCurrentSceneIndex()
    {
        levelIndex = GameData.levelIndex;
        //LevelIndex = PlayerPrefs.GetInt("LevelIndex");
        if(levelIndex == 0)
        {
            levelIndex = 1;
        }
        Debug.Log("Сцена номер: " + levelIndex);
        SceneManager.LoadScene(levelIndex);
    }


    void LoadPlaygamaModules()
    {

       // Bridge.advertisement.SetMinimumDelayBetweenInterstitial(5);

        if (Bridge.platform.name == "YandexGame")
        {
            Bridge.advertisement.ShowInterstitial();

        }

      //  OnInterstitialStateChanged(Bridge.advertisement.interstitialState);

        
    }


    void OnInterstitialStateChanged(InterstitialState state)
    {

        Debug.Log("��������: " + state);


        switch (state)
        {
            case InterstitialState.Loading:
                PauseGame();
                break;
            case InterstitialState.Opened:
                PauseGame();
                break;
            case InterstitialState.Closed:
                ResumeGame();
                break;
            case InterstitialState.Failed:
                ResumeGame();
                break;
        }
    }


    void PauseGame()
    {
        Time.timeScale = 0f;


    }


    void ResumeGame()
    {
        Time.timeScale = 1f;


    }


    void LoadPlaygamaSaves()
    {
        Bridge.storage.Get(new List<string>()
        { "levelIndex", "score", 
          "testBool", "_currentWaweIndex",
          "wawesComplete", "WeaponID", "tutorialComplete", "BaseDamage", "speed", "currentSoliderCount"}, OnStorageGetCompleted);
        
    }



    private void OnStorageGetCompleted(bool success, List<string> data)
    {

        // Loading succeeded
        if (success)
        {
            if (data[0] != null)
            {
                int.TryParse(data[0], out var value);
                GameData.levelIndex = value;

            }
            else
            {
                GameData.levelIndex = 1;
                Debug.Log("��� ���������� �� ����� levelIndex");
            }

            if(data[1] != null)
            {
                int.TryParse(data[1], out var value);
                GameData.score = value;
            }

            else
            {
                GameData.score = 0;
                Debug.Log("��� ���������� �� ����� score");
            }

            if (data[2] != null)
            {
                int.TryParse(data[2], out var value);
                GameData.testBool = (value == 1 ? true : false);
                Debug.Log("Bootstrap: " + GameData.testBool);
            }

            else
            {
                GameData.testBool = false;
                Debug.Log("��� ���������� �� ����� testBool");
            }

            if (data[3] != null)
            {
                int.TryParse(data[3], out var value);
                GameData._currentWaweIndex = value;
                
            }

            else
            {
                Debug.Log("��� ���������� �� ����� _currentWaweIndex");
                GameData._currentWaweIndex = 0;
            }

            if (data[4] != null)
            {
                int.TryParse(data[4], out var value);
                GameData.wawesComplete = (value == 1 ? true : false);

            }

            else
            {
                Debug.Log("��� ���������� �� ����� wawesComplete");
                GameData.wawesComplete = false;
            }

            if (data[5] != null)
            {
                int.TryParse(data[5], out var value);
                GameData.WeaponID = value;

            }

            else
            {
                GameData.WeaponID = 0;
                Debug.Log("��� ���������� �� ����� WeaponID");
            }

            if (data[6] != null)
            {
                int.TryParse(data[6], out var value);
                GameData.tutorialComplete = (value == 1 ? true : false);
            }

            else
            {
                GameData.tutorialComplete = false;
                Debug.Log("Нет сохранений по ключу tutorialComplete");
            }

            if (data[7] != null)
            {
                float.TryParse(data[7], out var value);
                GameData.BaseDamage = value;
            }
            
            if(data[8] != null)
            {
                float.TryParse(data[8], out var value);
                GameData.speed = value;
            }

            if (data[9] != null)
            {
                int.TryParse(data[9], out var value);
                GameData.currentSoliderCount = value;
            }

            else
            {
                GameData.currentSoliderCount = 0;
            }

            Debug.Log("Грузим Playgama сохранения");

        }
        else
        {
            // Error, something went wrong

            Debug.Log("Грузим PlayerPrefs сохранения");
           
        }

         

    }


    void LoadPlayerPrefs()
    {
        GameData.levelIndex = PlayerPrefs.GetInt("levelIndex");
        GameData.score = PlayerPrefs.GetInt("score");
        GameData._currentWaweIndex = PlayerPrefs.GetInt("_currentWaweIndex");
        GameData.wawesComplete = PlayerPrefs.GetInt("wawesComplete") == 1 ? true : false;
        GameData.WeaponID = PlayerPrefs.GetInt("WeaponID");
        GameData.tutorialComplete = PlayerPrefs.GetInt("tutorialComplete") == 1 ? true : false; 
        GameData.BaseDamage = PlayerPrefs.GetFloat("BaseDamage");
        GameData.speed = PlayerPrefs.GetFloat("speed");
        GameData.currentSoliderCount = PlayerPrefs.GetInt("currentSoliderCount");
    }
}
