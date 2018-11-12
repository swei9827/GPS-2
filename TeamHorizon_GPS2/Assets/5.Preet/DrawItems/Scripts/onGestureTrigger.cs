using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onGestureTrigger : MonoBehaviour
{
    public bool onQTETrigger = false;
    public Transform Spawnpoint;
    public GameObject Prefab;
    public bool boxGesture;


    void OnTriggerEnter(Collider other)
    {
        //print("QTE TRIGGET ON");
        if(other.gameObject.tag == "Player")
        {
            print("QTE TRIGGET ON");
            onQTETrigger = true;
            if (onQTETrigger == true)
            {
                this.gameObject.GetComponent<Renderer>().material.color = Color.red;
                Prefab.SetActive(true);
                //Instantiate(Prefab, Spawnpoint.position, Spawnpoint.rotation);
                Time.timeScale = 0.01f; //time.timescale = 0.5; // to slow doen the game during qte
            }
            Destroy(gameObject);
        }

        

                //if (tag == "boxGesture" && !gameObject.activeSelf)
        //  if (other.gameObject.tag == "boxGesture" && boxGesture == false)
        //    {
        //  Prefab.SetActive(true);
        //  }


    }
  //  private void OnCollisionStay2D(Collision2D collision)
   // {
        
  //  }
}
