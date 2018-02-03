using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotherfucker : Unit
{
    private float amplitudeY;
    private float omegaY = 5f;
    private Rigidbody2D rb2d;
	
    void Start()
    {
        movingSpeed = Random.Range(2f, 6f);
        amplitudeY = Random.Range(3f, 7f);
    }

	void Update ()
    {
        if (IsDead())
            Death();

        rb2d = GetComponent<Rigidbody2D>();
        Move();
	}

    void Move()
    {
        rb2d.velocity = new Vector2(-5, rb2d.velocity.y);
        transform.Translate(new Vector3(0, amplitudeY * Mathf.Sin(Time.time * omegaY), 0) * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            other.gameObject.SendMessage("ReceiveDamage", collisionDamage);
    }
}
