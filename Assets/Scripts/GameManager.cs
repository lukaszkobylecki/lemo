using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject desk;
    public static bool gameStarted = false;
    public static float gameStartDelay = 8;
    public static float money = 1000;
    public static int waveNumber = 1;
    public static int startingEnemyNumber = 8;
    public static int enemyNumber = startingEnemyNumber;

    public static bool speedUp;
    public static float shootingExhaust = 0.5f;
    public static float bulletLifeTime = 0.23f;
    public static float grenadeFreq = 7f;
    public static float grenadePow = 36f;
    public static bool jumpBoard = false;
    public static bool floatBoard = false;


    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (jumpBoard)
        {
            desk.gameObject.SetActive(true);
        }
    }

    public void LevelCompleted()
    {   
        waveNumber++;
        TextManager.instance.UpdateWaveText(waveNumber);
        enemyNumber += (int)Mathf.Sqrt(enemyNumber) * 2;

        TextManager.instance.ShowLevelCompleted();
    }
}
