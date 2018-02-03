using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static float enemyNumber;
    public GameObject Hydra;

    private bool completed = false;

    void Start()
    {
        LoadLevel();
    }

    void Update()
    {
        if (enemyNumber <= 0)
        {
            if (!completed)
            {
                completed = true;
                GameManager.instance.LevelCompleted();
            }
        }

    }

    void LoadLevel()
    {
        TextManager.instance.EnablePanel(false);

        completed = false;
        enemyNumber = GameManager.enemyNumber;
        StartLevel();
    }

    void StartLevel()
    {
        TextManager.instance.ShowStartingInfo();

        int walkerNumber = 0;
        int fuckersNumber = 0;
        int octoNumber = 0;
        int flyerNumber = 0;

        for (int i = 0; i < enemyNumber; i++)
        {
            float rand = Random.Range(0f, 4f);

            if (rand < 1)
                walkerNumber++;
            else if (rand < 2)
                fuckersNumber++;
            else if (rand < 3)
                octoNumber++;
            else
                flyerNumber++;
        }

        if (GameManager.waveNumber == 3)
        {
            Hydra.SetActive(true);
            enemyNumber++;
        }

        float spawnBreak = Random.Range(1f, 3f);
        LaunchOctopus.number = octoNumber;
        LaunchOctopus.spawnBreak = spawnBreak;

        spawnBreak = Random.Range(1f, 3f);
        LaunchMotherfucker.number = fuckersNumber;
        LaunchMotherfucker.spawnBreak = spawnBreak;

        spawnBreak = Random.Range(1f, 3f);
        LaunchFlyers.number = flyerNumber;
        LaunchFlyers.spawnBreak = spawnBreak;

        spawnBreak = Random.Range(1f, 3f);
        LaunchWalkers.number = walkerNumber;
        LaunchWalkers.spawnBreak = spawnBreak;


        GameObject child = transform.FindChild("LaunchOctopus").gameObject;
        child.SendMessage("StartInvoke");
        child = transform.FindChild("LaunchMotherfucker").gameObject;
        child.SendMessage("StartInvoke");
        child = transform.FindChild("LaunchFlyers").gameObject;
        child.SendMessage("StartInvoke");
        child = transform.FindChild("LaunchWalkers").gameObject;
        child.SendMessage("StartInvoke");
    }
}
