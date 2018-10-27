using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWobble : MonoBehaviour {

    public bool isMoving;
    public float m_translate;

    private void Start()
    {
        m_translate = 0.0f;
    }

    void Update () {

        m_translate += 0.2f;
        if (isMoving)
        {
            this.transform.Translate(new Vector3(0.0f, Mathf.Cos(m_translate)*0.005f, 0.0f));
        }
	}
}
