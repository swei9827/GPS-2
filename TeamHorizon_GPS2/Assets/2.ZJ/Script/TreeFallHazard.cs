using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFallHazard : MonoBehaviour
{
    Rigidbody rb;
    float fall = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(fall < 10)
        {
            fall += Time.deltaTime * 2;
        }
        else if(fall > 10 && fall < 20) {
            fall += Time.deltaTime * 5;
        }
        else if(fall > 20 && fall< 30){
            fall += Time.deltaTime * 10;
        }
        else if (fall > 30 && fall < 40)
        {
            fall += Time.deltaTime * 15;
        }
        else if (fall > 40 && fall < 55)
        {
            fall += Time.deltaTime * 20;
        }
        else if (fall > 55 && fall < 85)
        {
            fall += Time.deltaTime * 25;
        }

        transform.rotation = Quaternion.Euler(-Mathf.Clamp(fall,0,85), 0, 0);
        
        
    }

}
