using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
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

    // When Reaching CheckPoint
    public Transform player;
    public bool EnemyStartAppear = false;
    public float PlayerZAtCheckpoint;
    public static bool StartShooting = false;

    public float timer;
    public int hideDelayTimer = 3;

    public bool enemyHide = false;

    public int hp;

    void Start()
    {
        tempX = this.transform.position.x;
        tempY = this.transform.position.y;
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.z >= PlayerZAtCheckpoint)
        {
            EnemyStartAppear = true;
        }

        if(move == true && EnemyStartAppear == true)
        {
            endPosition = new Vector3(-endPosX, this.transform.position.y, this.transform.position.z);
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
            Debug.Log(timer);
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
}

