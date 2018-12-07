using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCenter : MonoBehaviour
{
    public GameObject player;
    public Camera camera;
    public GameObject winUI;

    public bool LevelTutorial;
    public bool Level1;
    public bool Level2;
    public bool Level3;
    public STATUS status;

    public bool GoLeft;
    public bool GoRight;
    public bool QTESuccess;
    public bool QTEFail;
    public bool InteractSuccess;
    public bool InteractFail;
    public bool BADestroyed = false;
    public int levelStatus = 0;

    public float[] cameraRotatePos;
    public List<Transform> locations = new List<Transform>();
    public List<Transform> battleArea = new List<Transform>();
    public List<GameObject> hazards = new List<GameObject>();
    public List<GameObject> qtes = new List<GameObject>();

    //reference
    private ScreenWobble screenWobble;
    IEnumerator coroutine;
    ScriptedMovement sMove;
    

    void Start()
    {
        sMove = player.GetComponent<ScriptedMovement>();
        screenWobble = GameObject.FindGameObjectWithTag("2ndCamera").GetComponent<ScreenWobble>();
        status = STATUS.MOVING;
    }

    void Update()
    {
        if (LevelTutorial)
        {
            Tutorial();
        }
        if (Level1)
        {
            LevelOne();
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
                if (!battleArea[0].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[0], Time.time * 0.5f), 0);                 
                    sMove.PlayerMove(battleArea[0]);
                    if(player.transform.position == battleArea[0].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
                else if (battleArea[0].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[1], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[1]);
                    if (player.transform.position == battleArea[1].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
            } 

            if(status == STATUS.CROUCH)
            {               
                if (battleArea[0].GetComponent<HSProfile>().destroyed && !BADestroyed)
                {
                    status = STATUS.BATTLE;
                    BADestroyed = true;
                }
            }

            if (locations[0].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                BADestroyed = false;
                camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, 0, Time.time * 0.2f), 0);
                if(camera.transform.rotation.y == 0)
                {
                    status = STATUS.IDLE;
                }              
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
            if(status == STATUS.QTE)
            {
                qtes[0].SetActive(true);
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
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[2], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[2]);
                    if (player.transform.position == battleArea[2].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
                else if (battleArea[2].GetComponentInParent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[3], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[3]);
                    if (player.transform.position == battleArea[2].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
            }

            if (status == STATUS.CROUCH)
            {
                if (battleArea[2].GetComponent<HSProfile>().destroyed && !BADestroyed)
                {
                    status = STATUS.BATTLE;
                    BADestroyed = true;
                }
            }

            if (locations[6].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, 0, Time.time * 0.2f), 0);
                if (camera.transform.rotation.y == 0)
                {
                    status = STATUS.IDLE;
                }
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
                    status = STATUS.TURNING;
                    levelStatus = 10;
                }
            }
        }
        // Interactable Object
        else if(levelStatus == 10)
        {
            if(status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[10]);
                if((player.transform.eulerAngles.y > 89 && player.transform.eulerAngles.y < 90.05f))
                {
                    status = STATUS.INTERACTABLE;
                }
            }

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
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[4], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[4]);
                    if (player.transform.position == battleArea[4].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
                else if (battleArea[4].GetComponentInParent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[5], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[5]);
                    if (player.transform.position == battleArea[4].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
            }

            if (status == STATUS.CROUCH)
            {
                if (battleArea[2].GetComponent<HSProfile>().destroyed && !BADestroyed)
                {
                    status = STATUS.BATTLE;
                    BADestroyed = true;
                }
            }

            if (locations[10].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, 90, Time.time * 0.2f), 0);
                if (camera.transform.rotation.y > 0 && camera.transform.rotation.y < 1)
                {
                    status = STATUS.IDLE;
                }
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
                    winUI.SetActive(true);
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
                    Level level = GetComponent<Level>();
                    level.LevelCleared();
                }
            }           
        }
    }

    void LevelOne()
    {
        // Moving to B1
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
                if (!battleArea[0].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[0], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[0]);
                    if (player.transform.position == battleArea[0].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
                else if (battleArea[0].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[1], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[1]);
                    if (player.transform.position == battleArea[1].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
            }

            if (status == STATUS.CROUCH)
            {
                if (battleArea[0].GetComponent<HSProfile>().destroyed && !BADestroyed)
                {
                    status = STATUS.BATTLE;
                    BADestroyed = true;
                }
            }
            if (locations[0].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                BADestroyed = false;
                camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, 0, Time.time * 0.2f), 0);
                if (camera.transform.rotation.y == 0)
                {
                    status = STATUS.IDLE;
                }
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
        // Moving from B1 to corner
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
                if (player.transform.eulerAngles.y > 89 && player.transform.eulerAngles.y < 90.05f)
                {
                    levelStatus = 3;
                    status = STATUS.MOVING;
                }
            }
        }
        // Moving to next corner
        else if (levelStatus == 3)
        {
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[2]);
                if (player.transform.position == locations[2].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if (status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[3]);
                if (player.transform.eulerAngles.y > 179 && player.transform.eulerAngles.y < 180.05f)
                {
                    levelStatus = 4;
                    status = STATUS.MOVING;
                }
            }
        }
        // Moving to B2
        else if (levelStatus == 4)
        {
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[3]);
                if (player.transform.position == locations[3].position)
                {
                    status = STATUS.BATTLE;
                }
            }
            if (status == STATUS.BATTLE)
            {
                if (!battleArea[2].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[2], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[2]);
                    if (player.transform.position == battleArea[2].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
                else if (battleArea[2].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[3], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[3]);
                    if (player.transform.position == battleArea[3].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
            }
            if (status == STATUS.CROUCH)
            {
                if (battleArea[2].GetComponent<HSProfile>().destroyed && !BADestroyed)
                {
                    status = STATUS.BATTLE;
                    BADestroyed = true;
                }
            }
            if (locations[3].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                BADestroyed = false;
                camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, 180, Time.time * 0.2f), 0);
                if (camera.transform.rotation.y <= 180)
                {
                    status = STATUS.IDLE;
                }
            }
            if (status == STATUS.IDLE)
            {
                sMove.PlayerMove(locations[3]);
                if (player.transform.position == locations[3].position)
                {
                    status = STATUS.MOVING;
                    levelStatus = 5;
                }
            }
        }
        // Moving to T-Junction 
        else if (levelStatus == 5)
        {
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[4]);
                if (player.transform.position == locations[4].position)
                {
                    status = STATUS.INTERACTABLE;
                }
            }
            if (status == STATUS.INTERACTABLE)
            {
                // Choosen left path
                if (GoLeft)
                {
                    levelStatus = 7;
                    status = STATUS.MOVING;
                }
                // Choosen Right Path
                else if (GoRight)
                {
                    levelStatus = 14;
                    status = STATUS.MOVING;
                }
            }
        }
        // Left Path_Begin
        else if (levelStatus == 7)
        {
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[5]);
                if (player.transform.position == locations[5].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if (status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[12]);
                if (player.transform.eulerAngles.y > 89 && player.transform.eulerAngles.y < 91)
                {
                    status = STATUS.MOVING;
                    levelStatus = 8;
                }
            }
        }
        // Left Path_B3
        else if (levelStatus == 8)
        {
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[12]);
                if (player.transform.position == locations[12].position)
                {
                    status = STATUS.BATTLE;
                }
            }
            if (status == STATUS.BATTLE)
            {
                if (!battleArea[4].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[4], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[4]);
                    if (player.transform.position == battleArea[4].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
                else if (battleArea[4].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[5], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[5]);
                    if (player.transform.position == battleArea[5].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
            }
            if (status == STATUS.CROUCH)
            {
                if (battleArea[4].GetComponent<HSProfile>().destroyed && !BADestroyed)
                {
                    status = STATUS.BATTLE;
                    BADestroyed = true;
                }
            }
            if (locations[12].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                BADestroyed = false;
                camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, 90, Time.time * 0.2f), 0);
                if (camera.transform.rotation.y <= 180)
                {
                    status = STATUS.IDLE;
                }
            }
            if (status == STATUS.IDLE)
            {
                sMove.PlayerMove(locations[12]);
                if (player.transform.position == locations[12].position)
                {
                    status = STATUS.MOVING;
                    levelStatus = 9;
                }
            }
        }
        // Left Path_Qte
        else if(levelStatus == 9)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[13]);
                if(player.transform.position == locations[13].position)
                {
                    status = STATUS.QTE;
                }
            }
            if(status == STATUS.QTE)
            {
                if (QTESuccess)
                {
                    levelStatus = 10;
                    status = STATUS.MOVING;
                }
                else if (QTEFail)
                {
                    levelStatus = 11;
                    status = STATUS.MOVING;
                }
            }
        }
        // Left_Path_QteSuccess
        else if(levelStatus == 10)
        {
            QTESuccess = false;
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[14]);
                if(player.transform.position == locations[14].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if(status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[15]);
                if (player.transform.eulerAngles.y > 179 && player.transform.eulerAngles.y < 181)
                {
                    levelStatus = 12;
                    status = STATUS.MOVING;
                }
            }
        }
        // Left_Path_QteFail
        else if(levelStatus == 11)
        {
            QTEFail = false;
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[14]);
                if (player.transform.position == locations[14].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if (status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[15]);
                if (player.transform.eulerAngles.y > 179 && player.transform.eulerAngles.y < 181)
                {
                    levelStatus = 12;
                    status = STATUS.MOVING;
                }
            }
        }
        // Left_Path_B4
        else if(levelStatus == 12)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[15]);
                if(player.transform.position == locations[15].position)
                {
                    status = STATUS.BATTLE;
                }
            }
            if(status == STATUS.BATTLE)
            {
                if (!battleArea[6].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[6], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[6]);
                    if (player.transform.position == battleArea[6].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
                else if (battleArea[6].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[7], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[7]);
                    if (player.transform.position == battleArea[7].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }                
            }
            if (status == STATUS.CROUCH)
            {
                if (battleArea[6].GetComponent<HSProfile>().destroyed && !BADestroyed)
                {
                    BADestroyed = true;
                    status = STATUS.BATTLE;
                }
            }
            if (locations[15].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                BADestroyed = false;
                camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, 180, Time.time * 0.2f), 0);
                if (camera.transform.rotation.y <= 180)
                {
                    status = STATUS.IDLE;
                }
            }
            if (status == STATUS.IDLE)
            {
                sMove.PlayerMove(locations[15]);
                if (player.transform.position == locations[15].position)
                {
                    status = STATUS.MOVING;
                    levelStatus = 13;
                }
            }
        }
        // Left_Path_T_Junction
        else if(levelStatus == 13)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[16]);
                if(player.transform.position == locations[16].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if(status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[17]);
                if (player.transform.eulerAngles.y > 89 && player.transform.eulerAngles.y < 91)
                {
                    levelStatus = 22;
                    status = STATUS.MOVING;
                }
            }
        }
        // Right_Path_Corner
        else if(levelStatus == 14)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[5]);
                if(player.transform.position == locations[5].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if(status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[6]);
                if (player.transform.eulerAngles.y > 269 && player.transform.eulerAngles.y < 271)
                {
                    levelStatus = 15;
                    status = STATUS.MOVING;
                }
            }
        }
        // Right_Path_Moving to corner
        else if(levelStatus == 15)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[6]);
                if (player.transform.position == locations[6].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if(status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[7]);
                if (player.transform.eulerAngles.y > 179 && player.transform.eulerAngles.y < 181)
                {
                    levelStatus = 16;
                    status = STATUS.MOVING;
                }
            }
        }
        // Right_Path_B5
        else if(levelStatus == 16)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[7]);
                if(player.transform.position == locations[7].position)
                {
                    status = STATUS.BATTLE;
                }
            }
            if (status == STATUS.BATTLE)
            {
                if (!battleArea[8].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[8], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[8]);
                    if (player.transform.position == battleArea[8].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
                else if (battleArea[8].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[9], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[9]);
                    if (player.transform.position == battleArea[9].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
            }
            if (status == STATUS.CROUCH)
            {
                if (battleArea[8].GetComponent<HSProfile>().destroyed && !BADestroyed)
                {
                    BADestroyed = true;
                    status = STATUS.BATTLE;
                }
            }
            if (locations[7].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                BADestroyed = false;
                camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, 180, Time.time * 0.2f), 0);
                if (camera.transform.rotation.y <= 180)
                {
                    status = STATUS.IDLE;
                }
            }
            if (status == STATUS.IDLE)
            {
                sMove.PlayerMove(locations[7]);
                if (player.transform.position == locations[7].position)
                {
                    status = STATUS.MOVING;
                    levelStatus = 17;
                }
            }
        }
        // Right_Path_QTE
        else if(levelStatus == 17)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[8]);
                if(player.transform.position == locations[8].position)
                {
                    status = STATUS.QTE;
                }
            }
            if(status == STATUS.QTE)
            {
                if (QTESuccess)
                {
                    levelStatus = 18;
                    status = STATUS.MOVING;
                }
                else if (QTEFail)
                {
                    levelStatus = 19;
                    status = STATUS.MOVING;
                }
            }
        }
        // Right_Path_QteSuccess
        else if(levelStatus == 18)
        {
            QTESuccess = false;
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[9]);
                if(player.transform.position == locations[9].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if(status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[11]);
                if (player.transform.eulerAngles.y > 89 && player.transform.eulerAngles.y < 91f)
                {
                    levelStatus = 21;
                    status = STATUS.MOVING;
                }
            }
        }
        // Right_Path_QteFail
        else if(levelStatus == 19)
        {
            QTEFail = false;
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[10]);
                if(player.transform.position == locations[10].position)
                {
                    status = STATUS.MOVING;
                    levelStatus = 20;
                }
            }            
        }
        // Right_Path_QteFail_Moving Back 
        else if(levelStatus == 20)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[9]);
                if(player.transform.position == locations[9].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if(status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[11]);
                if (player.transform.eulerAngles.y > 89f && player.transform.eulerAngles.y < 91f)
                {
                    levelStatus = 21;
                    status = STATUS.MOVING;
                }
            }
        }
        // Right_Path_B6
        else if(levelStatus == 21)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[11]);
                if(player.transform.position == locations[11].position)
                {
                    status = STATUS.BATTLE;
                }
            }
            if (status == STATUS.BATTLE)
            {
                if (!battleArea[10].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[10], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[10]);
                    if (player.transform.position == battleArea[10].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
                else if (battleArea[10].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[11], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[11]);
                    if (player.transform.position == battleArea[11].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
            }
            if (status == STATUS.CROUCH)
            {
                if (battleArea[10].GetComponent<HSProfile>().destroyed && !BADestroyed)
                {
                    BADestroyed = true;
                    status = STATUS.BATTLE;
                }
            }
            if (locations[11].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                BADestroyed = false;
                camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, 90, Time.time * 0.2f), 0);
                if (camera.transform.rotation.y <= 180)
                {
                    status = STATUS.IDLE;
                }
            }
            if (status == STATUS.IDLE)
            {
                sMove.PlayerMove(locations[11]);
                if (player.transform.position == locations[11].position)
                {
                    status = STATUS.MOVING;
                    levelStatus = 22;
                }
            }
        }
        // B7
        else if(levelStatus == 22)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[17]);
                if(player.transform.position == locations[17].position)
                {
                    status = STATUS.BATTLE;
                }
            }
            if (status == STATUS.BATTLE)
            {
                if (!battleArea[12].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[12], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[12]);
                    if (player.transform.position == battleArea[12].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
                else if (battleArea[12].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[13], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[13]);
                    if (player.transform.position == battleArea[13].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
            }
            if (status == STATUS.CROUCH)
            {
                if (battleArea[12].GetComponent<HSProfile>().destroyed && !BADestroyed)
                {
                    BADestroyed = true;
                    status = STATUS.BATTLE;
                }
            }
            if (locations[17].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                BADestroyed = false;
                camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, 90, Time.time * 0.2f), 0);
                if (camera.transform.rotation.y <= 180)
                {
                    status = STATUS.IDLE;
                }
            }
            if (status == STATUS.IDLE)
            {
                sMove.PlayerMove(locations[17]);
                if (player.transform.position == locations[17].position)
                {
                    status = STATUS.MOVING;
                    levelStatus = 23;
                }
            }
        }
        // Moving to uphill corner
        else if(levelStatus == 23)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[18]);
                if(player.transform.position == locations[18].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if(status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[19]);
                if (player.transform.eulerAngles.y > 359 && player.transform.eulerAngles.y < 361)
                {
                    levelStatus = 24;
                    status = STATUS.MOVING;
                }
            }
        }
        // B8
        else if(levelStatus == 24)
        {
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[19]);
                if(player.transform.position == locations[19].position)
                {
                    status = STATUS.BATTLE;
                }
            }            
            if (locations[19].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                status = STATUS.IDLE;                
            }
            if (status == STATUS.IDLE)
            {
                sMove.PlayerMove(locations[19]);
                if (player.transform.position == locations[19].position)
                {
                    status = STATUS.MOVING;
                    levelStatus = 25;
                }
            }
        }
        // To QTE
        else if(levelStatus == 25)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[20]);
                if(player.transform.position == locations[20].position)
                {
                    status = STATUS.QTE;
                }
            }
            if(status == STATUS.QTE)
            {
                if (QTESuccess)
                {
                    levelStatus = 26;
                    status = STATUS.MOVING;
                }
                else if (QTEFail)
                {
                    levelStatus = 27;
                    status = STATUS.MOVING;
                }
            }
        }
        // QTE Success
        else if(levelStatus == 26)
        {
            QTESuccess = false;
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[21]);
                if(player.transform.position == locations[21].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if(status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[22]);
                if (player.transform.eulerAngles.y > 89 && player.transform.eulerAngles.y < 91)
                {
                    levelStatus = 28;
                    status = STATUS.MOVING;
                }

            }
        }
        // QTE Fail
        else if (levelStatus == 27)
        {
            QTEFail = false;
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[21]);
                if (player.transform.position == locations[21].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if (status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[22]);
                if (player.transform.eulerAngles.y > 89 && player.transform.eulerAngles.y < 91)
                {
                    levelStatus = 28;
                    status = STATUS.MOVING;
                }

            }
        }
        // To next corner
        else if(levelStatus == 28)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[22]);
                if(player.transform.position == locations[22].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if(status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[23]);
                if (player.transform.eulerAngles.y > 179 && player.transform.eulerAngles.y < 181)
                {
                    levelStatus = 29;
                    status = STATUS.MOVING;
                }
            }
        }
        // B10
        else if(levelStatus == 29)
        {
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[24]);
                if(player.transform.position == locations[24].position)
                {
                    status = STATUS.BATTLE;
                }
            }
            if (status == STATUS.BATTLE)
            {
                if (!battleArea[14].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[14], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[14]);
                    if (player.transform.position == battleArea[14].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
                else if (battleArea[14].GetComponent<HSProfile>().destroyed)
                {
                    camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, cameraRotatePos[15], Time.time * 0.5f), 0);
                    sMove.PlayerMove(battleArea[15]);
                    if (player.transform.position == battleArea[15].position)
                    {
                        status = STATUS.CROUCH;
                    }
                }
            }
            if (status == STATUS.CROUCH)
            {
                if (battleArea[14].GetComponent<HSProfile>().destroyed && !BADestroyed)
                {
                    BADestroyed = true;
                    status = STATUS.BATTLE;
                }
            }
            if (locations[24].GetComponent<TargetProfile>().EnemyCount <= 0)
            {
                BADestroyed = false;
                camera.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(camera.transform.rotation.y, 180, Time.time * 0.2f), 0);
                if (camera.transform.rotation.y <= 180)
                {
                    status = STATUS.IDLE;
                }
            }
            if (status == STATUS.IDLE)
            {
                sMove.PlayerMove(locations[24]);
                if (player.transform.position == locations[24].position)
                {
                    status = STATUS.MOVING;
                    levelStatus = 30;
                }
            }
        }
        // To Corner QTE
        else if(levelStatus == 30)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[25]);
                if(player.transform.position == locations[25].position)
                {
                    status = STATUS.QTE;
                }
            }
            if(status == STATUS.QTE)
            {
                if (QTESuccess)
                {
                    status = STATUS.MOVING;
                    levelStatus = 31;
                }
                else if (QTEFail)
                {
                    status = STATUS.MOVING;
                    levelStatus = 32;
                }
            }
        }
        // QTE Success
        else if(levelStatus == 31)
        {
            QTESuccess = false;
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[26]);
                if(player.transform.position == locations[26].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if(status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[27]);
                if (player.transform.eulerAngles.y > 269 && player.transform.eulerAngles.y < 271)
                {
                    levelStatus = 33;
                    status = STATUS.MOVING;
                }
            }
        }
        // QTE Fail
        else if(levelStatus == 32)
        {
            QTEFail = false;
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[26]);
                if (player.transform.position == locations[26].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if (status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[27]);
                if (player.transform.eulerAngles.y > 269 && player.transform.eulerAngles.y < 271)
                {
                    levelStatus = 33;
                    status = STATUS.MOVING;
                }
            }
        }
        // To next QTE
        else if(levelStatus == 33)
        {
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[27]);
                if (player.transform.position == locations[27].position)
                {
                    status = STATUS.QTE;
                }
            }
            if (status == STATUS.QTE)
            {
                if (QTESuccess)
                {
                    status = STATUS.MOVING;
                    levelStatus = 34;
                }
                else if (QTEFail)
                {
                    status = STATUS.MOVING;
                    levelStatus = 35;
                }
            }
        }
        // QTE Success
        else if(levelStatus == 34)
        {
            QTESuccess = false;
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[28]);
                if (player.transform.position == locations[28].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if (status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[29]);
                if (player.transform.eulerAngles.y > 179 && player.transform.eulerAngles.y < 181)
                {
                    levelStatus = 36;
                    status = STATUS.MOVING;
                }
            }
        }
        // QTE Fail
        else if(levelStatus == 35)
        {
            QTEFail = false;
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[28]);
                if (player.transform.position == locations[28].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if (status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[29]);
                if (player.transform.eulerAngles.y > 179 && player.transform.eulerAngles.y < 181)
                {
                    levelStatus = 36;
                    status = STATUS.MOVING;
                }
            }
        }
        // To Next QTE
        else if(levelStatus == 36)
        {
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[29]);
                if (player.transform.position == locations[29].position)
                {
                    status = STATUS.QTE;
                }
            }
            if (status == STATUS.QTE)
            {
                if (QTESuccess)
                {
                    status = STATUS.MOVING;
                    levelStatus = 37;
                }
                else if (QTEFail)
                {
                    status = STATUS.MOVING;
                    levelStatus = 38;
                }
            }
        }
        // QTE Success
        else if(levelStatus == 37)
        {
            QTESuccess = false;
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[30]);
                if (player.transform.position == locations[30].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if (status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[31]);
                if (player.transform.eulerAngles.y > 269 && player.transform.eulerAngles.y < 271)
                {
                    levelStatus = 39;
                    status = STATUS.MOVING;
                }
            }
        }
        // QTE Fail
        else if (levelStatus == 38)
        {
            QTEFail = false;
            if (status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[30]);
                if (player.transform.position == locations[30].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if (status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[31]);
                if (player.transform.eulerAngles.y > 269 && player.transform.eulerAngles.y < 271)
                {
                    levelStatus = 39;
                    status = STATUS.MOVING;
                }
            }
        }
        // To Riverside
        else if(levelStatus == 39)
        {
            if(status == STATUS.MOVING)
            {
                sMove.PlayerMove(locations[31]);
                if(player.transform.position == locations[31].position)
                {
                    status = STATUS.TURNING;
                }
            }
            if(status == STATUS.TURNING)
            {
                sMove.PlayerRotate(locations[32]);
                if (player.transform.eulerAngles.y > 179 && player.transform.eulerAngles.y < 181)
                {
                    levelStatus = 40;
                    status = STATUS.IDLE;
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
        CROUCH,
        QTE,
        INTERACTABLE,
        PORTAL,
        TOTAL_STATE
    }