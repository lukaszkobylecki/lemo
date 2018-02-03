using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydraAttack : MonoBehaviour
{
    public Animator anim;
    public float shootBreak;
    public int hydraHP;
    bool dead = false;

    void Start()
    {
        hydraHP = 5;
        anim.GetComponent<Animator>();
        shootBreak = 4f;
        InvokeRepeating("Attack", Random.Range(2f, 5f), shootBreak);
    }

    void Attack()
    {
        anim.Play("HydraAttack");
    }

    void Update()
    {
        if (dead)
        {
            
            return; 
        }

        if (hydraHP <= 0)
        {
            CancelInvoke();
            anim.Play("HydraDeath");
            dead = true;
            LevelManager.enemyNumber--;
        }
        if (GameManager.speedUp)
            GetPsyched();
    }

    void GetPsyched()
    {
        shootBreak -= 1f;
        CancelInvoke();
        GameManager.speedUp = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            hydraHP--;
        }
    }
}
