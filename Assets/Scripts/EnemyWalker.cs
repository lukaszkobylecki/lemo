using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : Unit
{
    public BoxCollider2D attackCollider;

    private GameObject player;
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool attacking;
    private bool grounded;

    public LayerMask whatIsGround;
    public Transform groundCheck;

    void Start ()
    {
        grounded = false;
        attacking = false;
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        facingRight = false;
        Flip();
    }
	
	
	void Update ()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 2f, whatIsGround);
        anim.SetBool("Grounded", grounded);
        if (IsDead())
            Death();
        anim.SetFloat("vSpeed", rb2d.velocity.y);
        if (!attacking)
            FollowPlayer();
	}

    void Attack()
    {
        attacking = true;
        anim.Play("WalkerAttack");
        rb2d.velocity = Vector2.zero;
    }

    void StopAttack()
    {
        attacking = false;
    }

    void FollowPlayer()
    {
        anim.SetFloat("Speed", rb2d.velocity.x);

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

    void PushOnCollision(float force)
    {
        rb2d.AddForce(new Vector2(2*force/3, force/2));
        StartCoroutine(StopForABit(0.3f));
    }
    
    IEnumerator StopForABit(float time)
    {
        float temp = movingSpeed; 
        movingSpeed = 0f;

        yield return new WaitForSeconds(time);

        movingSpeed = temp;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Player") || (other.gameObject.tag == "Ground"))
        {
            if (!grounded)
                Behave();
            grounded = true;

            BoxCollider2D coll = gameObject.GetComponent<BoxCollider2D>();
            coll.isTrigger = false;
        }
        else if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Death();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.tag == "Player") || (other.gameObject.tag == "Ground"))
        {
            if (other.gameObject.tag == "Player")
                other.gameObject.SendMessage("ReceiveDamage", collisionDamage);
        }
    }

    void Behave()
    {
        EnemyWalker script = GetComponent<EnemyWalker>();

        script.enabled = true;
    }
}
