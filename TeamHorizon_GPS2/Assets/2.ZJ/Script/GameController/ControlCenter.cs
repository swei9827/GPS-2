using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCenter : MonoBehaviour {
    
    public GameObject player;
    public bool LevelTutorial;
    public bool Level1;
    public bool Level2;
    public bool Level3;

    public bool BattleCompleted;

    public int levelStatus = 0;

    IEnumerator coroutine;

    ScriptedMovement sMove;

    void Start()
    {
        sMove = player.GetComponent<ScriptedMovement>();
    }

    void Update()
    {
        if (LevelTutorial)
        {
            //LevelTutorialScript();
        }   
    }

    void LevelTutorialScript()
    {        
        if(levelStatus == 0) // Start to first battle phase
        {
            sMove.PlayerMovement(1.0f, 0);
            if (BattleCompleted) // If battle phase completed move to next target
            {
                coroutine = IncreaseLevelStatus(1, 0.5f);
                StartCoroutine(coroutine);
            }            
        }
        else if(levelStatus == 1) // 2nd target 
        {         
            sMove.transitionMovement(1);
            coroutine = IncreaseLevelStatus(2, 3.0f);
            StartCoroutine(coroutine);
        }
        else if(levelStatus == 2) // 3rd target, camera pan to falling tree
        {
            sMove.PlayerMovement(0.0f, 2);
            coroutine = CameraPanPhase(1.0f); 
            StartCoroutine(coroutine);
            coroutine = IncreaseLevelStatus(3, 7.0f);
            StartCoroutine(coroutine);
        }
        else if(levelStatus == 3) // 4th target, qte phase
        {
            sMove.PlayerMovement(0.0f, 3);
        }

    } 

    private IEnumerator IncreaseLevelStatus(int num, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        levelStatus = num;
    }

    private IEnumerator CameraPanPhase(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject.FindGameObjectWithTag("Hazard").GetComponent<TreeFallHazard>().TreeFalling();
        GameObject.FindGameObjectWithTag("2ndCamera").GetComponent<PanToTarget>().CameraPan();
    }
}
