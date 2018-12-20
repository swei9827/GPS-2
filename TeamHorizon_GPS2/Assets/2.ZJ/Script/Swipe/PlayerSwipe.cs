using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour {

    public int swipeDistance;
    Vector3 downPosition;
    Vector3 upPosition;
    ControlCenter cc;
    float xDiff;

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

        if (cc.status == STATUS.INTERACTABLE && cc.levelStatus == 4)
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
                cc.InteractSuccess = true;
            }
            else if (SwipeDirectionChecker(downPosition, upPosition) < -swipeDistance)
            {
                Debug.Log("Swipe Left");
                cc.InteractFail = true;
            }
        }
    }

    void LevelOneQTE()
    {
        if(cc.status == STATUS.INTERACTABLE && cc.levelStatus == 5)
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
                downPosition = new Vector3(0, 0, 0);
                upPosition = new Vector3(0, 0, 0);
                cc.GoRight = true;
            }
            else if (SwipeDirectionChecker(downPosition, upPosition) < -swipeDistance)
            {
                Debug.Log("Swipe Left");
                downPosition = new Vector3(0, 0, 0);
                upPosition = new Vector3(0, 0, 0);
                cc.GoLeft = true;
            }
        }

        if (cc.status == STATUS.QTE && cc.levelStatus == 9)
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
                downPosition = new Vector3(0, 0, 0);
                upPosition = new Vector3(0, 0, 0);
                cc.QTESuccess = true;
            }
            else if (SwipeDirectionChecker(downPosition, upPosition) < -swipeDistance)
            {
                Debug.Log("Swipe Left");
                downPosition = new Vector3(0, 0, 0);
                upPosition = new Vector3(0, 0, 0);
                StartCoroutine(delay(2.0f, 1));
            }
        }

        if (cc.status == STATUS.QTE && cc.levelStatus == 17)
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
                downPosition = new Vector3(0, 0, 0);
                upPosition = new Vector3(0, 0, 0);
                cc.QTEFail = true;
            }
            else if (SwipeDirectionChecker(downPosition, upPosition) < -swipeDistance)
            {
                Debug.Log("Swipe Left");
                downPosition = new Vector3(0, 0, 0);
                upPosition = new Vector3(0, 0, 0);
                cc.QTESuccess = true;
            }
        }

        if (cc.status == STATUS.QTE && cc.levelStatus == 25)
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
                downPosition = new Vector3(0, 0, 0);
                upPosition = new Vector3(0, 0, 0);
                cc.QTESuccess = true;
            }
            else if (SwipeDirectionChecker(downPosition, upPosition) < -swipeDistance)
            {
                Debug.Log("Swipe Left");
                downPosition = new Vector3(0, 0, 0);
                upPosition = new Vector3(0, 0, 0);
                StartCoroutine(delay(2.0f, 1));
            }
        }

        if (cc.status == STATUS.QTE && cc.levelStatus == 30)
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
                downPosition = new Vector3(0, 0, 0);
                upPosition = new Vector3(0, 0, 0);
                cc.QTESuccess = true;
            }
            else if (SwipeDirectionChecker(downPosition, upPosition) < -swipeDistance)
            {
                Debug.Log("Swipe Left");
                downPosition = new Vector3(0, 0, 0);
                upPosition = new Vector3(0, 0, 0);
                StartCoroutine(delay(2.0f, 1));
            }
        }

        if (cc.status == STATUS.QTE && cc.levelStatus == 33)
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
                downPosition = new Vector3(0, 0, 0);
                upPosition = new Vector3(0, 0, 0);
                StartCoroutine(delay(2.0f, 0));

            }
            else if (SwipeDirectionChecker(downPosition, upPosition) < -swipeDistance)
            {
                Debug.Log("Swipe Left");
                downPosition = new Vector3(0, 0, 0);
                upPosition = new Vector3(0, 0, 0);
                cc.QTESuccess = true;
            }
        }

        if (cc.status == STATUS.QTE && cc.levelStatus == 36)
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
                downPosition = new Vector3(0, 0, 0);
                upPosition = new Vector3(0, 0, 0);
                cc.QTESuccess = true;
            }
            else if (SwipeDirectionChecker(downPosition, upPosition) < -swipeDistance)
            {
                Debug.Log("Swipe Left");
                downPosition = new Vector3(0, 0, 0);
                upPosition = new Vector3(0, 0, 0);
                StartCoroutine(delay(2.0f, 1));
            }
        }
    }

    float SwipeDirectionChecker(Vector3 p1, Vector3 p2)
    {
        xDiff = 0;
        xDiff = p2.x - p1.x;
        Debug.Log(xDiff);
        return xDiff;
    }

    IEnumerator delay(float waitTime, int boolID)
    {
        yield return new WaitForSeconds(waitTime);
        cc.QTEFail = true;
    }
}
