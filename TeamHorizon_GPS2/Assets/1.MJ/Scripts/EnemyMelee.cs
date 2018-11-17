using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : MonoBehaviour {

    public Transform Player;
    int MoveSpeed = 4;
    public int MaxDist = 10;
    public int MinDist = 5;

    Vector3 p;

    void Start()
    {
        p = transform.position;
    }

    void Update()
    {
        transform.LookAt(Player);

        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            {
                //enemy melee attack
            }
        }
    }

    void EnemyMeleeAttack()
    {

    }

}
