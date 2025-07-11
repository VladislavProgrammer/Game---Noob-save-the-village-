using UnityEngine;
using System.Collections;
using TMPro;
using Playgama;
using System.Collections.Generic;

public class WawesManager : MonoBehaviour
{

    public Wawes[] Wawes;
    private int _currentWaweIndex;
    private int _currentEnemyIndex;
    private int _enemiesLeftToSpawn;

    [SerializeField] private TMP_Text wawesText;

    public Transform[] spawnPoints;

    public TextMeshProUGUI currentFragsText, maxFragsText;
    [SerializeField] private int _currentFrags;
    public bool bossInLevel;
    public bool wawesComplete;
    public bool onlyWawes;

    [SerializeField] AudioSource levelBGSound;



    private void Awake()
    {
        LoadResources();
    }
    

    void CheckCurrentStage()
    {
        if (!wawesComplete)
        {

            _enemiesLeftToSpawn = Wawes[_currentWaweIndex].countEnemies;
            UpdateText();
            CheckStage();
            LaunchWawe();
            
            
        }

        else
        {
            if (bossInLevel)
            {
                EventManager.CallStartBossFight();
                
            }
        }
    }

    void LoadResources()
    {
        _currentWaweIndex = GameData._currentWaweIndex;
        wawesComplete = GameData.wawesComplete;

      //  _currentWaweIndex = PlayerPrefs.GetInt("_currentWaweIndex");
       // wawesComplete = PlayerPrefs.GetInt("wawesComplete") == 1 ? true : false;

        Debug.Log("Волна номер" + _currentWaweIndex); // �.� ����� � ������� � 0
        //CheckStage();
        
    }

   
    

    private void OnEnable()
    {
        EventManager.EnemyDeath += NewFrag;
        EventManager.NextWawe += LaunchWawe;
        
        CheckCurrentStage();
        
    }

    private void OnDisable()
    {
        EventManager.EnemyDeath -= NewFrag;
        EventManager.NextWawe -= LaunchWawe;


    }


    void CheckStage()
    {
        switch (_currentWaweIndex)
        {
            case 1: // ����� 2
                EventManager.CallStage2();
                //Debug.Log("Unlock Sparks");
                break;
            case 2: // ����� 3 
                EventManager.CallStage3();
                Debug.Log("Unlock TNT");
                break;
            case 3: // ����� 4
                Debug.Log("��������� �����");

                break;
        }
    }

    void UpdateText()
    {
        currentFragsText.text = _currentFrags.ToString() + " /";
        maxFragsText.text = Wawes[_currentWaweIndex].countEnemies.ToString();
    }

    private IEnumerator SpawnEnemyInWawe()
    {
        //Debug.Log("������" + Wawes[_currentWaweIndex].WaweID);
       

        if (_enemiesLeftToSpawn > 0)
        {
            yield return new WaitForSeconds(Wawes[_currentWaweIndex].SpawnDelay);
            int randomIndex = Random.Range(0, spawnPoints.Length);
            
            Instantiate(Wawes[_currentWaweIndex].Enemy,
                spawnPoints[randomIndex].position, Quaternion.identity);
            _enemiesLeftToSpawn--;
            StartCoroutine(SpawnEnemyInWawe());
        }

        
    }
   
    void NewFrag()
    {
        _currentFrags++;
        if (_currentFrags == Wawes[_currentWaweIndex].countEnemies)
        {

            

            if (_currentWaweIndex < Wawes.Length - 1)
            {
                UpdateWaweIndex();
                _enemiesLeftToSpawn = Wawes[_currentWaweIndex].countEnemies;
                _currentFrags = 0; // �� ����� ����� ������ ������� ������
                //LaunchWawe();


            }

            else
            {
                Debug.Log("����� �� ������ �����������");
                wawesComplete = true;
                GameData.wawesComplete = wawesComplete;
                Save("wawesComplete", wawesComplete ? 1 : 0);
                PlayerPrefs.SetInt("wawesComplete", wawesComplete ? 1 : 0);


                EventManager.CallAllWawesCompletedEvent();
                
                if(onlyWawes)
                {
                    EventManager.CallLevelCompleted();
                }


                if (bossInLevel)
                {
                    EventManager.CallStartBossFight();
                }
            }

            Debug.Log("����� ��������");

            EventManager.CallWaweCompleted();

        }

        UpdateText();
    }

    void UpdateWaweIndex()
    {
        _currentWaweIndex++;
        GameData._currentWaweIndex++;
        Save("_currentWaweIndex", _currentWaweIndex);

       // PlayerPrefs логика
       PlayerPrefs.SetInt("_currentWaweIndex", _currentWaweIndex);

        switch (_currentWaweIndex)
        {
            case 1: // ����� 2
                EventManager.CallStage2();
                //Debug.Log("Unlock Sparks");
                break;
            case 2: // ����� 3 
                EventManager.CallStage3();
                Debug.Log("Unlock TNT");
                break;
            case 3: // ����� 4
                Debug.Log("��������� �����");
                break;
        }

    }

    public void LaunchWawe()
    {
        StartCoroutine(SpawnEnemyInWawe());
        EventManager.CallStartWaweEvent();
        
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

[System.Serializable]
public class Wawes
{
    public int WaweID;
    public int countEnemies;
    public GameObject Enemy;
    public float SpawnDelay;
   
}