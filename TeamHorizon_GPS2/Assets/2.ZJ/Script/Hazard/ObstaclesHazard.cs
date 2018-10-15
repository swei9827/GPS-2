using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesHazard : MonoBehaviour {
    int health = 2;
    void OnMouseDown()
    {
        health -= 1;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
