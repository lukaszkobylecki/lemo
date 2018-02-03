using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchMotherfucker : MonoBehaviour
{
    public GameObject motherFucker;
    public static float spawnBreak;
    public static int number;

    private float yAmplitude = 3f;

    void StartInvoke()
    {
        StartCoroutine(Launch());
    }

    IEnumerator Launch()
    {
        yield return new WaitForSeconds(GameManager.gameStartDelay + 1);

        for (int i = 0; i < number; i++)
        {
            float randY = Random.Range(-1, yAmplitude);
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + randY, transform.position.z);

            Instantiate(motherFucker, pos, Quaternion.identity);
            yield return new WaitForSeconds(spawnBreak);
        }
    }
}
