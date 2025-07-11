using System;
using UnityEngine;

public class SwordBuyTrigger : BuyTrigger
{
    [SerializeField] private WeaponEquipment _weaponEquipment;
    [SerializeField] private int _currentWeaponIndex = 1; // 1 -огненный меч


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
        if (_scoreLogic.score >= _price && GameData.WeaponID != _currentWeaponIndex)
        {
            _buySound.Play();
            EventManager.CallChangeScore(-_price);
            SetFireSword();
        }

        else
        {
            _nopeSound.Play();
        }
    }

    void SetFireSword()
    {
        _weaponEquipment.ChangeWeapon(_currentWeaponIndex);
    }
}
