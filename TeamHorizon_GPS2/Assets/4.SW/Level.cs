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

        float minutes = Mathf.Floor(timeLeft / 60);
        float seconds = Mathf.RoundToInt(timeLeft % 60);

        timeLeftUI.text = minutes.ToString() + " : " + seconds.ToString();
        ScoreUI.text = "what is the score formula";

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

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}

enum LEVEL_STATE
{
    PLAYING,
    PAUSED,
    FINISHED,
    TOTAL_STATE
}
