using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveBullet : MonoBehaviour
{
    public GameObject shockWave;

    void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.tag == "Ground") || (other.gameObject.tag == "Player"))
        {
            Instantiate(shockWave, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
