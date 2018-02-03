using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyer : Unit
{
    public GameObject Bullet;
    public float swingTime;
    public float shootingFrequency;
    public float bulletDamage;
    public float bulletSpeed;

    private float swingTimer;
    private Rigidbody2D rb2d;
    private float yStopPosition;
    private float xStopPosition;
    private bool movingHorizontally;
    private Transform player;
    private bool attacking;
    private float amplitudeX = 7.0f;
    private float amplitudeY = 0.3f;
    private float omegaX = 1.0f;
    private float omegaY = 5.0f;
    private float index = 0;

    void Start ()
    {
        attacking = false;
        movingHorizontally = false;
        swingTimer = 0;
        yStopPosition = 7f;
        xStopPosition = -3;
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
		if (IsDead())
        {
            Death();
        }

        Move();
    }

    void Move()
    {
        if (movingHorizontally)
        {
            MoveHorizontally();

            if (!attacking)
                InvokeRepeating("Attack", 0, shootingFrequency);

            attacking = true;
        }
        else if ((transform.position.y < yStopPosition) && (!movingHorizontally))
        {
            rb2d.velocity = new Vector2(-Mathf.Sqrt(2) * movingSpeed / 2, Mathf.Sqrt(2) * movingSpeed / 2);
        }
        else if ((transform.position.x > xStopPosition) && (!movingHorizontally))
        {
            rb2d.velocity = new Vector2(-movingSpeed, 0);
        }
        else
        {
            movingHorizontally = true;
        }
    }

    void MoveHorizontally()
    {
        index += Time.deltaTime / 2;
        float x = amplitudeX * Mathf.Cos(omegaX * index) + xStopPosition - amplitudeX;
        float y = Mathf.Abs(amplitudeY * Mathf.Sin(omegaY * index)) + yStopPosition;
        transform.localPosition = new Vector3(x, y, 0);

        Timer.AddTime(ref swingTimer);
    }

    void Attack()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        GameObject bullet = Instantiate(Bullet, pos, Quaternion.identity) as GameObject;
        bullet.gameObject.SendMessage("SetBulletDamage", bulletDamage);

        Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();

        rb2d.velocity = new Vector2(0, -bulletSpeed);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("ReceiveDamage", collisionDamage);
        }
    }
}
