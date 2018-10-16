using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoUseFPSMovement : MonoBehaviour {
    public float speed;
    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 100.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
}
