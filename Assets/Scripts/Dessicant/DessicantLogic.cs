using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DessicantLogic : MonoBehaviour
{
    [SerializeField] GameObject headBullet;
    [SerializeField] Transform spawnPoint;

    public AudioSource spawnHeadSound;
    public int delay = 3;


    private void OnEnable()
    {
        StartCoroutine(SpawnHead());
    }

    IEnumerator SpawnHead()
    {
        yield return new WaitForSeconds(delay);
        AttackHead();
        StartCoroutine(SpawnHead());
        
    }
    

    void AttackHead()
    {
        spawnHeadSound.Play();
        Instantiate(headBullet, spawnPoint.position, Quaternion.identity);
    }


    
}
