using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironementTrigger : MonoBehaviour {

    public bool Tree;
    public int ETHealth;
    public Transform Player;
    public float MaxDistance;
    float fall = 90;

    public void ETDamage()
    {
        float distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= MaxDistance)
        {
            ETHealth -= 1;
        }
    }

    void Update()
    {
        if (Tree)
        {
            if(ETHealth <= 0)
            {
                TreeFalling();
            }
        }
    }

    void TreeFalling()
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

        transform.rotation = Quaternion.Euler(-Mathf.Clamp(fall, 5, 90), transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
