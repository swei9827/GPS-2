using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_NavMesh : MonoBehaviour {

    Vector3 playerPos;
    Vector3 enemyOriginalPos;
    GameObject player;

    public Transform target;
    NavMeshAgent agent;
    NavMeshPath path;
    public float DelayTime;
    WaitForSeconds delay;

    bool backOriginalPos = false;

    // Use this for initialization
    void Start () {
        enemyOriginalPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(FindPathRoutine());
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        playerPos = player.transform.position;
        getCurrentPath();
        transform.rotation = Quaternion.LookRotation(playerPos);
        delay = new WaitForSeconds(DelayTime);
        if(backOriginalPos == true)
        {

        }
    }

    void getCurrentPath()
    {
        path = agent.path;
    }

    void goBackOriginalPos()
    {

    }

    IEnumerator FindPathRoutine()
    {
        while(true)
        {
            yield return DelayTime;
            agent.SetDestination(target.position);
        }
    }
}
