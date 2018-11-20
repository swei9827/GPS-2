using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IObstacles : MonoBehaviour {

    public int IOHealth;
    public float interactDuration;
    public Transform Player;
    public float MaxDistance;
    public GameObject GameController;

    public void IObstaclesDamage()
    {
        float distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= MaxDistance)
        {
            IOHealth -= 1;
        }
    }

    void Update () {
        if(GameController.GetComponent<ControlCenter>().status == STATUS.INTERACTABLE)
        {
            StartCoroutine(Interacting(interactDuration));
        }       
	}

    private IEnumerator Interacting(float waitTime)
    {
        if (IOHealth <= 0)
        {
            GameController.GetComponent<ControlCenter>().InteractSuccess = true;
        }
        yield return new WaitForSeconds(waitTime);
        GameController.GetComponent<ControlCenter>().InteractFail = true;
    }
}
