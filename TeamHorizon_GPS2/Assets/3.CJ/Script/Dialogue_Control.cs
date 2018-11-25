using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Control : MonoBehaviour {

    public List<GameObject> dialogue;
    ControlCenter cc;
    Level level;
    Player_Crouch crouch;
    TargetProfile targetProfile;
    bool isPause = false;

    GameObject tempGameObject;
    GameObject tempGameObject2;

    // Crouch Ui Button
    public GameObject crouchButton;

    // Crouch for 1st time Boolean
    bool crouchFirstTime = false;

    bool hasClick = false;
    bool hasClick2 = false;
    bool hasClick3 = false;
    bool hasClick4 = false;
    bool hasClick5 = false;
    bool hasClick6 = false;

    Weapon weapon;

    float timer;
    float tempTimer;
    float tempTimer2;
    float DelayTime;

    // Dialogue 1 Completed
    bool Dialogue1Completed = false;

    // Dialogue 2 Completed
    bool Dialogue2Completed = false;

    // Dialogue 3 completed boolean
    bool Dialogue3Completed = false;

    // Dialogue 4
    bool hitByBulletForFirstTime = false;
    bool Dialogue4Completed = false;

    // Dialogue 5
    bool Dialogue5Completed = false;

    bool Dialogue6Completed = false;



    // Use this for initialization
    void Start () {
        cc = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();
        level = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<Level>();
       // weapon = GameObject.FindGameObjectWithTag("Player").GetComponent<RaycastShoot>().weapon;
        crouch = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Crouch>();
	}

    // Update is called once per frame
    void Update() {
        if(cc.levelStatus == 1 && Dialogue6Completed == false)
        {
            if(hasClick == false)
            {
                level.SetTimeScale(0.0f);
            }
            else if(hasClick == true)
            {
                DelayTime = 2.7f;
                level.SetTimeScale(1.0f);
                timer += Time.deltaTime;
                Dialogue1Completed = true;
                if(timer >= DelayTime)
                {
                    if (Dialogue1Completed == true && Dialogue2Completed == false)
                    {
                        if (hasClick2 == false)
                        {
                            level.SetTimeScale(0.0f);
                            dialogue[1].SetActive(true);
                        }
                        else if(hasClick2 == true)
                        {
                            level.SetTimeScale(1.0f);
                            Dialogue2Completed = true;
                        }
                    }
                } 
            }
            
            if(Dialogue2Completed == true)
            {
                DelayTime = 3.0f;
                tempTimer += Time.deltaTime;
                if(tempTimer >= DelayTime)
                {
                    if (hasClick3 == false)
                    {
                        level.SetTimeScale(0.0f);
                        dialogue[2].SetActive(true);
                    }
                    else
                    {
                        level.SetTimeScale(1.0f);
                        Dialogue3Completed = true;
                    }
                } 
            }

            if (Dialogue3Completed == true)
            {
                DelayTime = 1.0f;
                tempTimer2 += Time.deltaTime;
                if (tempTimer2 >= DelayTime)
                {
                    if (hasClick4 == false)
                    {
                        level.SetTimeScale(0.0f);
                        dialogue[3].SetActive(true);
                    }
                    else
                    {
                        level.SetTimeScale(1.0f);
                        Dialogue4Completed = true;
                    }
                }
            }

            if(Dialogue4Completed == true)
            {
                if (hasClick5 == false)
                {
                    level.SetTimeScale(0.0f);
                    crouchButton.SetActive(true);
                    dialogue[4].SetActive(true);
                }
                else
                {
                    level.SetTimeScale(1.0f);
                    Dialogue5Completed = true;
                }
            }

            if (Dialogue5Completed == true)
            {
                if (hasClick6 == false)
                {
                    level.SetTimeScale(0.0f);
                    crouchButton.SetActive(true);
                    dialogue[5].SetActive(true);
                }
                else
                {
                    level.SetTimeScale(1.0f);
                    Dialogue6Completed = true;
                }
            }
        }
        if(Dialogue6Completed == true)
        {
            timer = 0;
            tempTimer = 0;
            tempTimer2 = 0;

            hasClick = false;
            hasClick2 = false;
            hasClick3 = false;
            hasClick4 = false;
            hasClick5 = false;
            hasClick6 = false;
        }
        if(cc.levelStatus == 2)
        {
            DelayTime = 1.5f;
            timer += Time.deltaTime;
            if(timer >= DelayTime)
            {
                if(hasClick == false)
                {
                    level.SetTimeScale(0.0f);
                    dialogue[6].SetActive(true);
                }
                else
                {
                    level.SetTimeScale(1.0f);
                }
            }
        }
    }

    public void Pause()
    {
        isPause = true;
    }

    public void Unpause()
    {
        isPause = false;
    }

    public void HasClickTheDialogue()
    {
        if(hasClick == false)
        {
            hasClick = true;
        }
    }

    public void HasClickTheDialogue2()
    {
        if (hasClick2 == false)
        {
            hasClick2 = true;
        }
    }

    public void HasClickTheDialogue3()
    {
        if (hasClick3 == false)
        {
            hasClick3 = true;
        }
    }

    public void HasClickTheDialogue4()
    {
        if (hasClick4 == false)
        {
            hasClick4 = true;
        }
    }

    public void HasClickTheDialogue5()
    {
        if (hasClick5 == false)
        {
            hasClick5 = true;
        }
    }

    public void HasClickTheDialogue6()
    {
        if (hasClick6 == false)
        {
            hasClick6 = true;
        }
    }
}

    /*
       hitByBulletForFirstTime = Enemy_Bullet.hitByBulletForFirstTime;

       if(cc.levelStatus == 1)
       {
           targetProfile = level.targetProfile[0].GetComponent<TargetProfile>();
           timer += Time.deltaTime;
           DelayTime = 2.7f;

           if(targetProfile.EnemyCount == 2)
           {
               if(hasClick == false)
               {
                   if(timer>= DelayTime)
                   {
                       level.SetTimeScale(0f);
                   }
               }
           }
       }

       if(cc.levelStatus == 1)
       {
           targetProfile = level.targetProfile[0].GetComponent<TargetProfile>();
           timer += Time.deltaTime;
           DelayTime = 2.7f;

           // When Reach the 1st place, if Dialogue 1 still on the screen , deactive it and call Dialogue 2 out (Tap to Shoot on Target)
           if(targetProfile.EnemyCount == 2)
           {
               if (hasClick == false)
               {
                   if (timer >= DelayTime)
                   {
                       level.SetTimeScale(0f);
                       tempGameObject2 = dialogue[0].gameObject;
                       tempGameObject = dialogue[1].gameObject;
                       tempGameObject2.SetActive(false);
                       tempGameObject.SetActive(true);
                   }
               }
               else if (hasClick == true)
               {
                   level.SetTimeScale(1.0f);
                   // Start OnBattle When Reach Level Status 1
                   cc.status = STATUS.BATTLE; 
               }
           }

           // After killing one enemy, Dialogue 3 Come out ( Teach Reload, After reload dialogue missing)
           if (targetProfile.EnemyCount == 2 && dialogue3Completed == false)
           {/*
               if (weapon.reloading == false)
               {
                   level.SetTimeScale(0f);
                   tempGameObject = dialogue[2].gameObject;
                   tempGameObject.SetActive(true);
               }
               else
               {
                   tempGameObject.SetActive(false);
                   level.SetTimeScale(1.0f);
                   dialogue3Completed = true;
               }*/


