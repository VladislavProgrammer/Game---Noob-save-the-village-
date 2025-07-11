using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamomdTrigger : MonoBehaviour
{

    [SerializeField] AudioSource pickUpSound;
    [SerializeField] int prizeScore;

    public float RotateSpeed = 100f;


    private void OnEnable()
    {
        pickUpSound = GameObject.FindGameObjectWithTag("PickUpSound").GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PickUp();

        }
    }

    private void Update()
    {
        RotateSelf();
    }


    void PickUp()
    {
        EventManager.CallChangeScore(prizeScore);
        pickUpSound.Play();
        gameObject.SetActive(false);
    }

    void RotateSelf()
    {
        transform.Rotate(0, RotateSpeed * Time.deltaTime, 0);
    }
}
