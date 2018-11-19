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

    private void Start()
    {
        CC = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();
        plyrHp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHp>();
        crouch = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Crouch>();
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
                    Fire();
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
