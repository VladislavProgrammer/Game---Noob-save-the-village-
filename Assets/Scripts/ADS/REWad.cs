using Playgama;
using UnityEngine;
using Playgama.Modules.Advertisement;
using Playgama.Modules.Platform;
using System.Collections;

public class REWad : MonoBehaviour
{
    public int prizeDiamonds = 100;
    [SerializeField] AudioSource bgSound, bossFightSound;
    [SerializeField] WawesManager wawesManager;
    [SerializeField] PauseLogic pauseLogic;
    [SerializeField] DieLogic dieLogic;
    [SerializeField] SoundManager soundManager;


    private void OnEnable()
    {
        EventManager.NextWawe += ShowBanner;
        Bridge.advertisement.interstitialStateChanged += OnInterstitialStateChanged;
        Bridge.advertisement.rewardedStateChanged += OnRewardedStateChanged;
    }


    private void OnDisable()
    {
        EventManager.NextWawe -= ShowBanner;
        Bridge.advertisement.interstitialStateChanged -= OnInterstitialStateChanged;
        Bridge.advertisement.rewardedStateChanged -= OnRewardedStateChanged;

    }

    

    
   

    public void ShowBanner()
    {
        StartCoroutine(DelayShowBanner());
    }

    IEnumerator DelayShowBanner()
    {
        yield return new WaitForSeconds(0);
        Bridge.advertisement.ShowInterstitial();
        Debug.Log("����� �������");
        
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
                EventManager.CallUnlockCursor();
                break;
            case InterstitialState.Closed:
                ResumeGame();
                EventManager.CallLockCursor();
                CheckPlayerState();
                break;
            case InterstitialState.Failed:
                ResumeGame();
                EventManager.CallLockCursor();
                CheckPlayerState();
                break;
        }
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        EventManager.CallLockControls();
        soundManager.MuteCurrentAudioSource();

    }


    public void ResumeGame()
    {
        Time.timeScale = 1;
        EventManager.CallUnlockControls();
        soundManager.PlayCurrentAudioSource();
    }

    void CheckPlayerState()
    {
        if (dieLogic.PlayerDeath)
        {
            EventManager.CallUnlockCursor();
            EventManager.CallLockControls();
        }
    }



    void OnRewardedStateChanged(RewardedState state)
    {
        
        Debug.Log("������: " + state);
        

        switch (state)
        {
            case RewardedState.Loading:
                PauseGame();
                break;
            case RewardedState.Opened:
                PauseGame();
                EventManager.CallUnlockCursor();
                break;
            case RewardedState.Rewarded:
                RewardedPrizeNextLevel();
                break;
            case RewardedState.Closed:
                ResumeGame();
                EventManager.CallLockCursor();
                CheckPlayerState();
                break;
            case RewardedState.Failed:
                ResumeGame();
                EventManager.CallLockCursor();
                CheckPlayerState();
                break;
        }
    }


    

    public void RewardedPrizeNextLevel()
    {
        EventManager.CallLevelCompleted();
    }

    public void RewardedPrizeDiamonds()
    {
        EventManager.CallChangeScore(prizeDiamonds);
    }


    public void OnClickNextLevelRewBtc()
    {
        //YandexGame.RewVideoShow(1);

        Bridge.advertisement.ShowRewarded();
    }

    
}
