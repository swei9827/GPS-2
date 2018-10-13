using System.Collections;
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

    private float tempDmg = 0;
    float testDamage;
    public float healthAfterDamage;
    public float shieldAfterDamage;

    // Use this for initialization
    void Start () {
        //healthBar = GetComponent<Image>();
        health = startHealth;
        shield = startShield;
        healthAfterDamage = health;
        shieldAfterDamage = shield;
	}

    private void Update()
    {
        
        if(health != healthAfterDamage || shield != shieldAfterDamage)
        {
            health = Mathf.MoveTowards(health, healthAfterDamage, 20f * Time.deltaTime);
            shield = Mathf.MoveTowards(shield, shieldAfterDamage, 20f * Time.deltaTime);
            shieldBar.fillAmount = shield / startShield;
            healthBar.fillAmount = health / startHealth;
        }
        
    }

    public void TakeDamage (float damage)
    {
        if (activeShield == false)
        {
            healthAfterDamage = health - damage;
            Debug.Log("hehehehe");
        }
        else if (activeShield == true)
        {
            if (damage >= shield)
            {
                tempDmg = damage - shield;
                shieldAfterDamage = 0;
                healthAfterDamage = health - tempDmg;
                activeShield = false;
                Debug.Log("condition 1");
            }
            else
            {
                shieldAfterDamage = shield - damage;
                Debug.Log("condition 2");
            }
        }
    }
}


    
