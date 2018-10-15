using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Control : MonoBehaviour {

    public List<GameObject> dialogue;

    ControlCenter cc;
    Level level;

    bool isPause = false;
    GameObject tempGameObject;
    GameObject tempGameObject2;
    bool hasClick = false;

    float timer;
    float DelayTime;

    // Use this for initialization
    void Start () {
        cc = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();
        level = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<Level>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isPause == true)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }

        if(cc.levelStatus == 1)
        {
            timer += Time.deltaTime;
            DelayTime = 2.7f;
            if (hasClick == false)
            {
                if(timer >= DelayTime)
                {
                    level.SetTimeScale(0f);
                    tempGameObject2 = dialogue[0].gameObject;
                    tempGameObject = dialogue[1].gameObject;
                    tempGameObject2.SetActive(false);
                    tempGameObject.SetActive(true);
                }  
            }
            else if(hasClick == true)
            {
                level.SetTimeScale(1.0f);
            }
            
        }

        // After kill one enemy
        /*if(EnemyMovement.enemyCountArea == 4)
        {
            if (hasClick == false)
            {
                level.SetTimeScale(0f);
                tempGameObject = dialogue[2].gameObject;
                tempGameObject.SetActive(true);
            }
            else
            {
                level.SetTimeScale(1.0f);
            }
        }*/
	}

    public void Pause()
    {
        isPause = true;
    }

    public void Unpause()
    {
        isPause = false;
    }

    public void HasClickTheDialogue()
    {
        if(hasClick == false)
        {
            hasClick = true;
        }
    }
}