/*
        // Hit by bullet for 1st time, Dialogue 4 Come out (Be aware of HP Dialogue)
        if (hitByBulletForFirstTime == true && dialogue3Completed == true)
        {
            if (hasClick == false)
            {
                level.SetTimeScale(0f);
                tempGameObject = dialogue[3].gameObject;
                tempGameObject.SetActive(true);
            }
            else
            {
                level.SetTimeScale(1.0f);
                Enemy_Bullet.hitByBulletForFirstTime = false;
                Dialogue4Completed = true;
            }
        }

        // Dialogue 5, after B1-C, unhide Crouch UI here (Hold Crouch to hide from most of the projectile)
        if(Dialogue4Completed == true)
        {
            crouchButton.SetActive(true);
            if(hasClick == false)
            {
                tempGameObject = dialogue[4].gameObject;
                tempGameObject.SetActive(true);
                level.SetTimeScale(0.0f);
            }
            else
            {
                level.SetTimeScale(1.0f);
                Dialogue5Completed = true;
            }
        }


        // Dialogue 6, When player crouch for 1st time notice them (Timer will go 0, watch out the time)
        if (crouchFirstTime == false && Dialogue5Completed == true)
        {
            if(crouch.isCrouch == true)
            {
                crouchFirstTime = true;
            }
        }
        else if(crouchFirstTime == true && Dialogue5Completed == true)
        {
            if(hasClick == false)
            {
                tempGameObject = dialogue[5].gameObject;
                tempGameObject.SetActive(true);
                level.SetTimeScale(0.0f);
            }
            else
            {
                level.SetTimeScale(1.0f);
            }
        }
    }
}
*/
