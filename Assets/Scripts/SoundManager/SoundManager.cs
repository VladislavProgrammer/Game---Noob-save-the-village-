using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource fightBGSound, bossThemeSound, relaxBGSound;
    [SerializeField] private PauseLogic pauseLogic;
    [SerializeField] private AudioSource CurrentAudioSource;

    private void Awake()
    {
        MuteAllMusic();
        PlayRelaxTheme();
    }

    private void OnEnable()
    {
        EventManager.StartWaweEvent += PlayWaweFightTheme;
        EventManager.WaweCompleted += PlayRelaxTheme;
        EventManager.StartBossFight += PlayBossTheme;
        
    }

  
    
    void OnDisable()
    {
        EventManager.StartWaweEvent -= PlayWaweFightTheme;
        EventManager.WaweCompleted -= PlayRelaxTheme;
        EventManager.StartBossFight -= PlayBossTheme;
    }

 

    void PlayBossTheme()
    {
        if (!pauseLogic.MuteMusic)
        {
            fightBGSound.Pause();
            relaxBGSound.Pause();       
            bossThemeSound.Play();
        }
        
        CurrentAudioSource = bossThemeSound;
    }

    void PlayRelaxTheme()
    {
        if (!pauseLogic.MuteMusic && !GameData.wawesComplete)
        {
            relaxBGSound.Play();
            fightBGSound.Pause();
            bossThemeSound.Pause();
        }
        
        CurrentAudioSource = relaxBGSound;
    }

    void PlayWaweFightTheme()
    {
        if (!pauseLogic.MuteMusic)
        {
            bossThemeSound.Pause();
            relaxBGSound.Pause();
            fightBGSound.Play();
        }

        CurrentAudioSource = fightBGSound;
    }

    void MuteAllMusic()
    {
        bossThemeSound.Pause();
        relaxBGSound.Pause();
        fightBGSound.Pause();
    }


    public void MuteCurrentAudioSource()
    {
        CurrentAudioSource.Pause();
    }

    public void PlayCurrentAudioSource()
    {
        CurrentAudioSource.Play();
    }
}
