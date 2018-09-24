using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Move_Try : MonoBehaviour {

    float speed = 12f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        Invoke("SlowDown", 3);
	}

    void SlowDown()
    {
        speed = 5f;
    }
}
