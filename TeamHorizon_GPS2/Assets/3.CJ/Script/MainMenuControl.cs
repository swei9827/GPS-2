using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour {

    public GameObject MenuPage;
    public GameObject SettingPage;
    public GameObject SelectStage;
    public GameObject CreditPage1;
    public GameObject CreditPage2;
    public GameObject StageSelectPage;

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
        var audio = FindObjectOfType<AudioManager>();
        audio.Stop("TITLE");
        audio.Play("TUTORIAL");
        Time.timeScale = 1.0f;
    }

    public void StageSelect()
    {
        MenuPage.SetActive(false);
        SelectStage.SetActive(true);
    }

    public void Setting()
    {
        MenuPage.SetActive(false);
        SettingPage.SetActive(true); 
    }

    public void BackMenuPage()
    {
        SettingPage.SetActive(false);
        CreditPage1.SetActive(false);
        CreditPage2.SetActive(false);
        StageSelectPage.SetActive(false);
        MenuPage.SetActive(true);
    }

    public void CreditPage()
    {
        MenuPage.SetActive(false);
        CreditPage1.SetActive(true);
    }

    public void CreditChangeSecondPageButton()
    {
        CreditPage1.SetActive(false);
        CreditPage2.SetActive(true);
    }

    public void CreditBackFirstPageButton()
    {
        CreditPage2.SetActive(false);
        CreditPage1.SetActive(true);
    }
}
