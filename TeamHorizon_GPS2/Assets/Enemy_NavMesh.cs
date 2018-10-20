using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_NavMesh : MonoBehaviour {

    Vector3 playerPos;
    GameObject player;

    public Transform target;
    NavMeshAgent agent;
    NavMeshPath path;
    public float DelayTime;
    WaitForSeconds delay;

    // Use this for initialization
    void Start () {
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
    }

    void getCurrentPath()
    {
        path = agent.path;
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
