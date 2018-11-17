using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrow : MonoBehaviour {

    GameObject prefab;
    public Transform Player;
    public int MinDist = 0;
    public int MoveSpeed = 4;
    public bool isAttacking = false;



    void Start () {
        
        prefab = Resources.Load("Projectile") as GameObject;


    }

    private IEnumerator EnemyGrenade()
    {

        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {
            if(isAttacking == false)
            {
                GameObject projectile = Instantiate(prefab) as GameObject;
         
                isAttacking = true;
                yield return new WaitForSeconds(2f);
                isAttacking = false;
                
            }
           
        }

    }


	// Update is called once per frame
	void Update () {
        StartCoroutine(EnemyGrenade());
    }
}
