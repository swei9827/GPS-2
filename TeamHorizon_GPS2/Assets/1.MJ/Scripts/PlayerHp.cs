﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerHp : MonoBehaviour {


    public float startHealth = 100f;
    public float startShield = 100f;
    public bool activeShield = true;
    public float health;
    public float shield;

    public Image healthBar;
    public Image shieldBar;
    public Image healthBarCover;
    public Image shieldBarCover;

    private float tempDmg = 0;
    float testDamage;
    public float healthAfterDamage;
    public float shieldAfterDamage;

    // Effect When Player Take Damage
    bool Invulnerable = false;
    public GameObject BulletHolePic;
    float timer;
    public float InvulnerableTime;
    private IEnumerator coroutine;

    public GameObject loseUI;

    // Use this for initialization
    void Start () {
        //healthBar = GetComponent<Image>();
        health = startHealth;
        shield = startShield;
        healthAfterDamage = health;
        shieldAfterDamage = shield;

        //StartCoroutine(coroutine);
	}

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (health != healthAfterDamage || shield != shieldAfterDamage)
        {
            health = Mathf.MoveTowards(health, healthAfterDamage, 20f * Time.deltaTime);
            shield = Mathf.MoveTowards(shield, shieldAfterDamage, 20f * Time.deltaTime);
            shieldBar.fillAmount = shield / startShield;
            healthBar.fillAmount = health / startHealth;
            healthBarCover.fillAmount = health / startHealth;
            shieldBarCover.fillAmount = shield / startShield;



        }
        
        if(health <= 0)
        {
            loseUI.SetActive(true);
        }
        
    }

    public void TakeDamage (float damage)
    {
        if (Invulnerable == false)
        {
            BulletHolePic.SetActive(false);
            if (activeShield == false)
            {
                healthAfterDamage = health - damage;
                //Debug.Log("hehehehe");
            }
            else if (activeShield == true)
            {
                if (damage >= shield)
                {
                    tempDmg = damage - shield;
                    shieldAfterDamage = 0;
                    healthAfterDamage = health - tempDmg;
                    activeShield = false;
                    // Debug.Log("condition 1");
                }
                else
                {
                    shieldAfterDamage = shield - damage;
                    //Debug.Log("condition 2");
                }
            }
        }
        else
        {
            BulletHolePic.SetActive(true);
            StartCoroutine("WaitForSeconds", 2.0f);
        }
    }

    private IEnumerator WaitForSec(float waitTime)
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);
            Invulnerable = false;
            BulletHolePic.SetActive(false);
        }
    }
}


    
