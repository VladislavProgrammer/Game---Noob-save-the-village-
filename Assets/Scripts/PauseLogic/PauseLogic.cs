using UnityEngine;
using Playgama;
using Unity.VisualScripting.Dependencies.NCalc;

public class PauseLogic : MonoBehaviour
{
    [SerializeField] GameObject _pauseBg, checkOn, checkOff, checkRU, checkEN;
    [SerializeField] AudioSource bgSound, bossFightSound;
    [SerializeField] SoundManager soundManager;
    [SerializeField] WawesManager wawesManager;
    public bool MuteMusic = false;
    public bool GamePaused;

    [SerializeField] private int soundIndex; // 0 - вкл., 1- выкл.
    [SerializeField] private AudioListener audioListener;

    private void Awake()
    {
        SetDefaultParametrs();
        CheckCurrentMusicState();
    }


    private void Update()
    {
        CheckClickPause();
    }

    void CheckClickPause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }

        
    }

    void CheckCurrentMusicState()
    {
        soundIndex = PlayerPrefs.GetInt("soundIndex");
        switch (soundIndex)
        {
            case 0:
                OnClickOnMusic();
                break;
            case 1:
                OnClickOffMusic();
                break;
        }
    }

    void SetDefaultParametrs()
    {
        _pauseBg.SetActive(false);
        MuteMusic = false;


        checkOn.SetActive(true);
        checkOff.SetActive(false);
        if(Bridge.platform.language == "ru")
        {
            checkRU.SetActive(true);
            checkEN.SetActive(false);
        }

        else
        {
            checkRU.SetActive(false);
            checkEN.SetActive(true);
        }

    }

    public void PauseGame()
    {
        GamePaused = true;
        _pauseBg.SetActive(true);
        Time.timeScale = 0;
        EventManager.LockControls();
        EventManager.UnLockCursor();
    }


    public void ResumeGame()
    {
        GamePaused = false;
        _pauseBg.SetActive(false);
        Time.timeScale = 1;
        EventManager.UnLockControls();
        EventManager.LockCursor();
    }


    public void OnClickOnMusic()
    {
        checkOn.SetActive(true);
        checkOff.SetActive(false);
        MuteMusic = false;
        AudioListener.volume = 1f;
        AudioListener.pause = false;
        soundIndex = 0;
        PlayerPrefs.SetInt("soundIndex", soundIndex);
        soundManager.PlayCurrentAudioSource();
    }

    public void OnClickOffMusic()
    {
        checkOff.SetActive(true);
        checkOn.SetActive(false);
        MuteMusic = true;
        AudioListener.volume = 0f;
        AudioListener.pause = true;
        soundIndex = 1;
        PlayerPrefs.SetInt("soundIndex", soundIndex);
        soundManager.MuteCurrentAudioSource();
              
    }

    public void OnClickRUlanguage()
    {
        checkRU.SetActive(true);
        checkEN.SetActive(false);
        EventManager.CallSetLocalizationRU();
    }

    public void OnClickENlanguage()
    {
        checkEN.SetActive(true);
        checkRU.SetActive(false);
        EventManager.CallSetLocalizationEN();
    }

}
