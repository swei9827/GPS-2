using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerHp : MonoBehaviour {

    public static PlayerHp instance;
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
    float ClipSize, Damage, ReloadTime;
    public float healthAfterDamage;
    public float shieldAfterDamage;

    // Effect When Player Take Damage
    bool Invulnerable = false;
    public GameObject BulletHolePic;
    float timer;
    public float InvulnerableTime;
    AudioManager audioM;

    void Awake()
    {
        instance = this;
    }

    public GameObject loseUI;

    void Start () {
        //healthBar = GetComponent<Image>();
        health = startHealth;
        shield = startShield;
        healthAfterDamage = health;
        shieldAfterDamage = shield;
        audioM = FindObjectOfType<AudioManager>();
        //StartCoroutine(coroutine);
    }

    private void Update()
    {
        timer += Time.deltaTime;
      /*  if (timer >= InvulnerableTime)
        {
            BulletHolePic.SetActive(false);
            timer = 0;
        }*/

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
            BulletHolePic.SetActive(true);
            audioM.Play("PLAYER HURT");
            if (activeShield == false)
            {
                healthAfterDamage = health - damage;
            }
            else if (activeShield == true)
            {
                if (damage >= shield)
                {
                    tempDmg = damage - shield;
                    shieldAfterDamage = 0;
                    healthAfterDamage = health - tempDmg;
                    activeShield = false;
                }
                else
                {
                    shieldAfterDamage = shield - damage;
                }
            }
            StartCoroutine("WaitForSec", 2.0f);
        }
        else
        {
            BulletHolePic.SetActive(true);
            StartCoroutine("WaitForSec", 2.0f);
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


    
