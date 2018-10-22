using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level: MonoBehaviour
{
    private float score;
    [SerializeField]private float givenTime;
    private float timeLeft;
    private float extraTime;
    [SerializeField] private Text timeLeftUI;
    [SerializeField] private Text ScoreUI;
    public LEVEL_STATE levelState;
    public List<GameObject> targetProfile;

	void Start () {
        timeLeft = givenTime;
        score = 0;
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

        timeLeftUI.text = minutes.ToString() + " : " + seconds.ToString();
        ScoreUI.text = score.ToString();

        if(levelState == LEVEL_STATE.PLAYING)
        {
            timeLeft -= Time.deltaTime;
        }
        else if(levelState == LEVEL_STATE.PAUSED)
        {
            SetTimeScale(0f);
        }
        else if(levelState == LEVEL_STATE.FINISHED)
        {
            //player currency +=  timeLeft* xxx;
            //show score + etc
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
}

public enum LEVEL_STATE
{
    PLAYING,
    PAUSED,
    FINISHED,
    TOTAL_STATE
}
