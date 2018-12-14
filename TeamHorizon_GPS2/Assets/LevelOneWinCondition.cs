using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneWinCondition : MonoBehaviour {

    int playerArea = 0;
    private ControlCenter CC;
    public GameObject ComingSoonShopImage;

    // Use this for initialization
    void Start () {
        CC = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();
    }
	
	// Update is called once per frame
	void Update () {
        playerArea = CC.levelStatus;
		if(playerArea >= 37)
        {
            Time.timeScale = 0.0f;
            ComingSoonShopImage.SetActive(true);
        }
	}
}
