using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNewShoot : MonoBehaviour {

    public GameObject particle;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // Construct a ray from the current touch coordinates
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Create a particle if hit
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                Debug.Log("hit");
                Instantiate(particle, hit.point,transform.rotation);
            }
            else
            {
                Debug.Log("miss");
            }
                
        }
    }
}
