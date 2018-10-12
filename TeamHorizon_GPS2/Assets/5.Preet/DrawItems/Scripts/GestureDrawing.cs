using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureDrawing : MonoBehaviour
{

    public Texture2D text;
    public Texture2D display;
    bool iniCount = false;
    public float timer = 1.0f;
    GameObject image;

    public GameObject player;


    // Update is called once per frame
    void Update()
    {

        if (image == null)
        {
            return; //return the value
        }
        else if (iniCount)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                image.GetComponent<RawImage>().enabled = false;
                iniCount = false;
            }
        }
    }


    void onGestureCorrect()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
        Debug.Log("CORRECT"); //this isnt working wtf! how to impliment without plugin
    }

    //player failed in the gesture
    void onGestureWrong()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        Debug.Log("WRONG");
    }
}


//time.timescale = 0.5; // to slow doen the game during qte
