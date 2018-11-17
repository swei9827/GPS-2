using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onGestureTrigger : MonoBehaviour
{
    GestureManager gm;
    public bool onQTETrigger = false;
    public Transform Spawnpoint;

    public GameObject Prefab;//gesturedrawing prefab
    public bool boxGesture;


    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        //print("QTE TRIGGET ON");
        if (other.gameObject.tag == "Player")
        {
            print("QTE TRIGGER ON");
            onQTETrigger = true;

            if (onQTETrigger == true)
            {
                Transform player = GameObject.FindGameObjectWithTag("Player").transform;

                

                GameObject QTE = Instantiate(Prefab, new Vector3(Spawnpoint.position.x + player.position.x, Spawnpoint.position.y + player.position.y, Spawnpoint.position.z + player.position.z), player.rotation);
                QTE.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;


                gm = GameObject.FindGameObjectWithTag("boxGesture").GetComponent<GestureManager>();


                Time.timeScale = 0.1f; //time.timescale = 0.5; // to slow doen the game during qte
                //gm.timeMoving = true; //time duration for qte drawing

            }
            Destroy(gameObject);
        }

    }
}
