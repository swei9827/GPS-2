using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour {

    public GameObject particle;
    RaycastHit hit;

	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            // Construct a ray from the current touch coordinates
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Create a particle if hit
            if (Physics.Raycast(ray,out hit))
            {
                //Debug.DrawRay(transform.position,(hit.))
                Instantiate(particle,transform.position,transform.rotation);
            }                
        }
    }
}
