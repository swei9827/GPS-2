﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFallHazard : MonoBehaviour
{
    float fall = 1;
    int health = 2;
    public bool FallenTree = false;

    void OnMouseDown()
    {
        if (FallenTree)
        {
            health -= 1;
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }        
    }

    public void TreeFalling()
    {
        if (FallenTree == false)
        {
            if (fall < 10)
            {
                fall += Time.deltaTime * 5;
            }
            else if (fall > 10 && fall < 20)
            {
                fall += Time.deltaTime * 10;
            }
            else if (fall > 20 && fall < 30)
            {
                fall += Time.deltaTime * 15;
            }
            else if (fall > 30 && fall < 40)
            {
                fall += Time.deltaTime * 20;
            }
            else if (fall > 40 && fall < 55)
            {
                fall += Time.deltaTime * 25;
            }
            else if (fall > 55 && fall < 85)
            {
                fall += Time.deltaTime * 30;
            }
            else if (fall >= 85)
            {
                FallenTree = true;
            }

            transform.rotation = Quaternion.Euler(-Mathf.Clamp(fall, 0, 85), transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }
}
