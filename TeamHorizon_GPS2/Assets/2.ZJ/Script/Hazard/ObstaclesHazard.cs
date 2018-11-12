using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesHazard : MonoBehaviour {
    int health = 2;
    public float nextMelee = 0.0f;
    public float meleeHit = 0.2f;
    public float slowDuration;
    public float slowSpeed;
    float originalSpeed = 10.0f;

    void OnMouseDown()
    {
        health -= 1;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

   

    
}
