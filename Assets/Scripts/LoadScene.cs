using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadScenee(int level)
    {
        StartCoroutine(Load(level));
    }

    public IEnumerator Load(int level)
    {
        yield return new WaitForSeconds(0.2f);

        SceneManager.LoadScene(level);
    }
}
