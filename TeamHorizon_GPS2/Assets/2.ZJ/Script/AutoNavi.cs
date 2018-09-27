using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoNavi : MonoBehaviour
{

    public Transform goal;
    public List<Transform> locations = new List<Transform>();
    NavMeshAgent agent;
    int targetCount = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetCount++;
            Debug.Log(targetCount);
        }
        goal = locations[targetCount];
        agent.destination = goal.position;
    }
}
