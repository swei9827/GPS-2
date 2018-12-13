using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Crouch : MonoBehaviour {

    public bool isCrouch = false;
    public Transform player;
    public ControlCenter cc;

    int playerArea = 0;

    //public Text text;

    // Use this for initialization
    void Start()
    {
        //text.text = "Vulnerable";
    }

    // Update is called once per frame
    void Update()
    {
        playerArea = cc.levelStatus;
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
                if(playerArea == 1)
                {
                    transform.Translate(new Vector3(-1.2f, 0.0f, 0.0f));
                }
                transform.Translate(new Vector3(0.0f, -1.2f, 0.0f));
                //player.transform.Translate(new Vector3(0.0f, -1.2f, 0.0f));
                //text.text = "Invulnerable";;
            }
            else
            {
                if(playerArea == 1)
                {
                    transform.Translate(new Vector3(1.2f, 0.0f, 0.0f));
                }
                transform.Translate(new Vector3(0.0f, 1.2f, 0.0f));
                //player.transform.Translate(new Vector3(0.0f, 1.2f, 0.0f));
                //text.text = "Vulnerable";
            }
        }
    }

    public void Crouch()
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
            if (playerArea == 1)
            {
                transform.Translate(new Vector3(-1.2f, 0.0f, 0.0f));
            }
            else if(playerArea == 7)
            {
                transform.Translate(new Vector3(1.2f, 0.0f, 0.0f));
            }
            else if(playerArea == 9)
            {
                transform.Translate(new Vector3(0.0f, -1.2f, 0.0f));
            }
            else if (playerArea == 11)
            {
                transform.Translate(new Vector3(-1.2f, 0.0f, 0.0f));
            }
            else if (playerArea == 13)
            {
                transform.Translate(new Vector3(0.0f, -1.2f, 0.0f));
            }
            //player.transform.Translate(new Vector3(0.0f, -1.2f, 0.0f));
            //text.text = "Invulnerable";;
        }
        else
        {
            if (playerArea == 1)
            {
                transform.Translate(new Vector3(1.2f, 0.0f, 0.0f));
            }
            else if (playerArea == 7)
            {
                transform.Translate(new Vector3(-1.2f, 0.0f, 0.0f));
            }
            else if (playerArea == 9)
            {
                transform.Translate(new Vector3(0.0f, 1.2f, 0.0f));
            }
            else if (playerArea == 11)
            {
                transform.Translate(new Vector3(1.2f, 0.0f, 0.0f));
            }
            else if (playerArea == 13)
            {
                transform.Translate(new Vector3(0.0f, 1.2f, 0.0f));
            }
            //player.transform.Translate(new Vector3(0.0f, 1.2f, 0.0f));
            //text.text = "Vulnerable";
        }
    }

    public void onPress()
    {
        isCrouch = true;
        if(isCrouch && cc.GetComponent<ControlCenter>().status == STATUS.CROUCH)
        {
            if (playerArea == 1)
            {
                transform.Translate(new Vector3(-1.2f, 0.0f, 0.0f));
            }
            else if (playerArea == 7)
            {
                transform.Translate(new Vector3(1.2f, 0.0f, 0.0f));
            }
            else if (playerArea == 9)
            {
                transform.Translate(new Vector3(0.0f, -1.2f, 0.0f));
            }
            else if (playerArea == 11)
            {
                transform.Translate(new Vector3(-1.2f, 0.0f, 0.0f));
            }
            else if (playerArea == 13)
            {
                transform.Translate(new Vector3(0.0f, -0.5f, 0.0f));
            }
        }
    }

    public void onRelease()
    {
        isCrouch = false;
        if(!isCrouch && cc.GetComponent<ControlCenter>().status == STATUS.CROUCH)
        {
            if (playerArea == 1)
            {
                transform.Translate(new Vector3(1.2f, 0.0f, 0.0f));
            }
            else if (playerArea == 7)
            {
                transform.Translate(new Vector3(-1.2f, 0.0f, 0.0f));
            }
            else if (playerArea == 9)
            {
                transform.Translate(new Vector3(0.0f, 1.2f, 0.0f));
            }
            else if (playerArea == 11)
            {
                transform.Translate(new Vector3(1.2f, 0.0f, 0.0f));
            }
            else if (playerArea == 13)
            {
                transform.Translate(new Vector3(0.0f, 0.5f, 0.0f));
            }
        }        
    }
}
