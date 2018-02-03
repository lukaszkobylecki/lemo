using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit
{
    public float jumpForce;
    public GameObject Shield;
    public float maxHealth;
    public Animator anim;
    public float dashExhaust;
    public float dashDuration;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public static bool playerShoot;
    public float hitExhaust;
    Color color;
    Color normalColor;
    public AudioClip hitSound;

    bool alphaUp = false;
    public static bool invulnerable = false;
    private float frequency = 2.0f;
    private float magnitude = 0.05f;
    private float dashingDirection;
    private bool dashing;
    private float dashTimer;
    private float dashBreakTimer;
    private float buttonExhaust;
    private float buttonCount;
    private bool grounded;
    private bool floated;
    private float xInput;
    private bool shield;
    private float shieldTimer;
    private float shieldDuration;
    private bool RightArrowLastClicked;
    private Rigidbody2D rb2d;
    private bool gotHit = false;
    private float hitTimer;

    float floatRadius = 0.2f;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        normalColor = color = GetComponent<SpriteRenderer>().material.color;

        health = maxHealth;
        facingRight = true;
        grounded = true;
        shield = false;
        shieldTimer = 0;
        buttonExhaust = 0.5f;
        buttonCount = 0;
        RightArrowLastClicked = false;
        dashBreakTimer = 0;
        dashTimer = 0;
        dashing = false;
        gotHit = false;
        hitTimer = 0;
    }

    void Shoot()
    {
        playerShoot = false;
    }

    void Start()
    {
        if (GameManager.jumpBoard)
           anim.Play("BoardIdle");
    }

    void FadingAlpha()
    {  
        if (!alphaUp)
            color.a -= 0.1f;
        else
            color.a += 0.1f;
        if ((color.a <= 0.2 && !alphaUp) || (color.a >= 0.8 && alphaUp)) alphaUp = !alphaUp;
        GetComponent<SpriteRenderer>().material.color = color;
    }

    void Update()
    {
        TextManager.instance.UpdateHealthText(health, maxHealth);
        if (IsDead())
            Death();

 

        anim.SetFloat("vSpeed", rb2d.velocity.y);

        if (invulnerable)
            FadingAlpha();

        if (playerShoot)
            Shoot();

        if (gotHit)
            Timer.AddTime(ref hitTimer);
        if (hitTimer > hitExhaust)
        {
            gotHit = false;
            hitTimer = 0;
        }

        GetInputs();
        ManageShield();
        Move();

        if ((Input.GetButtonDown("Jump")) && (grounded) && (transform.position.y < 2)) //HARD-FUCKING-CODING
        {
            Jump();
        }
    }


    void FixedUpdate()
    {
     
        floated = !Physics2D.OverlapCircle(groundCheck.position, floatRadius, whatIsGround);
        anim.SetBool("Grounded", !floated);
        if (floated)
        {
            float delta =  Mathf.Sin(Time.deltaTime * frequency) * magnitude;
            transform.position += transform.up * delta;
       }
    }

    void GetInputs()
    {
        xInput = Input.GetAxisRaw("Horizontal") * movingSpeed;
        anim.SetFloat("Speed", Mathf.Abs(xInput));
    }

    void TryToDash()
    {
        if (!GameManager.jumpBoard && !GameManager.floatBoard)
            return;

        if (dashBreakTimer > dashExhaust)
        {
            if (buttonCount == 0)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                    RightArrowLastClicked = true;
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    RightArrowLastClicked = false;
            }

            if ((Input.GetKeyDown(KeyCode.RightArrow) && (RightArrowLastClicked)) || (Input.GetKeyDown(KeyCode.LeftArrow) && (!RightArrowLastClicked)))
            {
                if ((buttonExhaust <= 0.5f) && (buttonCount > 0))
                {
                    buttonCount = 0;
                    dashBreakTimer = 0;
                    dashTimer = 0;
                    dashing = true;
                    dashingDirection = xInput;
                    Physics2D.IgnoreLayerCollision(8, 9, true);
                    Physics2D.IgnoreLayerCollision(8, 13, true);
                }
                else
                {
                    buttonExhaust = 0;
                    buttonCount += 1;
                }
            }

            if (buttonExhaust < 0.25f)
            {
                Timer.AddTime(ref buttonExhaust);
            }
            else
            {
                buttonCount = 0;
            }
        }

        if (dashing)
        {
            Timer.AddTime(ref dashTimer);
            anim.Play("Dash");
        }

        if (dashTimer > dashDuration)
        {
            dashing = false;
            Physics2D.IgnoreLayerCollision(8, 9, false);
            Physics2D.IgnoreLayerCollision(8, 13, false);
        }

        Timer.AddTime(ref dashBreakTimer);
    }

    void Move()
    {
        if (xInput > 0 && !facingRight)
            Flip();
        else if (xInput < 0 && facingRight)
            Flip();

        TryToDash();

        if (dashing)
        {
            rb2d.velocity = new Vector2(4 * dashingDirection * Time.fixedDeltaTime, rb2d.velocity.y);
            return;
        }

        rb2d.velocity = new Vector2(xInput * Time.fixedDeltaTime, rb2d.velocity.y);
    }

    void Jump()
    {
        grounded = false;
        if ((GameManager.jumpBoard) || (GameManager.floatBoard))
            rb2d.AddForce(new Vector2(0, jumpForce * 1.3f));
        else
            rb2d.AddForce(new Vector2(0, jumpForce));
    }


    public override void ReceiveDamage(float damage)
    {
        if (shield || gotHit || invulnerable)
            return;

        SoundManager.instance.PlaySingle(hitSound);
        GetComponent<SpriteRenderer>().material.color = new Color(255f, normalColor.g, normalColor.a);
        GetComponent<SpriteRenderer>().material.color = new Color(normalColor.r, normalColor.g, normalColor.a);
        //StartCoroutine(FlashWithColor(new Color(255F, 0, 0), 2f));
        Physics2D.IgnoreLayerCollision(8, 9, true);

        StartCoroutine(MakeInvulnerable(1.5f));
        gotHit = false;

        health -= damage;

        if (health < 0)
            health = 0;
    }

    public IEnumerator MakeInvulnerable(float amountOfTime)
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);                  
        invulnerable = true;
        yield return new WaitForSeconds(amountOfTime);
        invulnerable = false;
        GetComponent<SpriteRenderer>().material.color = normalColor;
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

    public override void Death()
    {
        Debug.Log("UMARŁEM");
    }

    void TurnOnShield(float duration)
    {
        shield = true;
        shieldTimer = 0;
        shieldDuration = duration;

        Shield.SetActive(true);
    }

    void ManageShield()
    {
        if (shield)
        {
            if (shieldTimer > shieldDuration)
            {
                Shield.SetActive(false);
                shield = false;
            }

            Timer.AddTime(ref shieldTimer);
        }
    }

    public void RestoreHealth(float hp)
    {
        health += hp;

        if (health > maxHealth)
            health = maxHealth;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
