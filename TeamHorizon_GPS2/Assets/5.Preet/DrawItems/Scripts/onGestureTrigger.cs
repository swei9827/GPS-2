using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onGestureTrigger : MonoBehaviour
{
    public Transform Spawnpoint;
    public GameObject Prefab;
    public bool boxGesture;
  

   
    void OnTriggerEnter(Collider other)
    {
            print("YAY!");

       {
           Instantiate(Prefab , Spawnpoint.position, Spawnpoint.rotation);
       }

        //if (tag == "boxGesture" && !gameObject.activeSelf)
        //  if (other.gameObject.tag == "boxGesture" && boxGesture == false)
        //    {
        //  Prefab.SetActive(true);
        //  }

        Time.timeScale = 0.1f; //time.timescale = 0.5; // to slow doen the game during qte
    }

}
