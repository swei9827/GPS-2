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
    private float timeCounter;
    
	void Start () {
        enemyMaterial = this.transform.GetChild(1).GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		if(damaged)
        {
            enemyMaterial.color = new Color32((byte)255, (byte)green, (byte)blue, 255);
        }
	}

    IEnumerator Flash()
    {
        damaged = true;
        while(damaged)
        {
            yield return updateRate;
            timeCounter += 0.05f;
            if(flashingIn)
            {
                if(blue <= 0)
                {
                    flashingIn = false;
                }
                else
                {
                    blue -= 25;
                    green -= 25;
                }
            }
            else
            {
                if(blue >= 80)
                {
                    flashingIn = true;
                }
                else
                {
                    blue += 25;
                    green += 25;
                }
            }

            if (timeCounter >= 1.5f)
            {
                timeCounter = 0;
                damaged = false;
                enemyMaterial.color = new Color32(255, 255, 255, 255);
                yield break;
            }
        }        
    }
}
