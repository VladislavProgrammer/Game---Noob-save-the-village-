using UnityEngine;
using System;


public class EventManager : MonoBehaviour
{

    public static Action<int> ChangeScoreEvent;

    public static Action GameOver;

    public static Action EnemyDeath;

    public static Action Stage2;

    public static Action Stage3;

    public static Action BossTakeDamage;

    public static Action StartBossFight;

    public static Action EndBossFight;

    public static Action LevelCompleted;

    public static Action WaweCompleted;

    public static Action GameCompleted;

    public static Action GirlDamage;

    public static Action StopGirlDamage;

    public static Action LockCursor;

    public static Action UnLockCursor;

    public static Action LockControls;

    public static Action UnLockControls;

    public static Action NextWawe;

    public static Action StartWaweEvent;

    public static Action SetLocalizationRU;

    public static Action SetLocalizationEN;

    public static Action AllWawesCompletedEvent;

    public static Action PlayerAttackEvent;
  
    public static void CallChangeScore(int value)
    {
        ChangeScoreEvent?.Invoke(value);

    }

    public static void CallGameOver()
    {
        GameOver?.Invoke();
    }


    public static void CallEnemyDeath()
    {
        EnemyDeath?.Invoke();
    }

    public static void CallStage2()
    {
        Stage2?.Invoke();
    }

    public static void CallStage3()
    {
        Stage3?.Invoke();
    }

    public static void CallBossTakeDamage()
    {
        BossTakeDamage?.Invoke();
    }


    public static void CallStartBossFight()
    {
        StartBossFight?.Invoke();
    }


    public static void CallEndBossFight()
    {
        EndBossFight?.Invoke();
    }

   


    public static void CallLevelCompleted()
    {
        LevelCompleted?.Invoke();
    }

    public static void CallWaweCompleted()
    {
        WaweCompleted?.Invoke();
    }


    public static void CallGameCompleted()
    {
        GameCompleted?.Invoke();
    }

    public static void CallGirlDamage()
    {
        GirlDamage?.Invoke();
    }


    public static void CallStopGirlDamage()
    {
        StopGirlDamage?.Invoke();
    }

    public static void CallUnlockCursor()
    {
        UnLockCursor?.Invoke();
    }

    public static void CallLockCursor()
    {
        LockCursor?.Invoke();
    }


    public static void CallLockControls()
    {
        LockControls?.Invoke();
    }


    public static void CallUnlockControls()
    {
        UnLockControls?.Invoke();
    }

    public static void CallNextWawe()
    {
        NextWawe?.Invoke();
    }

    public static void CallSetLocalizationRU()
    {
        SetLocalizationRU?.Invoke();
    }

    public static void CallSetLocalizationEN()
    {
        SetLocalizationEN?.Invoke();
    }

    public static void CallAllWawesCompletedEvent() => AllWawesCompletedEvent?.Invoke();

    public static void CallStartWaweEvent() => StartWaweEvent?.Invoke();
    
    public static void CallPlayerAttackEvent() => PlayerAttackEvent?.Invoke();

}
