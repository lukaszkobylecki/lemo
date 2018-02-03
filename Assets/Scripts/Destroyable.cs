using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour {

	void DestroyMe () {
        Destroy(gameObject);
	}

}
