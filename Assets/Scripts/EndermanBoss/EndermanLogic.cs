using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndermanLogic : MonoBehaviour
{

    public Transform[] TeleportPoints;
    public int delayTeleport = 3;
    public AudioSource endermanTeleportSound;


    private void OnEnable()
    {
        StartCoroutine(DelayEndermanTeleport());
    }

    IEnumerator DelayEndermanTeleport()
    {
        yield return new WaitForSeconds(delayTeleport);
        Teleport();
        StartCoroutine(DelayEndermanTeleport());
        
    }

    void Teleport()
    {
        endermanTeleportSound.Play();
        int randomIndex = Random.Range(0, TeleportPoints.Length);
        transform.position = TeleportPoints[randomIndex].position;

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sword")
        {
            EventManager.CallBossTakeDamage();
        }
    }
}
