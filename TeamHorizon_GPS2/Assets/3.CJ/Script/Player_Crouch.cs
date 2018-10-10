using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Crouch : MonoBehaviour {

    public Transform yawRoot;
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
        if (Input.GetMouseButtonDown(0))
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
                yawRoot.Translate(0f, -1.2f, 0f);
                //text.text = "Invulnerable";
            }
            else
            {
                yawRoot.Translate(0f, 1.2f, 0f);
                //text.text = "Vulnerable";
            }
        }
    }
}
