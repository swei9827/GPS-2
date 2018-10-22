using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour {

    public GameObject MenuPage;
    public GameObject SettingPage;
    public GameObject SelectStage;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void StageSelect()
    {
        MenuPage.SetActive(false);
        SelectStage.SetActive(true);

    }

    public void setting()
    {
        MenuPage.SetActive(false);
        SettingPage.SetActive(true); 
    }

    public void backMenuFromSetting()
    {
        SettingPage.SetActive(false);
        MenuPage.SetActive(true);
    }
}
