using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject target;
    private float trackSpeed = 10;


    void LateUpdate()
    {
        if (target != null)
        {
            float x = IncrementTowards(transform.position.x, target.transform.position.x, trackSpeed);
            float y = IncrementTowards(transform.position.y, target.transform.position.y, trackSpeed);
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }

    private float IncrementTowards(float n, float target, float a)
    {
        if (n == target)
            return n;
        else
        {
            float dir = Mathf.Sign(target - n);
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target;
        }
    }
}
