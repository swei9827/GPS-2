using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    

    public Material hitMaterial;
    public int numBoxDestroyed = 20;
    public bool hasBeenHit = false;


    ControlCenter cc;
    public GameObject Prefab;//gesturedrawing prefab
    public GameObject Spawnpoint;
    Camera SubCamera;//2ndCam
    public bool allBoxesHit = false;
    public int GestureBoxCount = 18;
    public bool beenClicked = false;
    public float timeLeft = 3.0f;
    public bool timeMoving = false;


    // Use this for initialization
    void Start()
    {
        cc = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();
        SubCamera = GameObject.FindGameObjectWithTag("2ndCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeMoving == true)
        {

            timeLeft -= Time.deltaTime * 10;
            {

                if (timeLeft <= 0)
                {
                    if (cc.QTESuccess == true)
                    {
                        timeMoving = false;
                    }
                    else
                    {
                        Debug.Log("TIMES UP QTE FAIL");
                        cc.QTEFail = true;
                        Destroy(gameObject);
                    }

                }
            }

        }
        

        if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || (Input.GetMouseButton(0) && !beenClicked)))
        {
            Debug.Log(Input.mousePosition);
            Ray ray = SubCamera.ScreenPointToRay(Input.mousePosition);
            Debug.Log("hit raycast");
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log(hitInfo);
                var rig = hitInfo.collider.GetComponent<Rigidbody>();
                var destroyTime = 5;
                //if (rig != null)
                if (rig != null && rig.GetComponent<MeshRenderer>().material != hitMaterial)
                {
                    GestureBoxCount -= 1;
                    Destroy(hitInfo.collider.gameObject);
                    Debug.Log("HIT BOX");
                    rig.GetComponent<MeshRenderer>().material = hitMaterial;

                }



                print(GameObject.FindGameObjectsWithTag("boxGesture").Length);

                //if (GameObject.FindGameObjectsWithTag("boxGesture").Length == 18)
                if (GestureBoxCount <= 0)
                {
                    Debug.Log("SUCCESS");
                    cc.QTESuccess = true;
                    Destroy(gameObject, destroyTime);
                    Time.timeScale = 1.0f;
                }
                else if (timeLeft <= 0.5f && GameObject.FindGameObjectsWithTag("boxGesture").Length>=0)
                {
                    Debug.Log("FAIL");
                    cc.QTEFail = true;
                    Destroy(gameObject, destroyTime);
                    Time.timeScale = 1.0f;
                }

            }

        }
    }


  /*   public void OnMouseDown()
    {
        if (beenClicked)
            return;

        // do stuff
        beenClicked = true;
    }





    public int boxesLeft = 18;

    public void boxesHasDied()
    {
        boxesLeft--;
        print(boxesLeft);

        if (boxesLeft == 0)
        {
            Debug.Log("ZERO BOXES");
            cc.QTESuccess = true;
        }
    }


    void Destroyedcount() 
     {
         numBoxDestroyed++;
         //if (numBoxDestroyed > 18)
        // {

             if (GameObject.FindGameObjectsWithTag("boxGesture").Length >= 18)
             {
                 Debug.Log("dvfdvdsvsdf");
             }
             //GetComponent<MeshRenderer>().enabled = true;
        // }
     }*/
}

/*  void GestureCorrect()
  {
      if (collider.GetComponent<Rigidbody>()  numBox)
      //this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
      Debug.Log("CORRECT"); //this isnt working wtf! how to impliment without plugin
  }

  //player failed in the gesture
  void GestureWrong()
  {
      this.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
      Debug.Log("WRONG");
  }
}*/
