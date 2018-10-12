using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerHp : MonoBehaviour {


    public float startHealth = 100f;
    public float startShield = 100f;
    public bool activeShield = false;
    private float health;
    private float shield;

    public Image healthBar;
    public Image shieldBar;

    private float tempDmg;

    // Use this for initialization
    void Start () {
        //healthBar = GetComponent<Image>();
        health = startHealth;
        shield = startShield;
	}
	
	// Update is called once per frame
	void TakeDamage (float damage) {

        if(activeShield == false)
        {
            health -= 50f;
            healthBar.fillAmount = health / startHealth;
            Debug.Log("hehehehe");

        }

        else if(activeShield == true)
        {
            shieldBar.fillAmount = shield / startShield;

            if (damage >= shield)
            {
                tempDmg = damage - shield;
                health -= tempDmg;
                healthBar.fillAmount = health / startHealth;
            }
            
        }

        

        if (healthBar.fillAmount <= 0.5f)
        {
            Debug.Log("hehehehe");
            //Die();
            //deathAudio.Play();

        }
    }
}


    
