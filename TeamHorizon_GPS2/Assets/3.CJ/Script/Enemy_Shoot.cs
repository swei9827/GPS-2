using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour {

    bool startShoot = false;

    public float timer;
    public float shootDelayTime;

    public int EnemyArea = 0;
    public int playerArea = 0;
    public ControlCenter CC;

    public float bulletDamage;
    PlayerHp plyrHp;
    Player_Crouch crouch;

    int tempRand = 0;

    public Transform Player;
    public float MaxDistance;

    private void Start()
    {
        CC = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();
        plyrHp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHp>();
        crouch = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Crouch>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        playerArea = CC.levelStatus;
        timer += Time.deltaTime;
        startShoot = EnemyMovement.StartShooting;
        if(timer  > shootDelayTime)
        {
            if (startShoot == true)
            {
                if(EnemyArea == playerArea)
                {
                    float distance = Vector3.Distance(Player.position, transform.position);
                    if(distance <= MaxDistance)
                    {
                        Fire();
                    }
                }
            }
            timer = 0;
        }
        
    }

    void Fire()
    {
        tempRand = Random.Range(0, 3);
        if(tempRand == 2)
        {
            if (crouch.isCrouch == true)
            {
                
            }
            else
            {
                plyrHp.TakeDamage(bulletDamage);
                //hitByBulletForFirstTime = true;
            }
        } 
    }
}
