using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour {

    public int bulletSpeed;
    public Transform player;
    Rigidbody rb;

    Vector3 playerPos;

    PlayerHp plyrHp;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("PlayerPoint").transform;
        plyrHp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHp>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = new Vector3(player.position.x, player.position.y + 5, player.position.z) * bulletSpeed;

        transform.rotation = Quaternion.LookRotation(playerPos);

        rb.velocity = (player.position - transform.position).normalized * bulletSpeed;

        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("HIT");
            plyrHp.TakeDamage(2.0f);
            //collision.gameObject.GetComponent<PlayerHp>().TakeDamage(2.0f);
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Hp>().hp -= 1;
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
