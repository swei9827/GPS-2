using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleQTE : MonoBehaviour {
    public GameObject canvas;
    public ControlCenter cc;
    public bool qte1Cleared;
    public bool qte2Cleared;
    public bool qte3Cleared;

    void Start()
    {
        cc.GetComponent<ControlCenter>();
    }

    void Update()
    {
        if (cc.OnQTE || cc.OnIO)
        {
            canvas.SetActive(true);
            if (cc.QTEFail)
            {
                canvas.SetActive(false);
                cc.OnQTE = false;
            }
            if (qte1Cleared && qte2Cleared && qte3Cleared)
            {
                cc.QTESuccess = true;
                canvas.SetActive(false);                
            }
        }
        else
        {
            canvas.SetActive(false);
            qte1Cleared = false;
            qte2Cleared = false;
            qte3Cleared = false;
        }

    }

    public void ReturnTouch1()
    {
        qte1Cleared = true;
    }

    public void ReturnTouch2()
    {
        qte2Cleared = true;        
    }

    public void ReturnTouch3()
    {
        qte3Cleared = true;
    }  
}
