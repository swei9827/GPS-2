using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessSetting : MonoBehaviour {

    Image img;
    Color tempColor;
    public Slider BrightnessSliderInMainMenu;
    public Slider BrightnessSliderInGame;

	// Use this for initialization
	void Start () {
        img = this.GetComponent<Image>();
        tempColor = img.color;

        BrightnessSliderInMainMenu = GameObject.FindGameObjectWithTag("B_Slider_MainMenu").GetComponent<Slider>();
        BrightnessSliderInGame = GameObject.FindGameObjectWithTag("B_Slider_Game").GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        

        BrightnessSliderInGame.value = BrightnessSliderInMainMenu.value;
        BrightnessSliderInMainMenu.value = BrightnessSliderInGame.value;


        tempColor.a = BrightnessSliderInMainMenu.value;
        img.color = tempColor;
	}
}
