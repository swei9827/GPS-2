using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToShopControlScript : MonoBehaviour
{
    public ControlCenter cc;

    public Text currencyText;
    public static int currencyAmount;
    int isUpgrade1Sold;
    int isUpgrade2Sold;
    int isUpgrade3Sold;
    int isHealthSold;
    int isSheildSold;

    public GameObject Upgrade1;
    public GameObject Upgrade2;
    public GameObject Upgrade3;
    public GameObject Health;
    public GameObject Sheild;

	// Use this for initialization
	void Start ()
    {
        cc = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();

        currencyAmount = PlayerPrefs.GetInt("CurrencyAmount");
        isUpgrade1Sold = PlayerPrefs.GetInt("isUpgrade1Sold");
        isUpgrade2Sold = PlayerPrefs.GetInt("isUpgrade2Sold");
        isUpgrade3Sold = PlayerPrefs.GetInt("isUpgrade3Sold");
        isHealthSold = PlayerPrefs.GetInt("isHealthSold");
        isSheildSold = PlayerPrefs.GetInt("isSheildSold");

        if (isUpgrade1Sold == 1)
            Upgrade1.SetActive(true);
        else
            Upgrade1.SetActive(false);

        if (isUpgrade2Sold == 1)
            Upgrade2.SetActive(true);
        else
            Upgrade2.SetActive(false);

        if (isUpgrade3Sold == 1)
            Upgrade3.SetActive(true);
        else
            Upgrade3.SetActive(false);

        if (isHealthSold == 1)
            Health.SetActive(true);
        else
            Health.SetActive(false);

        if (isSheildSold == 1)
            Sheild.SetActive(true);
        else
            Sheild.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        currencyText.text = "Currency : " + currencyAmount.ToString() + "$";
	}

    public void goToShop()
    {
        if (cc.winUI ==true)
        {
            PlayerPrefs.SetInt("CurrencyAmount", currencyAmount);
            SceneManager.LoadScene("NewShop");
        }

    }
}
