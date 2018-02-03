using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWave : MonoBehaviour
{
    CircleCollider2D coll;
    float increaseFactor = 0.3f;
    int i = 0;

    void Start()
    {
        coll = GetComponent<CircleCollider2D>();
    }

	void Update ()
    {
        if (i > 20)
            Destroy(gameObject);
        transform.localScale += new Vector3(increaseFactor, increaseFactor, 0);
        coll.radius += 0.02f;
        i++;
	}
}
