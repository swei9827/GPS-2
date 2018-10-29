using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTest : MonoBehaviour {

    public Vector3 mouse_pos;
    public Camera meleeCamera;
    public bool hitEnvironment = false;

    private void Start()
    {
        
    }

    void Update()
    {
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5;
        Vector3 worldPos = meleeCamera.ScreenToWorldPoint(mouse_pos);
        transform.LookAt(worldPos);
    }

}
    