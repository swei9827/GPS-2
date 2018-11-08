using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBlockHazard : MonoBehaviour {

    private IEnumerator coroutine;
    public int TBHealth;
    public float slowDuration;
    public float slowSpeed;
    public Transform Player;
    public float MaxDistance;
    float originalSpeed = 10.0f;

    void OnMouseDown()
    {
        float distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= MaxDistance)
        {
            TBHealth -= 1;
        }
    }

    void OnTriggerEnter (Collider collision)
    {
        if(TBHealth > 0)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player.GetComponent<ScriptedMovement>().speed = slowSpeed;
                coroutine = ResetSpeed(slowDuration);
                StartCoroutine(coroutine);
            }
        }          
    }

    private IEnumerator ResetSpeed(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ScriptedMovement>().speed = originalSpeed;
    }
}
