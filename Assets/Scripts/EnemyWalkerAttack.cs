using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkerAttack : MonoBehaviour
{
    public float attackExhaust;

    private float timer = 0;
    private bool attacking = false;

    void Update()
    {
        if ((attacking) && (timer > attackExhaust))
        {
            attacking = false;
            timer = 0;
            gameObject.SendMessageUpwards("StopAttack");
        }
        else if ((attacking) && (timer < attackExhaust))
        {
            Timer.AddTime(ref timer);
        } 
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!attacking)
            {
                timer = 0;
                attacking = true;
                gameObject.SendMessageUpwards("Attack");
            }
        }
    }
}
