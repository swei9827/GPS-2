using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScript : MonoBehaviour {


	
	// Update is called once per frame
	void Update ()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            Debug.Log("PAUSED");
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }


    }
}
