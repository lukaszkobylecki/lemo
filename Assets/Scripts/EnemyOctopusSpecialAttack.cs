using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOctopusSpecialAttack : MonoBehaviour
{
    public float attackExhaust;

    private float startDelay = 0;
    private float timer = 0;
    private bool attacking = false;

    void Update()
    {
        if ((attacking) && (timer > attackExhaust))
        {
            attacking = false;
            timer = 0;
        }
        else if ((attacking) && (timer < attackExhaust))
        {
            Timer.AddTime(ref timer);
        }

        Timer.AddTime(ref startDelay);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (startDelay < 4)
                return;

            if (!attacking)
            {
                timer = 0;
                attacking = true;
                gameObject.SendMessageUpwards("SpecialAttack");
            }
        }
    }
}
