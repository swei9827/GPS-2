using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Move01 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        LeftMovement();
	}
    bool right = false;
    bool left = false;
    float speedZ = 20f;
    float speedX = 3f;

    // Update is called once per frame
    void Update () {
        this.transform.Translate(Vector3.forward * Time.deltaTime * speedZ);
        LeftMovement();
        /*Perform();
        if(left == true && right == false)
        {
            Perform();
        }
        else if(left == false && right == true)
        {
            Perform2();
        }*/
	}

    /*void Perform()
    {
        LeftMovement();
        if(this.transform.position.x <= -3)
        {
            left = false;
            right = true;
        }
    }

    void Perform2()
    {
        RightMovement();
        if(this.transform.position.x >= 3)
        {
            left = true;
            right = false;
        }
    }*/


    void LeftMovement()
    {
        speedX = 3;
        this.transform.Translate(Vector3.left * Time.deltaTime * 3f);
        Invoke("RightMovement", 2);
    }

    void RightMovement()
    {
        this.transform.Translate(Vector3.right * Time.deltaTime * 3f);
        Invoke("LeftMovement", 2); 
    }


}

