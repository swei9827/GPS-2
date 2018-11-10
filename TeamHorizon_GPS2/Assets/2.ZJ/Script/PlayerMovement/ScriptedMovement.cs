using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ScriptedMovement : MonoBehaviour {

    public float speed;
    public float turnSpeed;
    float step;

    private IEnumerator coroutine;

    public void PlayerMove( Transform target)
    {
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }    

    public void PlayerRotate(Transform target)
    {
        step = speed * Time.deltaTime;
        Vector3 targetDir = target.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnSpeed * step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private IEnumerator WaitAndMove(float waitTime, Transform target)
    {
        yield return new WaitForSeconds(waitTime);       
        step = speed * Time.deltaTime;              
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    private IEnumerator WaitAndRotate(float waitTime, Transform target)
    {
        yield return new WaitForSeconds(waitTime);
        step = speed * Time.deltaTime;
        Vector3 targetDir = target.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnSpeed * step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir); 
    }


}
