using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBlockHazard : MonoBehaviour {

    private IEnumerator coroutine;
    public float slowDuration;
    public float slowSpeed;
    float originalSpeed = 10.0f;

    void OnTriggerEnter (Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<ScriptedMovement>().speed = slowSpeed;
            coroutine = ResetSpeed(slowDuration);
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator ResetSpeed(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ScriptedMovement>().speed = originalSpeed;
    }
}
