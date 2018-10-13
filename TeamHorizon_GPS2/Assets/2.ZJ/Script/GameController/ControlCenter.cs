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
    public bool QTESuccess;   
    public bool QTEFail;   

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
            LevelTutorialScript();
            
        }   
    }

    void LevelTutorialScript()
    {        
        if(levelStatus == 1) // Start to first battle phase
        {
            sMove.PlayerMovement(0.0f, 0);
            if (BattleCompleted) // If battle phase completed move to next target
            {
                coroutine = IncreaseLevelStatus(2, 0.0f);
                StartCoroutine(coroutine);
                BattleCompleted = false;
            }            
        }
        else if(levelStatus == 2) // 2nd target 
        {
            sMove.PlayerMovement(0.0f, 1);
            coroutine = IncreaseLevelStatus(3, 1.0f);
            StartCoroutine(coroutine);
        }
        else if(levelStatus == 3) // 3rd target, camera pan to falling tree
        {
            sMove.PlayerMovement(0.0f, 2);
            coroutine = CameraPanPhase(0.8f); 
            StartCoroutine(coroutine);
            coroutine = IncreaseLevelStatus(4, 8.0f);
            StartCoroutine(coroutine);
        }
        else if(levelStatus == 4) // 4th target, qte phase
        {
            sMove.PlayerMovement(0.0f, 3);
            if (QTESuccess)
            {
                StopAllCoroutines();
                coroutine = IncreaseLevelStatus(5, 0.0f);
                StartCoroutine(coroutine);
                QTESuccess = false;
            }
            else if (QTEFail)
            {                
                player.transform.position = player.transform.position + new Vector3 (-5.0f,0,0);
                QTEFail = false;
               
            }
        }
        else if(levelStatus == 5)
        {
            sMove.PlayerMovement(0.0f, 4);
            coroutine = IncreaseLevelStatus(6, 1.0f);
            StartCoroutine(coroutine);
        }
        else if(levelStatus == 6)
        {
            sMove.PlayerMovement(0.5f, 5);
            if (BattleCompleted) // If battle phase completed move to next target
            {
                coroutine = IncreaseLevelStatus(7, 0.5f);
                StartCoroutine(coroutine);
                BattleCompleted = false;
            }
        }
        else if(levelStatus == 7)
        {
            sMove.PlayerMovement(0.5f, 6);
            coroutine = CameraPanPhase(2.0f);
            StartCoroutine(coroutine);            
            coroutine = IncreaseLevelStatus(8, 10.0f);
            StartCoroutine(coroutine);
        }
        else if(levelStatus == 8)
        {
            StopAllCoroutines();
            sMove.PlayerMovement(0.0f, 7);
            if (BattleCompleted) // If battle phase completed move to next target
            {
                StopAllCoroutines();
                coroutine = IncreaseLevelStatus(9, 0.5f);
                StartCoroutine(coroutine);
                BattleCompleted = false;
            }
        }
        else if(levelStatus == 9)
        {
            sMove.PlayerMovement(0.0f, 8);
            coroutine = CameraPanPhase(6.0f);
            StartCoroutine(coroutine);
            coroutine = IncreaseLevelStatus(10, 8.0f);
            StartCoroutine(coroutine);
        }
        else if(levelStatus == 10)
        {
            sMove.PlayerMovement(1.0f, 9);
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
        if (player.GetComponent<ScriptedMovement>().goal.GetComponent<TargetProfile>().Hazard != null)
        {
            GameObject.FindGameObjectWithTag("2ndCamera").GetComponent<PanToTarget>().CameraPan();
            player.GetComponent<ScriptedMovement>().goal.GetComponent<TargetProfile>().Hazard.GetComponent<TreeFallHazard>().TreeFalling();            
        }
    }
}
