using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAuraBuyTrigger : BuyTrigger
{
   [SerializeField] private GameObject _spinAura;
   [SerializeField] private float _deactivateDelay = 3f;

   private void OnEnable()
   {
      _priceText.text = _price.ToString();
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
         ActivateSpinAura();
      }

      else
      {
         _nopeSound.Play();
      }
   }

   void ActivateSpinAura()
   {
      _spinAura.gameObject.SetActive(true);
      Invoke("DeActivateSpinAura", _deactivateDelay);
      
   }

   void DeActivateSpinAura()
   {
      _spinAura.gameObject.SetActive(false);
   }
}
