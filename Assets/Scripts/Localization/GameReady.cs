using Playgama.Modules.Platform;
using UnityEngine;
using Playgama;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameReady : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        StartCoroutine(WaitForFinalSetup());
        Debug.Log("Загрузили сцену полностью " + scene.name);
    }


    private IEnumerator WaitForFinalSetup()
    {
        yield return new WaitForEndOfFrame();
        SetGameReady();
    }
    
    void SetGameReady() => Bridge.platform.SendMessage(PlatformMessage.GameReady);
}
