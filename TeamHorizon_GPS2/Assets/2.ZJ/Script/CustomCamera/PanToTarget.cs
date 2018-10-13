using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanToTarget : MonoBehaviour {

    public float zoomSpeed;
    public GameObject[] target;
    public int hazardID;
    Quaternion currentRotation;
    public Camera cam;
    bool rotationSet = false;
    private IEnumerator coroutine;

    void Start()
    {
        cam = GetComponent<Camera>();
    }   

    public void CameraPan()
    {
        coroutine = WaitAndPan(1.0f);
        StartCoroutine(coroutine);
    }

    void Reset()
    {
        coroutine = WaitAndReset(1.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitAndPan(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (target[hazardID].GetComponentInParent<TreeFallHazard>().FallenTree == false)
        {
            if (!rotationSet)
            {
                currentRotation = transform.rotation;
                rotationSet = true;
            }
            Quaternion lookOnLook = Quaternion.LookRotation(target[hazardID].transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 15.0f, zoomSpeed);
            Reset();
        }
    }

    private IEnumerator WaitAndReset(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (target[hazardID].GetComponentInParent<TreeFallHazard>().FallenTree == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, currentRotation, Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 40.0f, zoomSpeed);
            rotationSet = false;
        }
    }
}
