using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Playgama;
using System.Collections.Generic;


public class SoliderBuyTrigger : MonoBehaviour
{
    [SerializeField] private int _soliderPrice;
    [SerializeField] private TextMeshPro _soliderPriceText;
    [SerializeField] private ScoreLogic _scoreLogic;
    [SerializeField] private AudioSource _buySound;
    [SerializeField] private AudioSource _nopeSound;
    [SerializeField] private GameObject _solider;
    [SerializeField] private SoliderSpawner _soliderSpawner;
    

    private void OnEnable()
    {
        _soliderPriceText.text = _soliderPrice.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            BuySolider();
        }
    }

    void BuySolider()   
    {
        if (_scoreLogic.score >= _soliderPrice)
        {
            Debug.Log("Куплен солдат");
            SpawnSolider();
            _buySound.Play();
            EventManager.CallChangeScore(-_soliderPrice);
        }

        else
        { 
            Debug.Log("Недостаточно сресдтв");
            _nopeSound.Play();
        }
    }

    void SpawnSolider()
    {
        Instantiate(_solider, transform.position, Quaternion.identity);
        _soliderSpawner.currentSoliderCount++;
        GameData.currentSoliderCount = _soliderSpawner.currentSoliderCount;
        PlayerPrefs.SetInt("currentSoliderCount", _soliderSpawner.currentSoliderCount);
        Save("currentSoliderCount", _soliderSpawner.currentSoliderCount);
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
