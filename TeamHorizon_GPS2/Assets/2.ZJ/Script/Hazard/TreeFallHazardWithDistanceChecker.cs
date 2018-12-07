﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFallHazardWithDistanceChecker : MonoBehaviour {
    
    public int health;
    public bool FallenTree;
    public float slowDuration;
    public float slowSpeed;
    public float damage;
    public Transform Player;
    public float MaxDistance;
    private Material mat;
    float originalSpeed = 10.0f;
    float fall = 90;
    bool distanceReached;

    void Start()
    {
        mat = this.GetComponent<MeshRenderer>().material;        
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
            Player.GetComponent<ScriptedMovement>().speed = slowSpeed;
            //Player.GetComponent<PlayerHP>().playerHealth -= damage;
            StartCoroutine(ResetSpeed(slowDuration));
        }
    }

    public void TreeV2FallDamage()
    {
        float distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= MaxDistance)
        {
            health -= 1;
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
        yield return new WaitForSeconds(waitTime);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ScriptedMovement>().speed = originalSpeed;
    }
}
