using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {

    public int ObsHealth;

	void Update () {
	    if(ObsHealth <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
