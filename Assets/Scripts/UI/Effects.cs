using System;
using DG.Tweening;
using UnityEngine;
using System.Collections;
using System.Diagnostics.Tracing;

public class Effects : MonoBehaviour
{

    [SerializeField] GameObject sparkAbility, tntAbiblity;
    [SerializeField] GameObject dialogSpark, dialogTNT, completeWaweBg, nextWaweBg, clickBTC;
    [SerializeField] GameObject _fragsCounterBG;
    [SerializeField] WawesManager wawesManager;
    [SerializeField] CheckDevice checkDevice;
    [SerializeField] PauseLogic pauseLogic;
    public AudioSource waweCompleteSound;

    public bool CanPressBTC = false;
    
    

    private void OnEnable()
    {
        EventManager.NextWawe += ShowFragsCounterBG;
        EventManager.Stage2 += ShowSparkAbility;
        //EventManager.Stage3 += ShowTNTAbility;
        EventManager.StartBossFight += HideAbilites;
        EventManager.WaweCompleted += ShowWaweCompletePhrase;
        EventManager.WaweCompleted += HideFragsCounterBG;
    }

    private void OnDisable()
    {
        EventManager.NextWawe -= ShowFragsCounterBG;
        EventManager.Stage2 -= ShowSparkAbility;
        //EventManager.Stage3 -= ShowTNTAbility;
        EventManager.StartBossFight -= HideAbilites;
        EventManager.WaweCompleted -= ShowWaweCompletePhrase;
        EventManager.WaweCompleted -= HideFragsCounterBG;


    }

    void ShowSparkAbility()
    {
        sparkAbility.transform.DOScale(1, 1);
        ShowDialogPhrase(dialogSpark);
    }


    void ShowTNTAbility()
    {
        tntAbiblity.transform.DOScale(1, 1);
        ShowDialogPhrase(dialogTNT);

    }

    public void ShowFragsCounterBG()
    {
        _fragsCounterBG.transform.DOScale(1, 1);
    }

    void HideFragsCounterBG()
    {
        _fragsCounterBG.transform.DOScale(0, 1);
    }

    private void Update()
    {
        if (!checkDevice.IsMobileDevice())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                
                    OnClickNextWaweBTC();
                
            }
        }
    }

    void ShowWaweCompletePhrase()
    {
        if (!wawesManager.wawesComplete)
        {
            waweCompleteSound.Play();
            StartCoroutine(DelayDeactiveInputs());

            DOTween.Sequence()
              .Append(completeWaweBg.transform.DOScale(1, 2))
              .Append(completeWaweBg.transform.DOScale(0, 1))
              .Append(nextWaweBg.transform.DOScale(1, 1));
        }
        
    }

  

    void ShowDialogPhrase(GameObject obj)
    {
        DOTween.Sequence()
          .Append(obj.transform.DOScale(1, 2))
          .Append(obj.transform.DOScale(0, 1));
    }

    void HideAbilites()
    {
        tntAbiblity.transform.DOScale(0, 1);
        sparkAbility.transform.DOScale(0, 1);

    }

    public void OnClickNextWaweBTC()
    {
        if (!pauseLogic.GamePaused)
        {
            if (CanPressBTC)
            {
                StartCoroutine(DelayActiveBtc());
                CanPressBTC = false;
            }
        }
    }

    IEnumerator DelayDeactiveInputs()
    {
        yield return new WaitForSeconds(3);
        CanPressBTC = true;
        //EventManager.CallLockControls();
        // EventManager.CallUnlockCursor();

    }

    IEnumerator DelayActiveBtc()
    {
        yield return new WaitForSeconds(0);
        nextWaweBg.transform.DOScale(0, 1);
        if (!checkDevice.IsMobileDevice())
        {
            clickBTC.SetActive(true);

        }
        //EventManager.CallUnlockControls();
        EventManager.CallNextWawe();
    }
}
