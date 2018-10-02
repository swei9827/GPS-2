using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthSystem {

	public override void Death()
    {
        //deathAudio.Play();
    }

    public void Invulnerability()
    {

    }
}

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public virtual void Death() { }

    public void setHP(int hp)
    {
        currentHealth = hp;
    }
    public int getHP()
    {
        return currentHealth;
    }
}
