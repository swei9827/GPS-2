using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour {

    public int hp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(this.hp <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnMouseDown()
    {
        hp -= 1;
        Debug.Log(hp);
    }
}
