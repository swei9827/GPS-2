using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToShopControlScript : MonoBehaviour
{
    public Text currencyText;
    public static int currencyAmount;
    int isRPGSold;
    public GameObject RPG;

	// Use this for initialization
	void Start ()
    {
        currencyAmount = PlayerPrefs.GetInt("CurrencyAmount");
        isRPGSold = PlayerPrefs.GetInt("IsRPGSold");

        if (isRPGSold == 1)
            RPG.SetActive(true);
        else
            RPG.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        currencyText.text = "Currency : " + currencyAmount.ToString() + "$";
	}

    public void goToShop()
    {
        PlayerPrefs.SetInt("CurrencyAmount", currencyAmount);
        SceneManager.LoadScene("NewShop");
    }
}
