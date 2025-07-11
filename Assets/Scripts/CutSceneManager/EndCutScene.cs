using UnityEngine.SceneManagement;
using UnityEngine;

public class EndCutScene : MonoBehaviour
{
    private void OnEnable()
    {
        StartGameScene();
    }


    void StartGameScene()
    {
        SceneManager.LoadScene(2);
    }
}
