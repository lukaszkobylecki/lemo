using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOctopus : Unit
{
    public GameObject Tentacles;

    private GameObject player;
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool attacking;
    private bool special;

    void Start ()
    {
        attacking = false;
        special = false;
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        facingRight = true;

    }
	
	void Update ()
    {
        if (IsDead())
            Death();

        if (!attacking)
            FollowPlayer();
    }

    void FollowPlayer()
    {
        if (player == null)
        {
            rb2d.velocity = Vector2.zero;
            return;
        }

        float playerX = player.transform.position.x;
        float enemyX = transform.position.x;

        if (enemyX < playerX)
        {
            rb2d.velocity = new Vector2(movingSpeed, rb2d.velocity.y);

            if (facingRight)
                Flip();
        }
        else if (enemyX > playerX)
        {
            rb2d.velocity = new Vector2(-movingSpeed, rb2d.velocity.y);

            if (!facingRight)
                Flip();
        }
    }

    void Attack()
    {
        attacking = true;
        anim.SetBool("attacking", attacking);
        rb2d.velocity = Vector2.zero;
    }

    void StopAttack()
    {
        attacking = false;
        anim.SetBool("attacking", attacking);
    }

    void SpecialAttack()
    {
        StartCoroutine(Special());
    }

    IEnumerator Special()
    {
        attacking = true;
        rb2d.velocity = Vector2.zero;
        anim.Play("octopusSpecialAttack");
        yield return new WaitForSeconds(1.5f);

        Tentacles.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Tentacles.SetActive(false);
        attacking = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            other.gameObject.SendMessage("ReceiveDamage", attackPower);
     
    }
}
