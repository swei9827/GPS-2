using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFallHazard : MonoBehaviour
{
    float fall = 90;
    public int health = 2;
    public bool FallenTree = false;
    private Material mat;

    private void Start()
    {
        mat = this.GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if (health <= 0)
        {
            if(mat.color.a > 0)
            {
                Color newColor = mat.color;
                newColor.a -= Time.deltaTime;
                mat.color = newColor;
                gameObject.GetComponent<MeshRenderer>().material = mat;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnMouseDown()
    {
        if (FallenTree)
        {
            health -= 1;
            if (health <= 0)
            {
                //Destroy(this.gameObject);
            }
        }        
    }

    public void TreeFalling()
    {
        if (FallenTree == false)
        {
            if (fall > 80 && fall <= 90)
            {
                fall -= Time.deltaTime * 10;
            }
            else if (fall > 70 && fall < 80)
            {
                fall -= Time.deltaTime * 15;
            }
            else if (fall > 60 && fall < 70)
            {
                fall -= Time.deltaTime * 20;
            }
            else if (fall > 45 && fall < 60)
            {
                fall -= Time.deltaTime * 30;
            }
            else if (fall > 25 && fall < 45)
            {
                fall -= Time.deltaTime * 40;
            }
            else if (fall > 15 && fall < 25)
            {
                fall -= Time.deltaTime * 40;
            }
            else if (fall <= 15)
            {
                FallenTree = true;
            }

            transform.rotation = Quaternion.Euler(-Mathf.Clamp(fall, 5, 90), transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }

}
