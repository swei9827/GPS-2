using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Crouch : MonoBehaviour {

    public static bool isCrouch = false;
    //public Text text;

    // Use this for initialization
    void Start()
    {
        //text.text = "Vulnerable";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isCrouch == false)
            {
                isCrouch = true;
            }
            else
            {
                isCrouch = false;
            }

            if (isCrouch == true)
            {
                transform.Translate(new Vector3(0.0f, -1.2f, 0.0f));
                //text.text = "Invulnerable";;
            }
            else
            {
                transform.Translate(new Vector3(0.0f, 1.2f, 0.0f));
                //text.text = "Vulnerable";
            }
        }
    }

 
}
