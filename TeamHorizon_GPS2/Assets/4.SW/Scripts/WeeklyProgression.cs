using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeklyProgression : MonoBehaviour {

    public GameObject tree;

	void Start () {
        tree.GetComponent<TreeFallHazard>().TreeFalling();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
