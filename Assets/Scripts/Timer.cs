using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static void AddTime(ref float timer)
    {
        timer += Time.deltaTime;
    }

    public static void SubtractTime(ref float timer)
    {
        timer += Time.deltaTime;
    }
}
