using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

	void Start () {
        Invoke("DestroySelf",1.0f);
	}
	
	void DestroySelf()
    {
        Destroy(this,)
    }
}
