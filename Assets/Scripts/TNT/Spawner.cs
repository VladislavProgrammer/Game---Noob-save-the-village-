using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject TNT;
    public Transform TNTSpawnPoint;
    public Transform bombEffect;
    public TNT tnt;

    public int TNTPrice = 10;
    [SerializeField] ScoreLogic scoreLogic;
    [SerializeField] AudioSource nopeSound;
    [SerializeField] CheckDevice checkDevice;
    [SerializeField] PauseLogic pauseLogic;

    public bool TnTInLevel;
    public bool unlockTnT = false;


    private void OnEnable()
    {
        
        EventManager.Stage3 += UnLockTNT;
    }

    private void OnDisable()
    {
        EventManager.Stage3 -= UnLockTNT;

    }

    private void Awake()
    {
        DeactiveTNT();
    }

    private void Update()
    {
        if (!checkDevice.IsMobileDevice())
        {
            
            CheckKeyBoardClick();
        }
        
    }

    void CheckKeyBoardClick()
    {
        if (TnTInLevel && unlockTnT)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnClickTNTBtc();
            }
        }
    }


    void UnLockTNT()
    {
        unlockTnT = true;
    }

    void DeactiveTNT()
    {
        TNT.SetActive(false);
        
        
    }


    public void OnClickTNTBtc()
    {
        if (!pauseLogic.GamePaused)
        {
            if (tnt.canExplode)
            {
                if (scoreLogic.score >= TNTPrice)
                {
                    ActivateTNT();
                    EventManager.CallChangeScore(-TNTPrice);
                }

                else
                {
                    Debug.Log("Не хватает алмазов");
                    nopeSound.Play();
                }
            }
        }
        
        
    }

    void ActivateTNT()
    {
        TNT.SetActive(true);
        TNT.transform.position = TNTSpawnPoint.position;
        bombEffect.position = TNTSpawnPoint.position;
        bombEffect.SetParent(null);
        TNT.transform.SetParent(null);
    }
}
