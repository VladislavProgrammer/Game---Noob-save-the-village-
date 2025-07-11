using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{

    [SerializeField] GameObject trap;
    [SerializeField] Transform spawnPoint;
    [SerializeField] SpikeTrapTrigger spikeTrapTrigger;
    [SerializeField] AudioSource addTrapSound, nopeSound;
    [SerializeField] int trapPrice;
    [SerializeField] ScoreLogic scoreLogic;
    [SerializeField] CheckDevice checkDevice;
    [SerializeField] PauseLogic pauseLogic;

    public bool SparkInLevel;


    private void Awake()
    {
        trap.SetActive(false);
    }


    private void Update()
    {
        if (!checkDevice.IsMobileDevice())
        {
            if(!pauseLogic.GamePaused)
            CheckClickKeyBoard();
        }
        
    }

    void CheckClickKeyBoard()
    {
        if (SparkInLevel)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                OnClickTrapBtc();
            }
        }
    }

    public void OnClickTrapBtc()
    {
        if (!pauseLogic.GamePaused)
        {
            if (spikeTrapTrigger.canAdd)
            {
                if (scoreLogic.score >= trapPrice)
                {
                    ActivateTrap();
                    EventManager.CallChangeScore(-trapPrice);
                }

                else
                {
                    Debug.Log("Не хватает алмазов");
                    nopeSound.Play();
                }
            }
        }

        

    }


    void ActivateTrap()
    {
        addTrapSound.Play();
        trap.SetActive(true);
        trap.transform.position = spawnPoint.position;
        trap.transform.SetParent(null);
    }
}
