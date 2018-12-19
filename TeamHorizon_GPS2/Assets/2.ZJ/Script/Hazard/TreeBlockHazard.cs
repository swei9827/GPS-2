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
    private bool entered = false;
    public bool PlayerHit;
    float originalSpeed = 10.0f;
    ControlCenter cc;
    bool damageDealt;
    private float distance = 0;
    public Material outlineMat;

    void Start()
    {
        cc = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();
    }

    void Update()
    {
        if (TBHealth <= 0)
        {
            Destroy(this.gameObject);
        }
        distance = Vector3.Distance(Player.position, transform.position);
        if(distance <= MaxDistance)
        {
            if(!entered)
            {
                EnterInteractZone();
            }
        }
    }

    public void TreeBlockDamage()
    {
        if (!PlayerHit)
        {
            if (distance <= MaxDistance)
            {
                TBHealth -= 1;
            }
        }
    }

    public void EnterInteractZone()
    {
        entered = true;
        this.GetComponent<MeshRenderer>().material = outlineMat;
    }

    void OnTriggerEnter (Collider collision)
    {
        if(TBHealth > 0)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (cc.Level1 && !damageDealt)
                {
                    Player.GetComponent<PlayerHp>().TakeDamage(Damage);
                    damageDealt = true;
                }
                Player.GetComponent<ScriptedMovement>().speed = slowSpeed;
                coroutine = ResetSpeed(slowDuration);
                StartCoroutine(coroutine);
            }
        }          
    }

    private IEnumerator ResetSpeed(float waitTime)
    {
        PlayerHit = true;
        yield return new WaitForSeconds(waitTime);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ScriptedMovement>().speed = originalSpeed;
    }
}
