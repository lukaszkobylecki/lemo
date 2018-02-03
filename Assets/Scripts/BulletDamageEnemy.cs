using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageEnemy : MonoBehaviour
{
    public GameObject deathParticleBluePrefab;
    private float damage;
    private float lifetime = 1f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetBulletDamage(float dmg)
    {
        damage = dmg;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("ReceiveDamage", damage);
        }

        Instantiate(deathParticleBluePrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
