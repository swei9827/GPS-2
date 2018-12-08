using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopControlScript : MonoBehaviour {

    int currencyAmount;
    int isItemSold1;
    int isItemSold2;
    int isItemSold3;
    int isItemSold4;
    int isItemSold5;

    public Text currencyAmountText;
    public Text Upgrade1_price;
    public Text Upgrade2_price;
    public Text Upgrade3_price;
    public Text Health_price;
    public Text Sheild_price;

    public Button buyButton1;
    public Button buyButton2;
    public Button buyButton3;
    public Button buyButton4;
    public Button buyButton5;
    public Button buyButton6;

	// Use this for initialization
	void Start ()
    {
        currencyAmount = PlayerPrefs.GetInt("CurrencyAmount");
	}
	
	// Update is called once per frame
	void Update ()
    {
        currencyAmountText.text = " Currency: " + currencyAmount.ToString() + "$";

       isItemSold1 = PlayerPrefs.GetInt("IsItemSold1");
        if (currencyAmount >= 500 && isItemSold1 == 0)
            buyButton1.interactable = true;
        else
            buyButton1.interactable = false;

        isItemSold2 = PlayerPrefs.GetInt("IsItemSold2");
        if (currencyAmount >= 1000 && isItemSold2 == 0)
            buyButton2.interactable = true;
        else
            buyButton2.interactable = false;

        isItemSold3 = PlayerPrefs.GetInt("IsItemSold3");
        if (currencyAmount >= 1500 && isItemSold3 == 0)
            buyButton3.interactable = true;
        else
            buyButton3.interactable = false;


        isItemSold4 = PlayerPrefs.GetInt("IsItemSold4");
        if (currencyAmount >= 100 && isItemSold3 == 0)
            buyButton3.interactable = true;
        else
            buyButton3.interactable = false;

        isItemSold5 = PlayerPrefs.GetInt("IsItemSold5");
        if (currencyAmount >= 200 && isItemSold3 == 0)
            buyButton3.interactable = true;
        else
            buyButton3.interactable = false;

        if (currencyAmount <=0)
        {
            buyButton1.interactable = false;
            buyButton2.interactable = false;
            buyButton3.interactable = false;
            buyButton4.interactable = false;
            buyButton5.interactable = false;
           // buyButton6.interactable = false;
        }

        if (currencyAmount <500)
        {
            buyButton1.interactable = false;
            buyButton2.interactable = false;
            buyButton3.interactable = false;
            buyButton4.interactable = false;
            buyButton5.interactable = false;
            // buyButton6.interactable = false;
        }

    }

    public void Upgrade1()
    {
        currencyAmount -= 500;
        PlayerPrefs.SetInt("IsItemSold1", 1);
        Upgrade1_price.text = "SOLD";
        buyButton1.gameObject.SetActive(false);

    }

    public void Upgrade2()
    {
        currencyAmount -= 1000;
        PlayerPrefs.SetInt("IsItemSold2", 1);
        Upgrade2_price.text = "SOLD";
        buyButton2.gameObject.SetActive(false);

    }

    public void Upgrade3()
    {
        currencyAmount -= 1500;
        PlayerPrefs.SetInt("IsItemSold3", 1);
        Upgrade3_price.text = "SOLD";
        buyButton3.gameObject.SetActive(false);
    }

    public void Health()
    {
        currencyAmount -= 100;
        PlayerPrefs.SetInt("IsItemSold4", 1);
        Health_price.text = "SOLD";
        buyButton4.gameObject.SetActive(false);
    }

    public void Sheild()
    {
        currencyAmount -= 200;
        PlayerPrefs.SetInt("IsItemSold5", 1);
        Sheild_price.text = "SOLD";
        buyButton5.gameObject.SetActive(false);
        
    }

    public void Ready()
    {
        PlayerPrefs.SetInt("CurrencyAmount", currencyAmount);
        SceneManager.LoadScene("ScaledTutorialScene");
    }

    public void resetPlayerPrefs()
    {
        currencyAmount = 3000;
        buyButton1.gameObject.SetActive(true);
        buyButton2.gameObject.SetActive(true);
        buyButton3.gameObject.SetActive(true);
        buyButton4.gameObject.SetActive(true);
        buyButton5.gameObject.SetActive(true);

        buyButton1.interactable = true;
        buyButton2.interactable = true;
        buyButton3.interactable = true;
        buyButton4.interactable = true;
        buyButton5.interactable = true;
        // buyButton6.gameObject.SetActive(true);
        // Upgrade1_price.text = "Price : $500";
        PlayerPrefs.DeleteAll();
        
    }

}
