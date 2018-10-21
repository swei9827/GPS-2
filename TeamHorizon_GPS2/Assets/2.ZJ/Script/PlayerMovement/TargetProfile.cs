using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProfile : MonoBehaviour {

    public GameObject player;   
    public int EnemyCount;
    Transform playerPos;
    bool reached = false;

    public static int PlayerArea = 0;

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
            PlayerArea++;
            reached = true;
        }
    }    
}
