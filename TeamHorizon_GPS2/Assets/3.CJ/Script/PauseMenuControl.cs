using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuControl : MonoBehaviour {

    public GameObject PauseMenu;
    Level level;

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
}
