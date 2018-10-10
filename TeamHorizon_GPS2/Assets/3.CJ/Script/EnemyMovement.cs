using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool left;
    public bool right;
    public bool down;
    public bool top;

    public float moveSpeed;
    private float tempX;
    private float tempY;

    private Rigidbody rb;
    private Vector3 endPosition;

    public float endPosX;
    public float endPosY;

    // When Reaching CheckPoint
    public Transform player;
    public bool EnemyStartAppear = false;
    public float PlayerZAtCheckpoint;
    public static bool StartShooting = false;

    public int hp;

    void Start()
    {
        tempX = this.transform.position.x;
        tempY = this.transform.position.y;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.z >= PlayerZAtCheckpoint)
        {
            EnemyStartAppear = true;
        }

        if (left == true && right == false && down == false && top == false && EnemyStartAppear == true)
        {
            endPosition = new Vector3(-endPosX, this.transform.position.y, this.transform.position.z);
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
                }
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
                }
            }
        }
    }

   /* private void OnMouseDown()
    {
        if (hp > 0)
        {
            hp -= 1;

        }
        else if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }*/
}

