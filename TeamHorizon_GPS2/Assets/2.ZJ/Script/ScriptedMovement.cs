using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedMovement : MonoBehaviour {

    public Transform goal;
    public List<Transform> locations = new List<Transform>();
    public float speed;
    public float turnSpeed;
    int targetCount = 0;


    void Update()
    {
        float step = speed * Time.deltaTime;               

        if (Input.GetKeyDown(KeyCode.A))
        {
            targetCount++;
        }
        goal = locations[targetCount];

        Vector3 targetDir = goal.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnSpeed*step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);    
        transform.position = Vector3.MoveTowards(transform.position, goal.position, step);       
    }
}
