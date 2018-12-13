using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour {

    public int swipeDistance;
    Vector3 downPosition;
    Vector3 upPosition;
    ControlCenter cc;

    void Start()
    {
        cc = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();
    }

    void Update()
    {
        if (cc.LevelTutorial)
        {
            TutorialQTE();
        }
        else if (cc.Level1)
        {
            LevelOneQTE();
        }
    }

    void TutorialQTE()
    {
        if (cc.status == STATUS.QTE && cc.levelStatus == 4)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    downPosition = touch.position;
                    upPosition = touch.position;
                   // Debug.Log(downPosition);
                }


                if (touch.phase == TouchPhase.Ended)
                {
                    upPosition = touch.position;
                   // Debug.Log(upPosition);
                }
            }
            if (SwipeDirectionChecker(downPosition, upPosition) > swipeDistance)
            {
                Debug.Log("Swipe Right");
                cc.QTESuccess = true;
            }
            else if (SwipeDirectionChecker(downPosition, upPosition) < -swipeDistance)
            {
                Debug.Log("Swipe Left");
                cc.QTEFail = true;
            }
        }
    }

    void LevelOneQTE()
    {

    }

    float SwipeDirectionChecker(Vector3 p1, Vector3 p2)
    {
        float xDiff = p2.x - p1.x;
        Debug.Log(xDiff);
        return xDiff;
    }
}
