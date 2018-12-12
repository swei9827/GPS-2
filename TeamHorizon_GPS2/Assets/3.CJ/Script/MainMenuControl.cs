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
    AudioManager audioM;

    // Use this for initialization
    void Start () {
		audioM = FindObjectOfType<AudioManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void QuitGame()
    {
        audioM.Play("BUTTON");
        Application.Quit();
    }

    public void StartGame()
    {
        audioM.Stop("TITLE");
        audioM.Play("TUTORIAL");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void StageSelect()
    {
        audioM.Play("BUTTON");
        MenuPage.SetActive(false);
        SelectStage.SetActive(true);
    }

    public void Setting()
    {
        audioM.Play("BUTTON");
        MenuPage.SetActive(false);
        SettingPage.SetActive(true); 
    }

    public void BackMenuPage()
    {
        audioM.Play("BUTTON");
        SettingPage.SetActive(false);
        CreditPage1.SetActive(false);
        CreditPage2.SetActive(false);
        StageSelectPage.SetActive(false);
        MenuPage.SetActive(true);
    }

    public void CreditPage()
    {
        audioM.Play("BUTTON");
        MenuPage.SetActive(false);
        CreditPage1.SetActive(true);
    }

    public void CreditChangeSecondPageButton()
    {
        audioM.Play("BUTTON");
        CreditPage1.SetActive(false);
        CreditPage2.SetActive(true);
    }

    public void CreditBackFirstPageButton()
    {
        audioM.Play("BUTTON");
        CreditPage2.SetActive(false);
        CreditPage1.SetActive(true);
    }
}
