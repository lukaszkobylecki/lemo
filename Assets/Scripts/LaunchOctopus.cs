using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchOctopus : MonoBehaviour
{
    public GameObject enemyOctopus;
    public static float spawnBreak;
    public static int number;

    void StartInvoke()
    {
        StartCoroutine(Launch());
    }

    IEnumerator Launch()
    {
        yield return new WaitForSeconds(GameManager.gameStartDelay + 1);

        for (int i = 0; i < number; i++)
        {
            Instantiate(enemyOctopus, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnBreak);
        }
    }
}
