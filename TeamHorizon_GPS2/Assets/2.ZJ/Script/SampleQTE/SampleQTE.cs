using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleQTE : MonoBehaviour {

    public Material mats;
    public float MaxDistance;
    public Transform Player;
    public ControlCenter cc;
    public int QTEHealth;

    void OnMouseDown()
    {
        float distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= MaxDistance)
        {
            QTEHealth -= 1;
        }
    }

    void Update()
    {
        if (QTEHealth <= 0)
        {
            GetComponent<MeshRenderer>().material = mats;
            cc.QTESuccess = true;
        }
    }
}
