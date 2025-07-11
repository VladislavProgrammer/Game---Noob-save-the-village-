using UnityEngine.SceneManagement;
using UnityEngine;



public class SkipCutScene : MonoBehaviour
{


    public void OnClickSkipCutScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);



}
