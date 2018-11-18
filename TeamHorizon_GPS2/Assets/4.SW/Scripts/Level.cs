﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level: MonoBehaviour
{
    private float score;
    public float currency;
    [SerializeField]private float givenTime;
    private float timeLeft;
    private float extraTime;
    [SerializeField] private Text timeLeftUI;
    [SerializeField] private Text ScoreUI;
    public LEVEL_STATE levelState;
    public List<GameObject> targetProfile;
    public bool isContinue;
    public GameObject levelEndUI; // UI while level finished
    public Text endScore;
    public Text bonusTimeScore;
    public Text totalScore;
    public GameObject loseUI;

	void Start () {
        timeLeft = givenTime;
        score = 0;
        isContinue = false;
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

        float minutes = Mathf.Floor(timeLeft / 60);
        float seconds = Mathf.RoundToInt(timeLeft % 60);

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
            }           
        }/*
        else if(levelState == LEVEL_STATE.PAUSED)
        {
            SetTimeScale(0f);
        }*/
        if(timeLeft <= 0)
        {
            timeLeft = 0;
        }
	}

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SetTimeScale(float ts)
    {
        Time.timeScale = ts;
    }

    public void LevelCleared()
    {
        levelEndUI.SetActive(true);
        endScore.text = score.ToString();
        bonusTimeScore.text = (timeLeft * 256.0f).ToString();
        totalScore.text = score + (timeLeft * 256.0f).ToString();
    }
}

public enum LEVEL_STATE
{
    PLAYING,
    PAUSED,
    FINISHED,
    TOTAL_STATE
}
