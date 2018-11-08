using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {

    public int ObsHealth;
    public Transform Player;
    public float MaxDistance;

    void OnMouseDown()
    {
        float distance = Vector3.Distance(Player.position, transform.position);
        if(distance <= MaxDistance)
        {
            ObsHealth -= 1;
        }        
    }

    void Update () {
	    if(ObsHealth <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
