using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour {

    [SerializeField] Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    bool startShoot = false;

    public float timer;
    public float shootDelayTime;

    public int EnemyArea = 0;
    public int playerArea = 0;
    public ControlCenter CC;

    private void Start()
    {
        CC = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();
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
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
    }
}
