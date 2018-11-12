using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BrightnessSetting : MonoBehaviour {

    Image img;
    Color tempColor;
    public Slider BrightnessSliderInMainMenu;
    public Slider BrightnessSliderInGame;

    Scene currentScene;

	// Use this for initialization
	void Start () {
        img = this.GetComponent<Image>();
        tempColor = img.color;

        //BrightnessSliderInMainMenu = GameObject.FindGameObjectWithTag("B_Slider_MainMenu").GetComponent<Slider>();
        //BrightnessSliderInGame = GameObject.FindGameObjectWithTag("B_Slider_Game").GetComponent<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if(sceneName == "MainMenu")
        {
            tempColor.a = BrightnessSliderInMainMenu.value;
            img.color = tempColor;
        }
        else if(sceneName == "ScaledTutorialScene")
        {
            tempColor.a = BrightnessSliderInGame.value;
            img.color = tempColor;
        } 
	}
}
