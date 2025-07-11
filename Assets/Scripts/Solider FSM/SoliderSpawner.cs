using System;
using UnityEngine;

public class SoliderSpawner : MonoBehaviour
{
    public int currentSoliderCount; //3
    public GameObject solider;
    public Transform spawnPoint;

    private void Awake()
    {
        currentSoliderCount = GameData.currentSoliderCount;
        Debug.Log("Нужно солдат: " +  currentSoliderCount);
        SpawnAllSoliders();
    }

    void SpawnAllSoliders()
    {
        int index = 0;

        while (index < currentSoliderCount)
        {
            Instantiate(solider, spawnPoint.position, Quaternion.identity);
            index++;

        }
    }
}
