using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Level lvlController;
    public bool left;
    public bool right;
    public bool down;
    public bool top;

    public bool move;

    public float moveSpeed;
    private float tempX;
    private float tempY;

    private Rigidbody rb;
    private Vector3 endPosition;
    private Vector3 startPosition;

    public float endPosX;
    public float endPosY;
    private ControlCenter CC;

    // When Reaching CheckPoint
    public Transform player;
    public bool EnemyStartAppear = false;

    public int EnemyArea = 0;
    int playerArea = 0;

    public static bool StartShooting = false;

    public float timer;
    public int hideDelayTimer = 3;

    public bool enemyHide = false;

    public int hp;

    public static int enemyCountArea = 5;

    void Start()
    {
        tempX = this.transform.position.x;
        tempY = this.transform.position.y;
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rb = GetComponent<Rigidbody>();
        CC = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();
        lvlController = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<Level>();
    }

    // Update is called once per frame
    void Update()
    {
        playerArea = TargetProfile.PlayerArea;
        if (this.hp <= 0)
        {
            Destroy(this.gameObject);
            enemyCountArea -= 1;
            CC.enemyCount -= 1;
            lvlController.GetComponent<Level>().setScore(lvlController.GetComponent<Level>().getScore()+1000);
        }

        if (EnemyArea == playerArea )
        {
            EnemyStartAppear = true;  
        }

        if(move == true && EnemyStartAppear == true)
        {
            endPosition = new Vector3(-endPosX, endPosY, this.transform.position.z);
            Invoke("StartOnBattle", 3f);
            if (transform.position != endPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
            }
        }

        if (transform.position == endPosition)
        {
            move = false;
            EnemyStartAppear = false;
            StartShooting = true;

            timer += Time.deltaTime;
            //Debug.Log(timer);
        }

        if (timer >= hideDelayTimer)
        {
            StartShooting = false;
            enemyHide = true;
            if (enemyHide == true && StartShooting == false)
            {
                //back last position;
                if (transform.position != startPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
                }
            }
        }

        if(enemyHide == true && StartShooting == false)
        {
            if (transform.position == startPosition && enemyHide == true)
            {
                timer = 0;
                enemyHide = false;
                EnemyStartAppear = true;
                move = true;
            }
        }
        /*
        if (left == true && right == false && down == false && top == false && EnemyStartAppear == true)
        {
            endPosition = new Vector3(-endPosX, this.transform.position.y, this.transform.position.z);
            if (transform.position != endPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
            }
        }
        if (left == false && right == true && down == false && top == false && EnemyStartAppear == true)
        {
            endPosition = new Vector3(endPosX, this.transform.position.y, this.transform.position.z);
            if (transform.position != endPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
                if (transform.position == endPosition)
                {
                    left = false;
                    right = false;
                    down = false;
                    top = false;
                    EnemyStartAppear = false;
                    StartShooting = true;

                    timer += Time.deltaTime;

                    if (timer >= hideDelayTimer)
                    {
                        StartShooting = false;
                        enemyHide = true;
                        timer = 0;
                        if (enemyHide == true && StartShooting == false)
                        {
                            //back last position;
                            if (transform.position != startPosition)
                            {
                                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
                                if (transform.position == startPosition)
                                {
                                    timer++;
                                    enemyHide = false;
                                    EnemyStartAppear = true;
                                    right = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        if (left == false && right == false && down == true && top == false && EnemyStartAppear == true)
        {
            endPosition = new Vector3(this.transform.position.x, -endPosY, this.transform.position.z);
            if (transform.position != endPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
                if (transform.position == endPosition)
                {
                    left = false;
                    right = false;
                    down = false;
                    top = false;
                    EnemyStartAppear = false;
                    StartShooting = true;

                    timer += Time.deltaTime;

                    if (timer >= hideDelayTimer)
                    {
                        StartShooting = false;
                        enemyHide = true;
                        timer = 0;
                        if (enemyHide == true && StartShooting == false)
                        {
                            //back last position;
                            if (transform.position != startPosition)
                            {
                                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
                                if (transform.position == startPosition)
                                {
                                    timer++;
                                    enemyHide = false;
                                    EnemyStartAppear = true;
                                    down = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        if (left == false && right == false && down == false && top == true && EnemyStartAppear == true)
        {
            endPosition = new Vector3(this.transform.position.x, endPosY, this.transform.position.z);
            if (transform.position != endPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
                if (transform.position == endPosition)
                {
                    left = false;
                    right = false;
                    down = false;
                    top = false;
                    EnemyStartAppear = false;
                    StartShooting = true;

                    timer += Time.deltaTime;

                    if (timer >= hideDelayTimer)
                    {
                        StartShooting = false;
                        enemyHide = true;
                        timer = 0;
                        if (enemyHide == true && StartShooting == false)
                        {
                            //back last position;
                            if (transform.position != startPosition)
                            {
                                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
                                if (transform.position == startPosition)
                                {
                                    timer++;
                                    enemyHide = false;
                                    EnemyStartAppear = true;
                                    top = true;
                                }
                            }
                        }
                    }
                }
            }
        }*/


    }

    void StartOnBattle()
    {
        //CC.OnBattle = true;
    }
}

