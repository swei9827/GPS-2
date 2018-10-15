using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCenter : MonoBehaviour {
    
    public GameObject player;
    public Camera camera;
   
    public bool LevelTutorial;
    public bool Level1;
    public bool Level2;
    public bool Level3;

    public bool OnBattle;
    public bool setEnemyCount;
    public bool BattleCompleted;
    public bool OnQTE;
    public bool QTESuccess;   
    public bool QTEFail;
    public bool OnIO;
    public bool InteractSuccess;
    public bool InteractFail;
    public float HidingSpotSwapTime;
    public float levelStatus = 0;
    public int enemyCount;

    public List<Transform> locations = new List<Transform>();
    public List<Transform> battleArea = new List<Transform>();
    public List<Transform> cameraFocus = new List<Transform>();
    public List<GameObject> hazards = new List<GameObject>();

    //reference
    private ScreenWobble screenWobble;
    EnemySpawn es;
    IEnumerator coroutine;
    ScriptedMovement sMove;
    bool Moved = false;
    float timeLeft = 6.0f;

    int pos = 1;

    void Start()
    {
        sMove = player.GetComponent<ScriptedMovement>();
        screenWobble = GameObject.FindGameObjectWithTag("2ndCamera").GetComponent<ScreenWobble>();
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
            if (!setEnemyCount)
            {
                enemyCount = locations[0].GetComponent<TargetProfile>().EnemyCount;
                setEnemyCount = true;
            }
            if (!OnBattle)
            {                
                sMove.PlayerMove(0.0f, locations[0]);
                sMove.PlayerRotate(0.0f, locations[1]);
                locations[0].GetComponent<TargetProfile>().EnterBattlePhase(1);
            }
            else if (OnBattle)
            {                
                BattlePhase(0,1,0);
                screenWobble.isMoving = false;
            }
            if (enemyCount== 0)
            {
                OnBattle = false;
            }      
            if (BattleCompleted) // If battle phase completed move to next target
            {
                coroutine = IncreaseLevelStatus(2, 0.0f);
                StartCoroutine(coroutine);        
            }            
        }
        else if(levelStatus == 2) // 2nd target 
        {
            BattleCompleted = false;
            OnBattle = false;
            setEnemyCount = false;
            sMove.PlayerMove(0.0f, locations[1]);
            sMove.PlayerRotate(0.5f, locations[2]);
            coroutine = IncreaseLevelStatus(3, 1.0f);
            StartCoroutine(coroutine);
        }
        else if(levelStatus == 3) // 3rd target, camera pan to falling tree
        {            
            sMove.PlayerMove(0.0f, locations[2]);
            coroutine = CameraPanPhase(0.8f,0); 
            StartCoroutine(coroutine);
            screenWobble.isMoving = false;
            coroutine = IncreaseLevelStatus(4, 7.5f);
            StartCoroutine(coroutine);
        }
        else if(levelStatus == 4) // 4th target, qte phase
        {
            sMove.PlayerMove(0.0f, locations[3]);
            screenWobble.isMoving = true;
            if (player.transform.position == locations[3].GetComponent<Transform>().position)
            {
                timeLeft -= Time.deltaTime;
                OnQTE = true;
                screenWobble.isMoving = false;
                if(timeLeft <= 0)
                {
                    QTEFail = true;
                }
                if (QTESuccess)
                {
                    StopAllCoroutines();
                    levelStatus = 5;
                }
                else if (QTEFail)
                {
                    StopAllCoroutines();
                    levelStatus = 5.5f;
                }
            }                
        }
        else if(levelStatus == 5.5)
        {
            sMove.PlayerMove(0.0f, locations[5]);         
            coroutine = IncreaseLevelStatus(6.5f, 2.0f);
            StartCoroutine(coroutine);            
        }
        else if(levelStatus == 5)
        {
            sMove.PlayerMove(0.0f, locations[4]);
            sMove.PlayerRotate(0.8f, locations[6]);
            sMove.PlayerMove(0.8f, locations[6]);
            coroutine = IncreaseLevelStatus(6, 1.3f);
            StartCoroutine(coroutine);
        }
        else if(levelStatus == 6.5)
        {
            sMove.PlayerMove(0.0f, locations[4]);
            sMove.PlayerRotate(0.8f, locations[6]);
            sMove.PlayerMove(0.8f, locations[6]);
            coroutine = IncreaseLevelStatus(6, 2f);
            StartCoroutine(coroutine);
        }
        else if(levelStatus == 6)
        {
            OnQTE = false;
            QTESuccess = false;
            QTEFail = false;
            if (!setEnemyCount)
            {
                enemyCount = locations[6].GetComponent<TargetProfile>().EnemyCount;
                setEnemyCount = true;
            }
            
            if (!OnBattle)
            {                                
                screenWobble.isMoving = true;
                sMove.PlayerMove(0.0f, locations[6]);
                sMove.PlayerRotate(0.0f, locations[7]);
                locations[6].GetComponent<TargetProfile>().EnterBattlePhase(2);                
            }
            else if (OnBattle)
            {
                BattlePhase(2, 3, 1);
                screenWobble.isMoving = false;
            }
            if (enemyCount == 0)
            {
                OnBattle = false;
            }
            if (BattleCompleted) // If battle phase completed move to next target
            {                  
                coroutine = IncreaseLevelStatus(7, 0.5f);
                StartCoroutine(coroutine);
            }            
        }
        else if(levelStatus == 7)
        {
            BattleCompleted = false;
            OnBattle = false;
            setEnemyCount = false;
            sMove.PlayerMove(0.0f, locations[7]);
            screenWobble.isMoving = false;
            coroutine = CameraPanPhase(2.0f, 1);
            StartCoroutine(coroutine);            
            coroutine = IncreaseLevelStatus(8, 9.0f);
            StartCoroutine(coroutine);
        }
        else if(levelStatus == 8)
        {
            if (!OnBattle)
            {
                screenWobble.isMoving = true;
                sMove.PlayerMove(0.0f, locations[8]);
                if(player.transform.position == locations[8].position)
                {
                    enemyCount = 1;
                    OnBattle = true;                    
                }
            }
            else if (OnBattle)
            {
                screenWobble.isMoving = false;
                if(enemyCount <= 0)
                {
                    BattleCompleted = true;
                }
            }
            
            if (BattleCompleted) // If battle phase completed move to next target
            {
                StopAllCoroutines();
                levelStatus = 9;
                BattleCompleted = false;
            }
        }
        else if(levelStatus == 9)
        {
            OnBattle = false;
            sMove.PlayerMove(0.0f, locations[9]);
            //coroutine = CameraPanPhase(5f,2);
           // StartCoroutine(coroutine);
            coroutine = IncreaseLevelStatus(10, 6f);
            StartCoroutine(coroutine);
        }
        else if(levelStatus == 10)
        {
            sMove.PlayerMove(0.0f, locations[10]);            
            coroutine = IncreaseLevelStatus(11, 2.5f);
            StartCoroutine(coroutine);
        }
        else if(levelStatus == 11)
        {
            sMove.PlayerRotate(0.0f, locations[13]);
            sMove.PlayerMove(1.0f, locations[13]);
            if(player.transform.position == locations[13].position)
            {
                OnIO = true;
                timeLeft -= Time.deltaTime;
                screenWobble.isMoving = false;
                if (timeLeft <= 0)
                {
                    QTEFail = true;
                }
                if (QTESuccess)
                {
                   coroutine = IncreaseLevelStatus(12, 0.0f);
                   StartCoroutine(coroutine);
                }
                else if (QTEFail)
                {
                   coroutine = IncreaseLevelStatus(14, 0.0f);
                   StartCoroutine(coroutine);
                }
            }
            if (InteractSuccess)
            {
                
            }
            else if (InteractFail)
            {
                
            }
        }
        else if(levelStatus == 14)
        {
            sMove.PlayerRotate(0.0f, locations[11]);
            sMove.PlayerMove(2.0f, locations[11]);
            coroutine = IncreaseLevelStatus(15, 4.0f);
            StartCoroutine(coroutine);
        }
        else if(levelStatus == 15)
        {
            StopAllCoroutines();
            if (BattleCompleted)
            {
                levelStatus = 16;
                BattleCompleted = false;
            }
        }
        else if(levelStatus == 16)
        {
            sMove.PlayerMove(0.0f, locations[12]);
        }
        else if(levelStatus == 12)
        {
            if (!OnBattle)
            {
                sMove.PlayerMove(0.0f, locations[14]);
                sMove.PlayerRotate(1.0f, locations[15]);
                locations[14].GetComponent<TargetProfile>().EnterBattlePhase(4);
            }
            else if (OnBattle)
            {
                BattlePhase(4, 5, 3);
            }
            if(enemyCount <= 0)
            {
                OnBattle = false;
            }
            if (BattleCompleted) // If battle phase completed move to next target
            {
                StopAllCoroutines();
                levelStatus = 13;
                BattleCompleted = false;
                OnBattle = false;
            }
        }
        else if(levelStatus == 13)
        {
            sMove.PlayerMove(0.0f, locations[15]);
        }       
    }

    void BattlePhase(int ID1, int ID2, int focusID)
    {
        sMove.PlayerRotate(0.0f, cameraFocus[focusID]);
        coroutine = MoveBetweenHS(ID1, ID2);
        StartCoroutine(coroutine);
    }

    private IEnumerator IncreaseLevelStatus(float num, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        levelStatus = num;
    }

    private IEnumerator CameraPanPhase(float waitTime, int hazardID)
    {
        yield return new WaitForSeconds(waitTime);
        hazards[hazardID].GetComponent<TreeFallHazard>().TreeFalling();
        camera.GetComponent<PanToTarget>().CameraPan(hazardID);             
    }

    private IEnumerator DelayStop(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StopAllCoroutines();
    }

    private IEnumerator MoveBetweenHS(int ID1, int ID2)
    {       
        if(pos == 1)
        {
            sMove.PlayerMove(0.0f, battleArea[ID1]);
            yield return new WaitForSeconds(HidingSpotSwapTime);
            pos = 2;
        }
        else if(pos == 2)
        {
            sMove.PlayerMove(0.0f, battleArea[ID2]);
            yield return new WaitForSeconds(HidingSpotSwapTime);
            pos = 1;
        }               
    }
}
