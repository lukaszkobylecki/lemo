using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    void Destroy()
    {
        GameManager.speedUp = true;
        Destroy(gameObject);
    }
}
