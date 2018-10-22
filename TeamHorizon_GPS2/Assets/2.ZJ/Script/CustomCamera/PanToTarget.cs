using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanToTarget : MonoBehaviour {

    public float zoomSpeed;
    public float zoomLevel;
    public bool panComplete;
    public GameObject[] target;

    Quaternion currentRotation;
    Camera cam;
    bool rotationSet = false;
    private IEnumerator coroutine;

    void Start()
    {
        cam = GetComponent<Camera>();
    }   

    public void CameraPan(int targetID)
    {
        if (target[targetID].GetComponentInParent<TreeFallHazard>().FallenTree == false)
        {
            if (!rotationSet)
            {
                currentRotation = transform.rotation;
                rotationSet = true;
            }
            Quaternion lookOnLook = Quaternion.LookRotation(target[targetID].transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, zoomLevel, zoomSpeed);
            Reset(targetID);
        }
    }

    void Reset(int targetID)
    {
        coroutine = WaitAndReset(1.0f, targetID);
        StartCoroutine(coroutine);       
    }

    private IEnumerator WaitAndReset(float waitTime, int targetID)
    {
        yield return new WaitForSeconds(waitTime);
        if (target[targetID].GetComponentInParent<TreeFallHazard>().FallenTree == true)
        {            
            transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, 0.8f);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 40.0f, zoomSpeed);
            rotationSet = false;
            yield return new WaitForSeconds(0.5f);
            panComplete = true;
        }
        yield return new WaitForSeconds(1.0f);
        panComplete = false;
    }
}
