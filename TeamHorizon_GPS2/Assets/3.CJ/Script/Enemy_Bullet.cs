using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour {

    public int bulletSpeed;
    public Transform player;
    public float bulletDamage;  
    Rigidbody rb;

    Vector3 playerPos;

    PlayerHp plyrHp;
    Player_Crouch crouch;

    public static bool hitByBulletForFirstTime = false;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("PlayerHead").transform;
        plyrHp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHp>();
        crouch = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Crouch>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector3(player.position.x, player.position.y, player.position.z) * bulletSpeed;

        transform.rotation = Quaternion.LookRotation(playerPos);

        rb.velocity = (player.position - transform.position).normalized * bulletSpeed;

        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter (Collider collider)
    {
        if (hitByBulletForFirstTime == false)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                if (crouch.isCrouch == true)
                {
                    Destroy(gameObject);
                }
                else
                {
                    plyrHp.TakeDamage(bulletDamage);
                    Destroy(gameObject);
                    hitByBulletForFirstTime = true;
                }
            }
        }
        else
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                if (crouch.isCrouch == true)
                {
                    Destroy(gameObject);
                }
                else
                {
                    plyrHp.TakeDamage(2.0f);
                    Destroy(gameObject);
                }
            }
        }
        

        if(collider.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
