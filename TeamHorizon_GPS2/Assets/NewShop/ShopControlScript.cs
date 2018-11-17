using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopControlScript : MonoBehaviour {

    int currencyAmount;
    int isItemSold;

    public Text currencyAmountText;
    public Text Upgrade1_price;
    public Text Upgrade2_price;
    public Text Upgrade3_price;
    public Text Health_price;
    public Text Sheild_price;

    public Button buyButton;

	// Use this for initialization
	void Start ()
    {
        currencyAmount = PlayerPrefs.GetInt("CurrencyAmount");
	}
	
	// Update is called once per frame
	void Update ()
    {
        currencyAmountText.text = " Currency: " + currencyAmount.ToString() + "$";

        isItemSold = PlayerPrefs.GetInt("IsItemSold");
        if (currencyAmount >= 500 && isItemSold == 0)
            buyButton.interactable = true;
        else
            buyButton.interactable = false;
	}

    public void Upgrade1()
    {
        currencyAmount -= 500;
        PlayerPrefs.SetInt("IsItemSold", 1);
        Upgrade1_price.text = "SOLD";
        buyButton.gameObject.SetActive(false);
    }

    public void Upgrade2()
    {
        currencyAmount -= 1000;
        PlayerPrefs.SetInt("IsItemSold", 1);
        Upgrade2_price.text = "SOLD";
        buyButton.gameObject.SetActive(false);
    }

    public void Upgrade3()
    {
        currencyAmount -= 1500;
        PlayerPrefs.SetInt("IsItemSold", 1);
        Upgrade3_price.text = "SOLD";
        buyButton.gameObject.SetActive(false);
    }

    public void Health()
    {
        currencyAmount -= 100;
        PlayerPrefs.SetInt("IsItemSold", 1);
        Upgrade2_price.text = "SOLD";
        buyButton.gameObject.SetActive(false);
    }

    public void Sheild()
    {
        currencyAmount -= 200;
        PlayerPrefs.SetInt("IsItemSold", 1);
        Upgrade2_price.text = "SOLD";
        buyButton.gameObject.SetActive(false);
    }

    public void Ready()
    {
        PlayerPrefs.SetInt("CurrencyAmount", currencyAmount);
        SceneManager.LoadScene("ScaledTutorialScene");
    }

    public void resetPlayerPrefs()
    {
        currencyAmount = 0;
        buyButton.gameObject.SetActive(true);
       // Upgrade1_price.text = "Price : $500";
        PlayerPrefs.DeleteAll();
        
    }

}
