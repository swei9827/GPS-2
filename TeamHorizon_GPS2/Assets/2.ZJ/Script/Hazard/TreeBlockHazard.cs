using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBlockHazard : MonoBehaviour {

    private IEnumerator coroutine;
    public int Damage;
    public float nextMelee = 0.0f;
    public float meleeHit = 0.2f;
    public int TBHealth;
    public float slowDuration;
    public float slowSpeed;
    public Transform Player;
    public float MaxDistance;
    float originalSpeed = 10.0f;
    ControlCenter cc;
    bool damageDealt;

    void Start()
    {
        cc = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();
    }

    void Update()
    {
        if (TBHealth <= 0)
        {
            Destroy(this);
        }
    }

    public void TreeBlockDamage()
    {
        float distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= MaxDistance)
        {
            TBHealth -= 1;
        }       
    }

    void OnTriggerEnter (Collider collision)
    {
        if(TBHealth > 0)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (cc.Level1 && !damageDealt)
                {
                    Player.GetComponent<PlayerHp>().health -= Damage;
                    damageDealt = true;
                }
                Player.GetComponent<ScriptedMovement>().speed = slowSpeed;
                coroutine = ResetSpeed(slowDuration);
                StartCoroutine(coroutine);
            }

            if (collision.gameObject.CompareTag("PlayerBlade"))
            {
                if(Time.deltaTime > nextMelee)
                {
                    nextMelee = Time.deltaTime + meleeHit;
                    TBHealth -= 2;
                } 
            }
        }          
    }

    private IEnumerator ResetSpeed(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ScriptedMovement>().speed = originalSpeed;
    }
}
