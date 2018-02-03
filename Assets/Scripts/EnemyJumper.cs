using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumper : Unit
{
    public float jumpForce;
    
    private Animator anim;
    private Rigidbody2D rb2d;
    private GameObject player;
    private bool grounded;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        grounded = false;
    }

    void Update()
    {
        
        if (IsDead())
            Death();

        anim.SetFloat("vSpeed", rb2d.velocity.y/10);

        FollowPlayer();
        if (grounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        grounded = false;
        rb2d.AddForce(new Vector2(0, jumpForce));
    }

    void FollowPlayer()
    {
        if (player == null)
            return;

        float playerX = player.transform.position.x;
        float enemyX = transform.position.x;

        if (enemyX < playerX)
            rb2d.velocity = new Vector2(movingSpeed, rb2d.velocity.y);

        else
            rb2d.velocity = new Vector2(-movingSpeed, rb2d.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
        else if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("ReceiveDamage", collisionDamage);
        }
    }
}
