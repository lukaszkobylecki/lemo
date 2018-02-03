using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Grenade;
    public float bulletSpeed;
    public float grenadeExhaust;
    public float grenadeThrowForce;
    public float grenadeDelay;
    public float grenadeDamage;
    public Transform PistolPoint;
    public Transform grenadePoint;
    public Animator anim;
    public float bulletLifeTime = 0.2f;

    private float grenadeTimer;
    private bool facingRight;
    private float xInput;
    private float yInput;
    private bool shootInput;
    private bool grenadeInput;
    private float timer;
    private PlayerController script;

	void Start ()
    {
        grenadeExhaust = GameManager.grenadeFreq;

        anim = GetComponent<Animator>();
        timer = 0;
        xInput = 0;
        yInput = 0;
        grenadeTimer = 10;
        shootInput = false;
        script = GetComponent<PlayerController>();
        facingRight = GetComponent<PlayerController>();
	}
	
	void Update ()
    {
        Timer.AddTime(ref timer);
        Timer.AddTime(ref grenadeTimer);
        GetInputs();
        GetFacingDirection();

        TryToAttack();
        TryToThrowGrenade();
    }

    void GetInputs()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        shootInput = Input.GetButtonDown("Fire1");
        grenadeInput = Input.GetButtonDown("Fire2");
    }

    void GetFacingDirection()
    {
        facingRight = script.facingRight;
    }

    void TryToAttack()
    {
        if ((shootInput) && (timer >= GameManager.shootingExhaust))
        {
            PlayerController.playerShoot = true;
            Attack();
            timer = 0;
        }
    }

    void Attack()
    {
        GameObject bullet = Instantiate(Bullet, PistolPoint.position, Quaternion.identity) as GameObject;

        bullet.SendMessage("SetBulletDamage", script.attackPower);
        SetBulletDirection(ref bullet);
    }

    void SetBulletDirection(ref GameObject bullet)
    {
        Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();

        if ((xInput == 0) && (yInput > 0))      //strzelanie w górę w miejscu
        {
            rb2d.velocity = new Vector2(0, bulletSpeed);
            bullet.transform.eulerAngles = new Vector3(0, 0, 90f);
            if (GameManager.jumpBoard)
                anim.Play("BoardUpShoot");
            else
                anim.Play("UpShoot");
        }
        else if ((xInput != 0) && (yInput > 0))  //strzelanie na ukos
        {
            if (facingRight)
            {
                rb2d.velocity = new Vector2(Mathf.Sqrt(2) * bulletSpeed / 2 , Mathf.Sqrt(2) * bulletSpeed / 2);
                bullet.transform.eulerAngles = new Vector3(0, 0, 45f);
                if (GameManager.jumpBoard)
                    anim.Play("BoardUpMidShoot");
                else
                    anim.Play("UpMidShoot");
            }
            else
            {
                rb2d.velocity = new Vector2(-Mathf.Sqrt(2) * bulletSpeed / 2, Mathf.Sqrt(2) * bulletSpeed / 2);
                bullet.transform.eulerAngles = new Vector3(0, 0, -45f);
                if (GameManager.jumpBoard)
                    anim.Play("BoardUpMidShoot");
                else
                    anim.Play("UpMidShoot");
            }
        }
        else                                //strzelanie tylko w poziomie
        {
            if (GameManager.jumpBoard)
                anim.Play("BoardMidShoot");
            else
                anim.Play("MidShoot");
            if (facingRight)
                rb2d.velocity = new Vector2(bulletSpeed, 0);
            else
                rb2d.velocity = new Vector2(-bulletSpeed, 0);
        }
    }

    void TryToThrowGrenade()
    {
        if ((grenadeTimer > grenadeExhaust) && (grenadeInput))
        {
            grenadeTimer = 0;
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(Grenade, grenadePoint.position, Quaternion.identity) as GameObject;
        if ((GameManager.jumpBoard) || (GameManager.floatBoard))
            anim.Play("BoardThrow");
        else
            anim.Play("Throw");
        Rigidbody2D rb2d = grenade.GetComponent<Rigidbody2D>();
        rb2d.AddForce(new Vector2(0, grenadeThrowForce));

        float[] grenadeInfo = new float[2];
        grenadeInfo[0] = grenadeDelay;
        grenadeInfo[1] = grenadeDamage;
        grenade.gameObject.SendMessage("ExplodeAfterDelay", grenadeInfo);
    }
}
