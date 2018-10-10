using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedMovement : MonoBehaviour {

    public Transform goal;
    public List<Transform> locations = new List<Transform>();
    public float speed;
    public float turnSpeed;

    private IEnumerator coroutine;

    public void PlayerMovement(float waitTime, int targetCount)
    {        
        coroutine = WaitAndMove(waitTime, targetCount);
        StartCoroutine(coroutine);
    }    

    public void transitionMovement(int targetCount)
    {
        StopAllCoroutines();
        goal = locations[targetCount];
        float step = speed * Time.deltaTime;

        Vector3 targetDir = goal.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnSpeed * step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
        transform.position = Vector3.MoveTowards(transform.position, goal.position, step);
    }

    private IEnumerator WaitAndMove(float waitTime, int targetCount)
    {
        yield return new WaitForSeconds(waitTime);
        goal = locations[targetCount];
        float step = speed * Time.deltaTime;

        Vector3 targetDir = goal.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnSpeed * step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
        transform.position = Vector3.MoveTowards(transform.position, goal.position, step);
    }
}
