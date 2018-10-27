using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE_Testing : MonoBehaviour {

    public float timer;
    public int GreenDelayTimer = 0;
    public int YellowDelayTimer = 2;
    public int RedDelayTimer = 4;
    public int DelayTimer = 6;
    public GameObject img;
    public Image tempColor;
    Color newColor;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += 0.1f*Time.deltaTime;/*
        if (timer >= GreenDelayTimer && timer < YellowDelayTimer)
        {
            img.GetComponent<Image>().color = Color.green;
            //img.GetComponent<Image>().color.a = 0.2f;
        }
        if (timer >= YellowDelayTimer && timer < RedDelayTimer )
        {
            img.GetComponent<Image>().color = Color.yellow;
        }
        if(timer>=RedDelayTimer)
        {
            img.GetComponent<Image>().color = Color.red;
            
        }
        if(timer>= DelayTimer)
        {
            timer = 0;
        }*/
        newColor = Color.Lerp(Color.yellow, Color.red, Time.time);
        img.GetComponent<Image>().color = newColor;
	}
}
