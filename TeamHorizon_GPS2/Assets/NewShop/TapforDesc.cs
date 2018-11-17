using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapforDesc : MonoBehaviour
{
    public Text textShowed = null;
    public void ChangeWord (string word)
    {
        textShowed.text = word;
    }

	// Use this for initialization
	void Start ()
    {


	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
