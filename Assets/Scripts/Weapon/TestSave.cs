using TMPro;
using UnityEngine;
using Playgama;
using System.Collections.Generic;

public class TestSave : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _testText, _testBool;
    public int value;


    private void Start()
    {
        Bridge.storage.Get(new List<string>() {"value"}, OnStorageGetCompleted);

        _testBool.text = GameData.testBool.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UpdateText();
        }

        TestChangeBool();
    }


    private void OnStorageGetCompleted(bool success, List<string> data)
    {

        // Loading succeeded
        if (success)
        {
            if (data[0] != null)
            {
                int.TryParse(data[0], out var count);
                value = count;
                _testText.text = value.ToString();
                Debug.Log(count);
                
            }
            else
            {
                Debug.Log("нет сохранений по ключу valueTestText");
            }

            



        }
        else
        {
            // Error, something went wrong
        }
    }



    void UpdateText()
    {
        value++;
        _testText.text = value.ToString();
        Save();
    }

    void Save()
    {
        var key = new List<string> { "value" };
        var data = new List<object> { value };
        Bridge.storage.Set(key, data, OnStorageSetCompleted);
    }

    void TestChangeBool()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameData.testBool = true;
            _testBool.text = GameData.testBool.ToString();
            int value = GameData.testBool ? 1 : 0;
            var key = new List<string> { "testBool" };
            var data = new List<object> { value };
            Bridge.storage.Set(key, data, OnStorageSetCompleted);
        }
    }

    private void OnStorageSetCompleted(bool success)
    {
        Debug.Log($"OnStorageSetCompleted, success: {success}");
    }
}
