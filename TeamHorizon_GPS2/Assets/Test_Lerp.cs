using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Lerp : MonoBehaviour {

    Rigidbody rb;
    public Vector3 StartingPos;
    public Vector3 EndPos;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
        StartingPos = transform.position;
        EndPos = StartingPos;
        EndPos.x += 30;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position =  Vector3.Lerp(StartingPos, EndPos, 3f * Time.deltaTime),0,0);
	}
}
