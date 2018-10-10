using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shoot : MonoBehaviour {

    [SerializeField] Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    bool startShoot = false;

    public float timer;
    public int shootDelayTime;

    public int EnemyArea = 0;
    public int playerArea = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerArea = TargetProfile.PlayerArea;
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
