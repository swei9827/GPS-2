using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFallHazard : MonoBehaviour
{
    float fall = 90;
    public float Damage;
    public int health;
    public bool FallenTree = false;
    public float slowDuration;
    public float slowSpeed;
    public GameObject Player;
    private Material mat;
    float originalSpeed = 10.0f;
    public float nextMelee = 0.0f;
    public float meleeHit = 0.2f;
    ControlCenter cc;
    bool damageDealt;

    private void Start()
    {
        mat = this.GetComponent<MeshRenderer>().material;
        cc = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            if(mat.color.a > 0)
            {
                Color newColor = mat.color;
                newColor.a -= Time.deltaTime;
                mat.color = newColor;
                gameObject.GetComponent<MeshRenderer>().material = mat;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && health > 0)
        {
            if (cc.Level1 && !damageDealt)
            {
                Player.GetComponent<PlayerHp>().health -= Damage;
                damageDealt = true;
            }
            Player.GetComponent<ScriptedMovement>().speed = slowSpeed;
            StartCoroutine(ResetSpeed(slowDuration));
        }/*
        if (collision.gameObject.CompareTag("PlayerBlade"))
        {
            if (Time.deltaTime > nextMelee)
            {
                nextMelee = Time.deltaTime + meleeHit;
                health -= 2;
            }
        }*/
    }

    public void TreeFallDamage()
    {
        if (FallenTree)
        {
            health -= 1;
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }        
    }

    public void TreeFalling()
    {
        if (FallenTree == false)
        {
            if (fall > 80 && fall <= 90)
            {
                fall -= Time.deltaTime * 10;
            }
            else if (fall > 70 && fall < 80)
            {
                fall -= Time.deltaTime * 15;
            }
            else if (fall > 60 && fall < 70)
            {
                fall -= Time.deltaTime * 20;
            }
            else if (fall > 45 && fall < 60)
            {
                fall -= Time.deltaTime * 30;
            }
            else if (fall > 25 && fall < 45)
            {
                fall -= Time.deltaTime * 40;
            }
            else if (fall > 15 && fall < 25)
            {
                fall -= Time.deltaTime * 40;
            }
            else if (fall <= 15)
            {
                FallenTree = true;
            }

            transform.rotation = Quaternion.Euler(-Mathf.Clamp(fall, 5, 90), transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }

    private IEnumerator ResetSpeed(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ScriptedMovement>().speed = originalSpeed;
    }

}
