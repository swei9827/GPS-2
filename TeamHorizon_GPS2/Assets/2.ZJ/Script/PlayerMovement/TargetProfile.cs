using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetProfile : MonoBehaviour {

    public GameObject player;
    public bool ConnectionNodes;
    public bool BattleNodes;
    public bool CameraPanNodes;
    public bool QTENodes;
    public bool InteractableNodes;
    public bool PortalNodes;
    public int NodeID;
    Transform playerPos;
    ControlCenter cc;
    bool reached = false;

    public static int PlayerArea = 0;

    // Use this for initialization
    void Start()
    {
        playerPos = player.GetComponent<Transform>();        
        cc = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();
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
            PlayerArea++;           
        }
    }

    public void EnterBattlePhase(int ID)
    {
        if (BattleNodes && reached && !cc.EnemyEliminated && NodeID == ID)
        {
            cc.OnBattle = true;
        }
        else if (BattleNodes && reached && cc.EnemyEliminated && NodeID == ID)
        {
            cc.OnBattle = false;
            if (playerPos.position == this.transform.position)
            {
                cc.BattleCompleted = true;
            }
        }
    }
}
