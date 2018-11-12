using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {

    public int ObsHealth;
    public Transform Player;
    public float MaxDistance;
    public float nextMelee = 0.0f;
    public float meleeHit = 0.2f;
    public float slowDuration;
    public float slowSpeed;
    float originalSpeed = 10.0f;

    void OnMouseDown()
    {
        float distance = Vector3.Distance(Player.position, transform.position);
        if(distance <= MaxDistance)
        {
            ObsHealth -= 1;
        }        
    }

    void Update () {
	    if(ObsHealth <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter(Collider collision)
    {
        if (ObsHealth > 0)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.GetComponent<ScriptedMovement>().speed = slowSpeed;
                StartCoroutine(ResetSpeed(slowDuration));
            }
        }
        if (collision.gameObject.CompareTag("PlayerBlade"))
        {
            if (Time.deltaTime > nextMelee)
            {
                nextMelee = Time.deltaTime + meleeHit;
                ObsHealth -= 2;
            }
        }
    }

    private IEnumerator ResetSpeed(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ScriptedMovement>().speed = originalSpeed;
    }
}
