using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchWalkers : MonoBehaviour
{
    public GameObject enemyWalker;
    public static float spawnBreak;
    public static int number;

    private float xMin = -3000;
    private float xMax = -1000;
    private float yMin = 3000;
    private float yMax = 3500;

    void StartInvoke()
    {
        StartCoroutine(Launch());
    }

    IEnumerator Launch()
    {
        yield return new WaitForSeconds(GameManager.gameStartDelay + 1);

        for (int i = 0; i < number; i++)
        {
            GameObject mob = Instantiate(enemyWalker, transform.position, Quaternion.identity);

            Rigidbody2D rb2d = mob.GetComponent<Rigidbody2D>();

            float xRand = Random.Range(xMin, xMax);
            float yRand = Random.Range(yMin, yMax);

            rb2d.AddForce(new Vector2(xRand, yRand));

            yield return new WaitForSeconds(spawnBreak);
        }
    }
}
