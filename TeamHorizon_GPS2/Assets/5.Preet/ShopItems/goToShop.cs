using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToShop : MonoBehaviour {

    public void ShopButton()
    {
        SceneManager.LoadScene("Shop");
    }
}
