using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    public static WeaponShop weaponShop;
    public List<Weapon> weaponList = new List<Weapon>();
    public GameObject itemHolderPrefab;
    public Transform grid;


	// Use this for initialization
	void Start ()
    {
		weaponShop= this;
        FullList();

    }
	
	void FullList ()
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            GameObject holder = Instantiate(itemHolderPrefab, grid);
            ItemHolder holderScript = holder.GetComponent<ItemHolder>();

            holderScript.itemName.text = weaponList[i].weaponName;
            holderScript.itemPrice.text = "$" + weaponList[i].weaponPrice.ToString("N2");
            holderScript.itemID = weaponList[i].weaponID;

            if(weaponList[i].bought)
            {
                holderScript.itemImage.sprite = weaponList[i].boughtSprite;
            }
            else
            {
                holderScript.itemImage.sprite = weaponList[i].unboughtSprite;
            }

        }
	}
}

/*[System.Serializable]
public class Weapon
{
    public string weaponName;
    public int weaponID;

    public Sprite unboughtSprite;
    public Sprite boughtSprite;

    public float weaponPrice;
    public bool bought;

}*/

/*[System.Serializable]
public class ItemHolder

{
    public int itemID;
    public Text itemName;
    public Text itemPrice;
    public Image itemImage;
    public GameObject buyButton;
}*/
