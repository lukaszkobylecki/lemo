using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float damage;
    public float duration;

	void Start ()
    {
        Destroy(gameObject, duration);		
	}
	
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("ReceiveDamage", damage);
        }
    }
}
