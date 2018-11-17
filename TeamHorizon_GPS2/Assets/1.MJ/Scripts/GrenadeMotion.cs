using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeMotion : MonoBehaviour {

    public int MoveSpeed = 10;
    public Transform Player; 


	
	void Throwing() {
        //projectile.transform.position = transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        //Rigidbody rb = projectile.GetComponent<Rigidbody>();
        //rb.velocity = transform.forward * 12;
    }
	
	

}
