using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFallHazardWithDistanceChecker : MonoBehaviour {

    public float Damage;
    public int health;
    public bool FallenTree;
    public float slowDuration;
    public float slowSpeed;
    public Transform Player;
    public float MaxDistance;
    public bool PlayerHit;
    private Material mat;
    float originalSpeed = 10.0f;
    float fall = 90;
    bool distanceReached;
    ControlCenter cc;
    bool damageDealt;

    void Start()
    {
        mat = this.GetComponent<MeshRenderer>().material;
        cc = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();
    }
    
    void Update()
    {
        float distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= MaxDistance)
        {
            TreeFalling();
        }       

        if (health <= 0)
        {
            if (mat.color.a > 0)
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
            if(cc.Level1 && !damageDealt)
            {
                Player.GetComponent<PlayerHp>().TakeDamage(Damage);
                damageDealt = true;

            }
            Player.GetComponent<ScriptedMovement>().speed = slowSpeed;
            //Player.GetComponent<PlayerHP>().playerHealth -= damage;
            StartCoroutine(ResetSpeed(slowDuration));
        }
    }

    public void TreeV2FallDamage()
    {
        if (!PlayerHit)
        {
            float distance = Vector3.Distance(Player.position, transform.position);
            if (distance <= MaxDistance)
            {
                health -= 1;
            }
        }
    }

    void TreeFalling()
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
            else if (fall < 16)
            {
                FallenTree = true;
                Debug.Log("x");
            }

            transform.rotation = Quaternion.Euler(-Mathf.Clamp(fall, 5, 90), transform.eulerAngles.y,transform.eulerAngles.z);
        }
    }

    private IEnumerator ResetSpeed(float waitTime)
    {
        PlayerHit = true;
        yield return new WaitForSeconds(waitTime);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ScriptedMovement>().speed = originalSpeed;
    }
}
