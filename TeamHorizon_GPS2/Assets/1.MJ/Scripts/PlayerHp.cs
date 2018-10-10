using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerHp : MonoBehaviour {


    public float startHealth = 100;
    private float health;

    public Image healthBar;

    // Use this for initialization
    void Start () {
        //healthBar = GetComponent<Image>();
        health = startHealth;
	}
	
	// Update is called once per frame
	void TakeDamage (float damage) {

        health -= damage;
        healthBar.fillAmount = health / startHealth;

        if (health < 0)
        {
            //Die();
            //deathAudio.Play();

        }
    }
}
