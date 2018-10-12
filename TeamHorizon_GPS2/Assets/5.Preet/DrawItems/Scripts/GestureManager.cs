using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{

    public Material hitMaterial;
    public int numBoxDestroyed = 0;
    
    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // if (Input.GetMouseButtonDown(0))
        if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
         || Input.GetMouseButton(0)))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                var rig = hitInfo.collider.GetComponent<Rigidbody>();
                var destroyTime = 5;
                if (rig != null)
                {
                    Debug.Log("CORRECT");
                    rig.GetComponent<MeshRenderer>().material = hitMaterial;
                    //rig.AddForceAtPosition(ray.direction * 50f, hitInfo.point, ForceMode.VelocityChange);
                    //Destroy(this.gameObject);

                    Destroy(gameObject, destroyTime);

                }
            }
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
    }
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

    