using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private GameObject bullet;
    public GameObject burningBullet;
    public GameObject shockwaveBullet;
    public GameObject hydraMouth;
    private float minShootPower = 100;
    private float maxShootPower = 700;
    float randPower;

      //  void Awake()
      //   {
      //      //Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
      //  }

    void Breathe()
    {
        float rand = Random.Range(1, 3);

        for (int i = 0; i < rand; i++)
        {
            float num = Random.Range(0f, 1f);
            if (num < 0.5f)
                bullet = Instantiate(burningBullet, hydraMouth.transform.position, Quaternion.identity);
            else
                bullet = Instantiate(shockwaveBullet, hydraMouth.transform.position, Quaternion.identity);

            Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();

            randPower = Random.Range(minShootPower, maxShootPower);
            rb2d.AddForce(new Vector2(-randPower, randPower / 2));
        }

    }

  
}
