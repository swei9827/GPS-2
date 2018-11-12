using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : MonoBehaviour {

    GameObject prefab;
    public Transform Player;
    int MinDist = 10;
    int MoveSpeed = 4;


	void Start () {
        prefab = Resources.Load("Projectile") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position, Player.position) >= MinDist)
        {
            GameObject projectile = Instantiate(prefab) as GameObject;
            projectile.transform.position = transform.position += transform.forward * MoveSpeed * Time.deltaTime; ;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * 40;
        }
	}
}
