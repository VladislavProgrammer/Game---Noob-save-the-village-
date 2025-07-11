using UnityEngine;
using System.Collections;



public class CutSceneManager : MonoBehaviour
{

    [SerializeField] GameObject attackTutorialBG, abilitesBg, gameCompleteBG, clickBTC;

    public GameObject CutSceneEndBoss;

    public int delayBeforeStartBossCutScene;

    [SerializeField] CheckDevice checkDevice;

    public bool isFinalLvlCutScene;

    bool isClicked = false;

    private void Awake()
    {
        CutSceneEndBoss.SetActive(false);
    }

    private void OnEnable()
    {
        EventManager.EndBossFight += PlayCutSceneEndBoss;
        EventManager.EndBossFight += ShowGameCompleteUI;
    }

    private void OnDisable()
    {
        EventManager.EndBossFight -= PlayCutSceneEndBoss;
        EventManager.EndBossFight -= ShowGameCompleteUI;


    }

    public void OnCheckClickTutorial()
    {
        if (!isClicked)
        {
            attackTutorialBG.SetActive(false);
            abilitesBg.SetActive(true);
            Debug.Log("�������");
            isClicked = true;
            
        }

        if (!checkDevice.IsMobileDevice())
        {
            EventManager.CallLockCursor();
        }
        
        clickBTC.SetActive(false);
    }


    public void PlayCutSceneEndBoss()
    {
        if (isFinalLvlCutScene)
        {
            StartCoroutine(DelayBeforePlayBossCutScene());
        }
        else Invoke("CompleteLvLAfterBoss", 3);
    }

    void CompleteLvLAfterBoss()
    {
        EventManager.CallLevelCompleted();
    }

    IEnumerator DelayBeforePlayBossCutScene()
    {
        yield return new WaitForSeconds(delayBeforeStartBossCutScene);
        CutSceneEndBoss.SetActive(true);

    }


    public void EndCutSceneLevel1()
    {
        EventManager.CallLevelCompleted();
        Debug.Log("������: �������1 �������");
    }

    public void ShowGameCompleteUI()
    {
       if(gameCompleteBG) gameCompleteBG.SetActive(true);
        EventManager.CallUnlockCursor();
        EventManager.CallLockControls();
    }


   


}
