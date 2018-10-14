using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDestroy : MonoBehaviour {

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Melee")
        {
            Debug.Log("Destroy");
        }
    }
}
