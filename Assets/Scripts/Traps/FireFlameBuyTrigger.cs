using System;
using UnityEngine;

public class FireFlameBuyTrigger : BuyTrigger
{
    [SerializeField] private GameObject _fireFlame;
    [SerializeField] private int _deactiveDelay;

    private void OnEnable()
    {
        DeactivateFireFlame();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TryBuy();
        }
    }

    void TryBuy()
    {
        if (_scoreLogic.score >= _price)
        {
            _buySound.Play();
            EventManager.CallChangeScore(-_price);
            ActivateFireFlame();
        }

        else
        {
            _nopeSound.Play();
        }
    }


    void ActivateFireFlame()
    {
        if(_fireFlame) _fireFlame.SetActive(true);
        Invoke("DeactivateFireFlame", _deactiveDelay);
    }

    void DeactivateFireFlame()
    {
        if(_fireFlame) _fireFlame.SetActive(false);
    }
}
