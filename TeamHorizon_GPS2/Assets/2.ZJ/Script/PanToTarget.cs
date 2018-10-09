using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanToTarget : MonoBehaviour {

    public Transform target;
    Quaternion currentRotation;
    Camera cam;
    bool rotationSet = false;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Pan();
        }
        if (Input.GetKey(KeyCode.F))
        {
            ResetCamera();
        }

    }

    void Pan()
    {
        if (!rotationSet)
        {
            currentRotation = transform.rotation;
            rotationSet = true;
        }
        Debug.Log(currentRotation);
        Quaternion lookOnLook = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 20.0f, 0.5f);
    }

    void ResetCamera()
    {     
        transform.rotation = Quaternion.Slerp(transform.rotation, currentRotation, Time.deltaTime);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 40.0f , 0.5f);
        rotationSet = false;
    }
}
