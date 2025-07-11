using System.Collections.Generic;
using UnityEngine;
using Playgama;

public class WeaponEquipment : MonoBehaviour
{
    [SerializeField] private int WeaponID = 0;
    
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();

    private void Awake()
    {
        WeaponID = GameData.WeaponID;
        SetCurrentWeapon(WeaponID);
    }
    

   
    public void ChangeWeapon(int value)
    {
        WeaponID = value;
        GameData.WeaponID = WeaponID;
        PlayerPrefs.SetInt("WeaponID", WeaponID);
        Save("WeaponID", WeaponID);
        SetCurrentWeapon(WeaponID);
    }
    
    void SetCurrentWeapon(int id)
    {
        HideAllWeapons();
        weapons[id].SetActive(true);
    }

    void HideAllWeapons()
    {
        foreach (var weapon in weapons)
        {
            weapon.SetActive(false);
        }
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
