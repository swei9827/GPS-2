using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuControl : MonoBehaviour {

    public GameObject PauseMenu;
    public GameObject PauseMenuMainPage;
    public GameObject SettingPageInPauseMenu;
    public GameObject PlayerReturnMainMenuConfirmationPage;
    public Level level;
    public static bool canPause;

	// Use this for initialization
	void Start () {
        level = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<Level>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PauseMenuPopOut()
    {
        level.SetTimeScale(0.0f);
        PauseMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        level.SetTimeScale(1.0f);
    }

    // Confirmation player want to quit to main menu scene
    public void ConfirmPlayerWantToReturnMainMenuScene()
    {
        PauseMenuMainPage.SetActive(false);
        PlayerReturnMainMenuConfirmationPage.SetActive(true);
    }

    public void PlayerAnswerNo()
    {
        PlayerReturnMainMenuConfirmationPage.SetActive(false);
        PauseMenuMainPage.SetActive(true);
    }

    public void PlayerAnswerYes()
    {
        ReturnMainMenu();
    }

    public void ReturnMainMenu()
    {
        var audio = FindObjectOfType<AudioManager>();
        audio.Stop("Stage 1");
        audio.Play("OpeningBGM");
        SceneManager.LoadScene(0);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(1);
        level.SetTimeScale(1.0f);        
    }

    public void SettingPage()
    {
        PauseMenuMainPage.SetActive(false);
        SettingPageInPauseMenu.SetActive(true);
    }

    public void ReturnToPauseMenuPageFromSettingPage()
    {
        SettingPageInPauseMenu.SetActive(false);
        PauseMenuMainPage.SetActive(true);
    }
}
