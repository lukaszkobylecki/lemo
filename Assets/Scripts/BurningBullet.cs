using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningBullet : MonoBehaviour
{
    public GameObject Fire;
    public float damage;

    public void SetBulletDamage(float dmg)
    {
        damage = dmg;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Instantiate(Fire, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("ReceiveDamage", damage);
            Destroy(gameObject);
        }
    }
}
