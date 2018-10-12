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
            shield -= 50f;
            shieldBar.fillAmount = shield / startShield;
        }

        

        if (healthBar.fillAmount <= 0.5f)
        {
            Debug.Log("hehehehe");
            //Die();
            //deathAudio.Play();

        }
    }
}
