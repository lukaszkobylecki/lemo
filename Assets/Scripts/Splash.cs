using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public GameObject Bullet;
    public float BulletSpeed;

    private float grenadePower;

    void Start()
    {
        grenadePower = GameManager.grenadePow;
    }

    void ExplodeAfterDelay(float[] grenadeInfo)
    {
        StartCoroutine(Explode(grenadeInfo[0], grenadeInfo[1]));
    }

    IEnumerator Explode(float delay, float damage)
    {
        yield return new WaitForSeconds(delay);

        float rotation = 0;

        for (int i = 0; rotation < 360; i++)
        {
            rotation = i * grenadePower;

            GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation) as GameObject;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.gameObject.SendMessage("SetBulletDamage", damage);

            bullet.transform.eulerAngles = new Vector3(0, 0, rotation);
            rb.velocity = bullet.transform.up * -BulletSpeed;

            bullet.transform.eulerAngles = new Vector3(0, 0, rotation + 90);
            bullet.transform.localScale = new Vector3(1.25f, 2f, 1f);
        }

        Destroy(gameObject);
    }
}
