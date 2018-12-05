using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadyAndGo : MonoBehaviour {

    public static bool ReadySceneFinish = false;
    float timer = 0;
    float delayTime = 1.5f;

	// Use this for initialization
	void Start () {
        ReadySceneFinish = false;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer >= delayTime)
        {
            ReadySceneFinish = true;
        }
		if(ReadySceneFinish == true)
        {
            SceneManager.LoadScene(2);
        }
	}
}
