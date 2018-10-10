﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProfile : MonoBehaviour {

    public GameObject player;
    public int TargetID;
    public bool ConnectionNodes;
    public bool BattleNodes;
    public bool CameraPanNodes;
    public int SetHazardID;
    public bool QTENodes;
 
    
    Transform playerPos;
    bool reached = false;

    // Use this for initialization
    void Start()
    {
        playerPos = player.GetComponent<Transform>();        
    }

    // Update is called once per frame
    void Update()
    {
        ShowPlayerStatus(); 
    }

    void ShowPlayerStatus()
    {
        if (playerPos.position == this.transform.position && !reached)
        {
            Debug.Log("Reached Target : " + this.name);
            reached = true;
            if (CameraPanNodes)
            {
                GameObject.FindGameObjectWithTag("2ndCamera").GetComponent<PanToTarget>().hazardID = SetHazardID;
            }
        }
    }
}
