using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour {

    public Material enemyMaterial;
    public int red;
    public int green;
    public int blue;
    public bool flashingIn = true;
    public bool startedFlashing = false;
    public bool damaged = false;
    public WaitForSeconds updateRate =  new WaitForSeconds(0.05f);
    
	void Start () {
        enemyMaterial = this.GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		if(damaged)
        {
            enemyMaterial.color = new Color32((byte)red, (byte)green, (byte)blue, 255);
        }
	}

    IEnumerator Flash()
    {
        while(damaged)
        {
            yield return updateRate;
            if(flashingIn)
            {
                if(red <= 80)
                {
                    flashingIn = false;
                }
                else
                {
                    red -= 25;
                }
            }
            else
            {
                if(red >= 250)
                {
                    flashingIn = true;
                }
                else
                {
                    red += 25;
                }
            }
        }
    }
}
