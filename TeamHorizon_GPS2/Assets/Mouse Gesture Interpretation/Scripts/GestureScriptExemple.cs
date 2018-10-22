﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GestureScriptExemple : MonoBehaviour {

	public Texture2D Texture2D;
	public Texture2D display;
	//bool iniCount = false;
	public bool QTETrigger = false;
	public float timer = 1.0f;
	GameObject image;

    public GameObject player;

	// Use this for initialization
	void Start () {
		image = GameObject.Find ("Display");
        //Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update () {
        if (image == null)
        {
            return;
        }
		//else if (iniCount)
		else if (QTETrigger)
        {
			timer -= Time.deltaTime;
			if (timer <= 0.0f) {
				image.GetComponent<RawImage> ().enabled = false;
                //iniCount = false;
                QTETrigger = false;
			}
		}
	}


    //This method is needed for the Gesture to run, here you set Texture for recognition and can set the sucess rate needed to be achived
    void setGestureConfig()
	{
        if(player == null)
        {
            Debug.LogError("<b>GestureScript:</b> The code need a reference to the object that has the playerInput script");
            return;
        }

		//player.GetComponent<GesturePlayer.PlayerInput> ().setTextureG (text);
       // player.GetComponent<GesturePlayer.PlayerInput>().setCorrectRate(0.8f);
        player.GetComponent<GesturePlayer.PlayerInput>().setTextureG (Texture2D);
        player.GetComponent<GesturePlayer.PlayerInput>().setCorrectRate(0.8f);
	}


    //This method happen when the player look to the object and can be used for a variety of things, in this exemple we use to display the Pattern
    void displayPattern()
	{
        if (image == null)
        {
            return;
        }
		else if (timer > 0.0f) {
            //iniCount = true;
            QTETrigger = true;

            image.GetComponent<RawImage> ().texture = display;
			image.GetComponent<RawImage> ().enabled = true;
		}
	}

    //This method happen when the player succefully do the gesture
    void onGestureCorrect()
	{
       this.gameObject.GetComponent<Renderer>().material.color = Color.green;
	}

    //This method happen when the player failed in the gesture
	void onGestureWrong()
	{
        this.gameObject.GetComponent<Renderer>().material.color = Color.red;
	}


    /* This method can be used if you wanna create a clickable object only, with no gesture, 
       use this method to detect when the object is click, the object must have the tag "Clickable"
    void onClick ()
    {

    }*/
}
