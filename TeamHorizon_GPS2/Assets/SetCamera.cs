using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour {

    Vector3 playerPos;

	void Start () {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y + 10, playerPos.z);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
