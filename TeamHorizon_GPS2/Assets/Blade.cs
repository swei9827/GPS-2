using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour {

	void OnCollisionEnter(Collision col)
    {
        Debug.Log("This");
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
        }
        else if (col.gameObject.tag == "Environment")
        {
            //hitEnvironment = true;
        }
        else if (col.gameObject.tag == "Hazard")
        {
            Debug.Log("Col");
            col.gameObject.GetComponent<TreeFallHazard>().health -= 2;
        }
    }
}
