using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemHolder : MonoBehaviour
{
    public int itemID;
    public Text itemName;
    public Text itemPrice;
    public Image itemImage;
    public GameObject buyButton;
}
