using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playgama;

public class WeaponLoader : MonoBehaviour
{
    public int WeaponID;
    public GameObject[] Weapons;
    [SerializeField] private SwordTrigger _swordTrigger;

    public int damageSword1, damageSword2, damageSword3;
    public int fireSwordPrice = 100, darkSwordPrice = 200;
    public AudioSource buySound, nopeSound;

    public ScoreLogic scoreLogic;
    public SellerTrigger sellerTrigger;

  


    private void Start()
    {
        LoadWeapon();
    }



    void LoadWeapon()
    {
        WeaponID = GameData.WeaponID;
       // WeaponID = PlayerPrefs.GetInt("WeaponID");
        Debug.Log("��� �����: " + WeaponID);

        if (WeaponID == 0)
        {
            _swordTrigger.damage = damageSword1;
        }

        if(WeaponID == 1)
        {
            _swordTrigger.damage = damageSword2;
        }

        if(WeaponID == 2)
        {
            _swordTrigger.damage = damageSword3;

        }


        foreach (GameObject weapon in Weapons)
        {
            weapon.SetActive(false); // скрыть все оружие
        }

        Weapons[WeaponID].SetActive(true);

    }


    public void OnClickBuyFireSword()
    {
        if(scoreLogic.score >= fireSwordPrice)
        {

            if (WeaponID!= 1) // id ��������� ����
            {
                BuyFireSword();
                EventManager.CallChangeScore(-fireSwordPrice);
            }

            else
            {
                Debug.Log("уже куплено");
                nopeSound.Play();
            }
        }


        else
        {
            Debug.Log("Недостаточно средств");
            nopeSound.Play();
        }
    }



    public void OnClickBuyDarkSword()
    {
        if (scoreLogic.score >= darkSwordPrice)
        {

            if (WeaponID != 2) // id ������� ����
            {
                BuyDarkSword();
                EventManager.CallChangeScore(-darkSwordPrice);
            }

            else
            {
                Debug.Log("��� �������");
                nopeSound.Play();
            }
        }


        else
        {
            Debug.Log("������������ �������");
            nopeSound.Play();
        }
    }

    void BuyFireSword()
    {
        WeaponID = 1;
        GameData.WeaponID = 1;
        buySound.Play();
        sellerTrigger.CloseShopBG();
        Save("WeaponID", WeaponID);
        PlayerPrefs.SetInt("WeaponID", WeaponID);
        LoadWeapon();
    }


    void BuyDarkSword()
    {
        WeaponID = 2;
        GameData.WeaponID = 2;
        Save("WeaponID", WeaponID);
        PlayerPrefs.SetInt("WeaponID", WeaponID);
        buySound.Play();
        sellerTrigger.CloseShopBG();
        LoadWeapon();
    }


    void Save(string name, int value)
    {
        var key = new List<string> { name };
        var data = new List<object> { value };
        Bridge.storage.Set(key, data, OnStorageSetCompleted);
    }

    private void OnStorageSetCompleted(bool success)
    {
        Debug.Log($"OnStorageSetCompleted, success: {success}");
    }

}

