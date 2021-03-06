﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level: MonoBehaviour
{
    public static Level instance;
    public float score;
    public float currency;
    [SerializeField]private float givenTime;
    private float timeLeft;
    private float extraTime;
    
    public LEVEL_STATE levelState;
    public List<GameObject> targetProfile;
    public bool isContinue;
    
    //!  UI
    public Text timeLeftUI;
    public Text ScoreUI;
    public Text endScore;
    public Text bonusTimeScore;
    public Text totalScore;

    public GameObject winUI;
    public GameObject loseUI;

    float minutes;
    float seconds;

    public bool isLevelCleared;

    void Awake()
    {
        instance = this;
    }

    void Start () {
        timeLeft = givenTime;
        score = 0;
        isContinue = false;
        isLevelCleared = false;
	}
	
    public void setScore(float s)
    {
        score = s;
    }
    
    public float getScore()
    {
        return score;
    }

	void Update () {

        minutes = Mathf.Floor(Mathf.RoundToInt(timeLeft) / 60);
        seconds = Mathf.Round(Mathf.RoundToInt(timeLeft) % 60);

        timeLeftUI.text = minutes.ToString("00") + " : " + seconds.ToString("00");
        ScoreUI.text = score.ToString();

        if(timeLeft <= 0)
        {
            loseUI.SetActive(true);
            SetTimeScale(0.0f);
        }

        if(levelState == LEVEL_STATE.PLAYING)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    timeLeft = 0;
                    levelState = LEVEL_STATE.FINISHED;
                }
            }           
        }
        else if (levelState == LEVEL_STATE.FINISHED)
        {
            Constants.TotalScore += (int)score;
            levelState = LEVEL_STATE.END;
            PlayerPrefs.SetInt("TotalScore", Constants.TotalScore);
        }
    }

    public void LoadScene(int scene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }

    public void Level1()
    {
        Time.timeScale = 1;
        AudioManager audio = FindObjectOfType<AudioManager>();
        audio.Play("LEVEL1");
        audio.Stop("TUTORIAL");
        SceneManager.LoadScene(3);
    }

    public void SetTimeScale(float ts)
    {
        //Time.timeScale = ts;
        if (!Constants.GameIsPaused)
        {
            Time.timeScale = ts;
        }
    }

    public void LevelCleared()
    {
        if(!isLevelCleared)
        {
            isLevelCleared = true;
            winUI.SetActive(true);
            Time.timeScale = 0;
            endScore.text = score.ToString();
            bonusTimeScore.text = Mathf.RoundToInt(timeLeft * 256.0f).ToString();
            totalScore.text = Mathf.RoundToInt(score + (timeLeft * 256.0f)).ToString();
        }        
    }

}

public enum LEVEL_STATE
{
    PLAYING,
    PAUSED,
    FINISHED,
    TOTAL_STATE,
    END
}
