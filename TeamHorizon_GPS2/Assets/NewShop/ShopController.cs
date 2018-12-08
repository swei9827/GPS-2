using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    public static ShopController instance;

    /* playerArea = CC.levelStatus;
         if (this.hp <= 0)
         {
         tp.EnemyCount -= 1;

         Destroy(this.gameObject);
         lvlController.GetComponent<Level>().setScore(lvlController.GetComponent<Level>().getScore() + 1000); //GETSCORE
     }*/

    public GameObject ShopPanel;
    int ClickCount;
    GameObject clipSize;// = GameObject.Find("clipSize");
    //GameObject child = ShopController.clipSize.transform.GetChild(0).gameObject;

    int scoreAmount;
    int isItemSold1;
    int isItemSold2;
    int isItemSold3;
    int isItemSold4;
    int isItemSold5;



    public Text scoreAmountText;
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

    public Image healthBar, shieldBar;
    public Text txtScore;

    public int HealthValue, ShieldValue, ClipVale, DamageValue, ReloadTimeValue;
    internal int ClipPurchaseTime = 0, DamagePurchaseTime = 0, ReloadTimePurchaseTime = 0;

    public GameObject[] ImgClipSequence, ImgDamageSequence, ImgReloadTimeSequence;
    private void Awake()
    {
        instance = this;
        clipSize = GameObject.Find("clipSize");
    }


    // Use this for initialization
    void Start()
    {
        if (ShopPanel != null)
        {
            ShopPanel.SetActive(false);
        }
        if (PlayerPrefs.HasKey("Score"))
        {
            scoreAmount = PlayerPrefs.GetInt("Score");
            Constants.LevelScore = scoreAmount;
        }

        for (int i = 0; i < ImgClipSequence.Length; i++)
        {
            ImgClipSequence[i].SetActive(false);
        }
        for (int i = 0; i < ImgDamageSequence.Length; i++)
        {
            ImgDamageSequence[i].SetActive(false);
        }
        for (int i = 0; i < ImgReloadTimeSequence.Length; i++)
        {
            ImgReloadTimeSequence[i].SetActive(false);
        }
    }
    public void OnOpenShopPanel()
    {
        //		print (PlayerHp.instance.healthBar.fillAmount);
        //		print (PlayerHp.instance.shieldBar.fillAmount);

        healthBar.fillAmount = PlayerHp.instance.healthBar.fillAmount;
        shieldBar.fillAmount = PlayerHp.instance.shieldBar.fillAmount;
        scoreAmount = Constants.TotalScore;
        txtScore.text = scoreAmount.ToString();
        Time.timeScale = 0f;
        PauseMenu.GameIsPaused = true;
        Constants.GameIsPaused = true;
        ShopPanel.SetActive(true);
        ShopPanel.transform.SetAsLastSibling();
    }
    public void OnReadyButtonClick()
    {
        PlayerPrefs.SetInt("Score", scoreAmount);
        //		SceneManager.LoadScene("ScaledTutorialScene");

        Level.instance.score = scoreAmount;
        PlayerHp.instance.healthBar.fillAmount = healthBar.fillAmount;
        PlayerHp.instance.shieldBar.fillAmount = shieldBar.fillAmount;
        PlayerHp.instance.healthBarCover.fillAmount = healthBar.fillAmount;
        PlayerHp.instance.shieldBarCover.fillAmount = shieldBar.fillAmount;
        PlayerHp.instance.healthAfterDamage = healthBar.fillAmount * 100;
        PlayerHp.instance.shieldAfterDamage = shieldBar.fillAmount * 100;

        ShopPanel.SetActive(false);
        PauseMenu.GameIsPaused = false;
        Constants.GameIsPaused = false;
        Time.timeScale = 1f;
    }



    //newshop2

    void Update()
    {
        if (ShopPanel != null)
        {
            isItemSold1 = PlayerPrefs.GetInt("IsItemSold1");
            if (scoreAmount >= ClipVale && isItemSold1 == 0 && ClipPurchaseTime < 5)
                buyButton1.interactable = true;
            else
                buyButton1.interactable = false;

            isItemSold2 = PlayerPrefs.GetInt("IsItemSold2");
            if (scoreAmount >= DamageValue && isItemSold2 == 0 && RaycastShoot.instance.weapon.damage < 6f)
                buyButton2.interactable = true;
            else
                buyButton2.interactable = false;

            isItemSold3 = PlayerPrefs.GetInt("IsItemSold3");
            if (scoreAmount >= ReloadTimeValue && isItemSold3 == 0 && RaycastShoot.instance.weapon.reloadTime > 0.5f)
                buyButton3.interactable = true;
            else
                buyButton3.interactable = false;


            //		isItemSold4 = PlayerPrefs.GetInt("IsItemSold4");
            if (scoreAmount >= HealthValue && healthBar.fillAmount < 1f)
                buyButton4.interactable = true;
            else
                buyButton4.interactable = false;

            //		isItemSold5 = PlayerPrefs.GetInt("IsItemSold5");
            if (scoreAmount >= ShieldValue && shieldBar.fillAmount < 1f)
                buyButton5.interactable = true;
            else
                buyButton5.interactable = false;


        }
        txtScore.text = scoreAmount.ToString();
    }


    public void OnHealthButtonClick()
    {
        scoreAmount -= HealthValue;
        PlayerPrefs.SetInt("Score", scoreAmount);
        healthBar.fillAmount += 0.25f;

    }

    public void OnShieldButtonClick()
    {
        scoreAmount -= ShieldValue;
        PlayerPrefs.SetInt("Score", scoreAmount);
        shieldBar.fillAmount += 0.25f;
    }

    public void OnClipSizePurchaseButtonClick()
    {
        if (ClipPurchaseTime < 5)
        {
            scoreAmount -= ClipVale;
            PlayerPrefs.SetInt("Score", scoreAmount);
            RaycastShoot.instance.currentAmmo += 5;

            ClipPurchaseTime++;
        }
        if (ClipPurchaseTime > 5)
        {
            ClipPurchaseTime = 5;
        }
        for (int i = 0; i < ClipPurchaseTime; i++)
        {
            ImgClipSequence[i].SetActive(true);
        }
    }
    public void OnDamagePurchaseButtonClick()
    {
        scoreAmount -= DamageValue;
        PlayerPrefs.SetInt("Score", scoreAmount);
        if (RaycastShoot.instance.weapon.damage < 6f)
        {
            RaycastShoot.instance.weapon.damage += 1;
            DamagePurchaseTime++;
        }
        if (DamagePurchaseTime > 5)
        {
            DamagePurchaseTime = 5;
        }

        for (int i = 0; i < DamagePurchaseTime; i++)
        {
            ImgDamageSequence[i].SetActive(true);
        }
    }
    public void OnReloadTimePurchaseButtonClick()
    {
        scoreAmount -= ReloadTimeValue;
        PlayerPrefs.SetInt("Score", scoreAmount);
        if (RaycastShoot.instance.weapon.reloadTime > 0.5f)
        {
            RaycastShoot.instance.weapon.reloadTime -= 0.2f;
            ReloadTimePurchaseTime++;
        }
        else
        {
            RaycastShoot.instance.weapon.reloadTime -= 0.2f;
            ReloadTimePurchaseTime++;
        }
        if (ReloadTimePurchaseTime > 5)
        {
            ReloadTimePurchaseTime = 5;
        }
        for (int i = 0; i < ReloadTimePurchaseTime; i++)
        {
            ImgReloadTimeSequence[i].SetActive(true);
        }
    }

    public void resetPlayerPrefs()
    {
        scoreAmount = 3000;
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
