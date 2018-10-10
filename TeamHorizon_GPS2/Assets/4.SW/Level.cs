using System;
using UnityEngine;
using UnityEngine.UI;

public class Level: MonoBehaviour
{
    private float score;
    [SerializeField]private float givenTime;
    private float timeLeft;
    private float extraTime;
    [SerializeField] private Text timeLeftUI;
    private LEVEL_STATE levelState;

	void Start () {
        timeLeft = givenTime;		
	}
	
    void setScore(float s)
    {
        score = s;
    }
    
    public float getScore()
    {
        return score;
    }

	void Update () {

        timeLeftUI.text = (Math.Truncate((int)timeLeft * 100.0) / 100.0).ToString();

        if(levelState == LEVEL_STATE.PLAYING)
        {
            timeLeft -= Time.deltaTime;
        }
        else if(levelState == LEVEL_STATE.PAUSED)
        {
            // show paused menu
        }
        else if(levelState == LEVEL_STATE.FINISHED)
        {
            //player currency +=  timeLeft* xxx;
            //show score + etc
        }
        

	}
}

enum LEVEL_STATE
{
    PLAYING,
    PAUSED,
    FINISHED,
    TOTAL_STATE
}
