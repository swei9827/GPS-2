using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopControlScript : MonoBehaviour {

    int currencyAmount;
    int isItemSold;

    public Text currencyAmountText;
    public Text itemRPGPrice;

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
        if (currencyAmount >= 5 && isItemSold == 0)
            buyButton.interactable = true;
        else
            buyButton.interactable = false;
	}

    public void buyRPG()
    {
        currencyAmount -= 5;
        PlayerPrefs.SetInt("IsRifleSold", 1);
        itemRPGPrice.text = "SOLD";
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
        itemRPGPrice.text = "Price : $500";
        PlayerPrefs.DeleteAll();
        
    }

}
