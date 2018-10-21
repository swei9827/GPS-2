using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Destroyable_Bullet : MonoBehaviour {

    public int bulletSpeed;
    public Transform player;
    Rigidbody rb;
    public int hp;

    Vector3 playerPos;

    PlayerHp plyrHp;
    Player_Crouch crouch;

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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (crouch.isCrouch == true)
            {
                Destroy(gameObject);
            }
            else
            {
                plyrHp.TakeDamage(10.0f);
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
