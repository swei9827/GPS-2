using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesHazard : MonoBehaviour {
    int health = 2;
    public float nextMelee = 0.0f;
    public float meleeHit = 0.2f;

    void OnMouseDown()
    {
        health -= 1;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PlayerBlade"))
        {
            if (Time.deltaTime > nextMelee)
            {
                nextMelee = Time.deltaTime + meleeHit;
                health -= 2;
            }
        }
    }
}
