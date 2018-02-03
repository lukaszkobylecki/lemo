using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public AudioClip deathSound;
    public GameObject deathParticle;
    public float health;
    public float movingSpeed;
    public float attackPower;
    public float collisionDamage;
    public float moneyValue;
    public bool facingRight;

    public virtual void ReceiveDamage(float damage)
    {
        health -= damage;

        if (health < 0)
        {
            health = 0;
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public virtual void Death()
    {
        TextManager.instance.UpdateMoneyText(moneyValue);
        SoundManager.instance.RandomizeSfx(deathSound);
        Instantiate(deathParticle, transform.position, transform.rotation);
        LevelManager.enemyNumber--;
        Destroy(gameObject);
    }

    public virtual bool IsDead()
    {
        if (health <= 0)
            return true;

        return false;
    }
}
