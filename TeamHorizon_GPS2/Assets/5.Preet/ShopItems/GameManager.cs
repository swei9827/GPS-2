using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] public float currency;
    public Text currencyText;


	// Use this for initialization
	void Start ()
    {
        gameManager = this;
        UpdateUI();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddCurrency (float amount)
    {

        currency += amount;
        UpdateUI();
    }

    public void ReduceCurrency (float amount)
    {
        currency -= amount;
        UpdateUI();
    }

    public bool RequestCurrency(float amount)
    {
        if (amount <= currency)
        {
            return true;
        }
        return false;
    }

    void UpdateUI()
    {
        currencyText.text = "$" + currency.ToString("N2");
    }

}
/*
public class TouchToShoot : MonoBehaviour
{

    public Material hitMaterial;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                var rig = hitInfo.collider.GetComponent<Rigidbody>();
                if (rig != null)
                {
                    rig.GetComponent<MeshRenderer>().material = hitMaterial;
                    rig.AddForceAtPosition(ray.direction * 50f, hitInfo.point, ForceMode.VelocityChange);
                }
            }
        }

    }
}*/