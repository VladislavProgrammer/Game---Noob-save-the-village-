using UnityEngine.SceneManagement;
using UnityEngine;

public class DieLogic : MonoBehaviour
{
    public GameObject gameOverBG;
    public CameraMove cameraMove;
    public bool PlayerDeath = false;


    void Awake()
    {
        HideGameOverBG();
    }

    void OnEnable()
    {
        EventManager.GameOver += ShowGameOverBG;
    }


    void OnDisable()
    {
        EventManager.GameOver -= ShowGameOverBG;  

    }

    public void ShowGameOverBG()
    {
        gameOverBG.SetActive(true);
        PlayerDeath = true;
        EventManager.CallUnlockCursor();
        EventManager.CallLockControls();
    }


    void HideGameOverBG()
    {
        gameOverBG.SetActive(false);
    }

    public void OnClickRestart()
    {
        //PlayerPrefs.DeleteKey("score");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
