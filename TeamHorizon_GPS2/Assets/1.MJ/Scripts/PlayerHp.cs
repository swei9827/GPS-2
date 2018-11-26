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

    // Effect When Player Take Damage
    bool Invulnerable = false;
    public GameObject BulletHolePic;
    float timer;
    public float InvulnerableTime = 2.0f;
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
        if (timer >= InvulnerableTime)
        {
            BulletHolePic.SetActive(false);
            timer = 0;
        }

        if (health != healthAfterDamage || shield != shieldAfterDamage)
        {
            health = Mathf.MoveTowards(health, healthAfterDamage, 20f * Time.deltaTime);
            shield = Mathf.MoveTowards(shield, shieldAfterDamage, 20f * Time.deltaTime);
            shieldBar.fillAmount = shield / startShield;
            healthBar.fillAmount = health / startHealth;
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
            BulletHolePic.SetActive(true);
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


    
