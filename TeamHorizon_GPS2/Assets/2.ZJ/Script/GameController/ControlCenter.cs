using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCenter : MonoBehaviour
{
    public GameObject player;
    public Camera camera;

    public bool LevelTutorial;
    public bool Level1;
    public bool Level2;
    public bool Level3;
    public STATUS status;

    public bool QTESuccess;
    public bool QTEFail;
    public bool InteractSuccess;
    public bool InteractFail;
    public int levelStatus = 0;

    public List<Transform> locations = new List<Transform>();
    public List<Transform> battleArea = new List<Transform>();
    public List<GameObject> hazards = new List<GameObject>();

    //reference
    private ScreenWobble screenWobble;
    IEnumerator coroutine;
    ScriptedMovement sMove; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sMove = player.GetComponent<ScriptedMovement>();
        screenWobble = GameObject.FindGameObjectWithTag("2ndCamera").GetComponent<ScreenWobble>();
        status = STATUS.MOVING;
    }

    void Update()
    {
        if (LevelTutorial)
        {
            Tutorial();
            Debug.Log(player.transform.eulerAngles.y);
        }
    }

    void Tutorial()
    {
        // B1
        if (levelStatus == 1)
        {
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[0]);
                if (player.transform.position == locations[0].position)
                {
                    status = STATUS.BATTLE;
                }
            }
            if (status == STATUS.BATTLE)
            {
                if (!battleArea[0].GetComponentInParent<HSProfile>().destroyed)
                {
                    sMove.PlayerMove(battleArea[0]);
                }
                else if (battleArea[0].GetComponentInParent<HSProfile>().destroyed)
                {
                    sMove.PlayerMove(battleArea[1]);
                }
            }            
            if (locations[0].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                status = STATUS.IDLE;
            }
            if (status == STATUS.IDLE)
            {
                sMove.PlayerMove(locations[0]);
                if (player.transform.position == locations[0].position)
                {
                    status = STATUS.MOVING;
                    levelStatus = 2;
                }
            }
        }
        // First Corner
        else if (levelStatus == 2)
        {
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[1]);
                if (player.transform.position == locations[1].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if (status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[2]);
                if(player.transform.eulerAngles.y > 269 && player.transform.eulerAngles.y < 270.05f)
                {
                    levelStatus = 3;
                    status = STATUS.MOVING;
                }
            }            
        }
        // Falling tree
        else if(levelStatus == 3)
        {
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[2]);
                if (player.transform.position == locations[2].position)
                {
                    status = STATUS.CAMERA;
                }
            }
            if (status == STATUS.CAMERA)
            {
                hazards[0].GetComponent<TreeFallHazard>().TreeFalling();
                camera.GetComponent<PanToTarget>().CameraPan(0);
                if (camera.GetComponent<PanToTarget>().panComplete)
                {
                    status = STATUS.MOVING;
                    levelStatus = 4;
                }
            }
        }
        // QTE
        else if(levelStatus == 4)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[3]);
                if (player.transform.position == locations[3].position)
                {
                    status = STATUS.QTE;                    
                }
            }
            if (QTESuccess)
            {
                levelStatus = 5;
                status = STATUS.MOVING;                        
            }
            else if (QTEFail)
            {
                levelStatus = 6;
                status = STATUS.MOVING;                       
            }
        }
        // QTE Success
        else if(levelStatus == 5)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[4]);
                if(player.transform.position == locations[4].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if(status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[6]);
                if (player.transform.eulerAngles.y > 359 && player.transform.eulerAngles.y < 360.05f ||
                    player.transform.eulerAngles.y > 0 && player.transform.eulerAngles.y < 0.8f)
                {
                    levelStatus = 7;
                    status = STATUS.MOVING;
                }
            }
        }
        // QTE Fail
        else if(levelStatus == 6)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[5]);
                if (player.transform.position == locations[5].position)
                {
                    status = STATUS.IDLE;
                }
            }
            if(status == STATUS.IDLE)
            {
                sMove.PlayerMove(locations[4]);
                if(player.transform.position == locations[4].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if(status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[6]);
                if (player.transform.eulerAngles.y > 0 && player.transform.eulerAngles.y < 0.8f)
                {
                    levelStatus = 7;
                    status = STATUS.MOVING;
                }
            }
        }
        // B2
        else if(levelStatus == 7)
        {           
            if(status == STATUS.MOVING)
            {
                QTEFail = false;
                QTESuccess = false;
                sMove.PlayerMove(locations[6]);
                if(player.transform.position == locations[6].position)
                {
                    status = STATUS.BATTLE;
                }
            }
            if (status == STATUS.BATTLE)
            {
                if (!battleArea[2].GetComponentInParent<HSProfile>().destroyed)
                {
                    sMove.PlayerMove(battleArea[2]);
                }
                else if (battleArea[2].GetComponentInParent<HSProfile>().destroyed)
                {
                    sMove.PlayerMove(battleArea[3]);
                }
            }
            if (locations[6].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                status = STATUS.IDLE;
            }
            if (status == STATUS.IDLE)
            {
                sMove.PlayerMove(locations[6]);
                if (player.transform.position == locations[6].position)
                {
                    status = STATUS.MOVING;
                    levelStatus = 8;
                }
            }
        }
        // Camera 2
        else if(levelStatus == 8)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[7]);
                if(player.transform.position == locations[7].position)
                {
                    status = STATUS.CAMERA;
                }
            }
            if(status == STATUS.CAMERA)
            {
                hazards[1].GetComponent<TreeFallHazard>().TreeFalling();
                camera.GetComponent<PanToTarget>().CameraPan(1);
                if (camera.GetComponent<PanToTarget>().panComplete)
                {
                    status = STATUS.MOVING;
                    levelStatus = 9;
                }
            }
        }
        // B3
        else if(levelStatus == 9)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[8]);
                if(player.transform.position == locations[8].position)
                {
                    status = STATUS.BATTLE;
                }
            }            
            if (locations[8].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                status = STATUS.IDLE;
            }
            if (status == STATUS.IDLE)
            {
                sMove.PlayerMove(locations[9]);     
                if(player.transform.position == locations[9].position)
                {
                    status = STATUS.INTERACTABLE;
                    levelStatus = 10;
                }
            }
        }
        // Interactable Object
        else if(levelStatus == 10)
        {
            if(status == STATUS.INTERACTABLE)
            {
                if (InteractSuccess)
                {
                    status = STATUS.TURNING;
                    levelStatus = 11;
                }
                else if (InteractFail)
                {
                    status = STATUS.TURNING;
                    levelStatus = 13;
                }
            }
        }
        // B4
        else if(levelStatus == 11)
        {
            if(status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[10]);
                if(player.transform.eulerAngles.y >= 89 && player.transform.eulerAngles.y <= 90.05f)
                {
                    status = STATUS.MOVING;
                }
            }
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[10]);
                if(player.transform.position == locations[10].position)
                {
                    status = STATUS.BATTLE;
                }
            }
            if (status == STATUS.BATTLE)
            {
                if (!battleArea[4].GetComponentInParent<HSProfile>().destroyed)
                {
                    sMove.PlayerMove(battleArea[4]);
                }
                else if (battleArea[4].GetComponentInParent<HSProfile>().destroyed)
                {
                    sMove.PlayerMove(battleArea[5]);
                }
            }
            if (locations[10].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                status = STATUS.IDLE;
            }
            if (status == STATUS.IDLE)
            {
                sMove.PlayerMove(locations[10]);
                if (player.transform.position == locations[10].position)
                {
                    status = STATUS.MOVING;
                    levelStatus = 12;
                }
            }
        }
        // Right Portal
        else if(levelStatus == 12)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[11]);
                if(player.transform.position == locations[11].position)
                {
                    status = STATUS.PORTAL;
                }
            }
        }
        // B5
        else if(levelStatus == 13)
        {
            if (status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[12]);
                if (player.transform.eulerAngles.y >= 269 && player.transform.eulerAngles.y <= 270.05f)
                {
                    status = STATUS.MOVING;
                }
            }
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[12]);
                if (player.transform.position == locations[12].position)
                {
                    status = STATUS.BATTLE;
                }
            }            
            if (locations[12].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                status = STATUS.IDLE;
            }
            if (status == STATUS.IDLE)
            {
                sMove.PlayerMove(locations[13]);
                if (player.transform.position == locations[13].position)
                {
                    status = STATUS.PORTAL;
                }
            }
        }

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
}

public enum STATUS
    {
        IDLE,
        MOVING,
        TURNING,
        CAMERA,
        BATTLE,
        QTE,
        INTERACTABLE,
        PORTAL,
        TOTAL_STATE
    }