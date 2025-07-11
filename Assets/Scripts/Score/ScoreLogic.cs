using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Playgama;



public class ScoreLogic : MonoBehaviour
{


    public int score;
    [SerializeField] private TextMeshProUGUI scoreText;



    private void OnEnable()
    {
        EventManager.ChangeScoreEvent += UpdateScoreText;
    }

    private void OnDisable()
    {
        EventManager.ChangeScoreEvent -= UpdateScoreText;

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UpdateScoreText(100);
        }
    }

    private void Awake()
    {
        score = GameData.score;
        if (score == 0)
        {
            score = 10;
        }
        scoreText.text = score.ToString();
    }

    void UpdateScoreText(int prize)
    {
        
        score += prize;
        scoreText.text = score.ToString();
        Save(score);

        // PlayerPrefs логика
        PlayerPrefs.SetInt("score", score);
        //YandexGame.NewLeaderboardScores("totalScore", score);
       
    }

   


    void Save(int value)
    {
        var key = new List<string> { "score" };
        var data = new List<object> { value };
        Bridge.storage.Set(key, data, OnStorageSetCompleted);
    }

    private void OnStorageSetCompleted(bool success)
    {
        Debug.Log($"OnStorageSetCompleted, success: {success}");
    }
}
