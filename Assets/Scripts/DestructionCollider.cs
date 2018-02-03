using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
            LevelManager.enemyNumber--;

        Destroy(other.gameObject);
    }
}
