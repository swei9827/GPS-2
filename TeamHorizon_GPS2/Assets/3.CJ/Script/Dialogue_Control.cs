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

    Weapon weapon;

    float timer;
    float DelayTime;

    // Dialogue 3 completed boolean
    bool dialogue3Completed = false;

    // Dialogue 4
    bool hitByBulletForFirstTime = false;
    bool Dialogue4Completed = false;

    // Dialogue 5
    bool Dialogue5Completed = false;



    // Use this for initialization
    void Start () {
        cc = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();
        level = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<Level>();
        weapon = GameObject.FindGameObjectWithTag("Player").GetComponent<RaycastShoot>().weapon;
        crouch = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player_Crouch>();
	}
	
	// Update is called once per frame
	void Update () {
        hitByBulletForFirstTime = Enemy_Bullet.hitByBulletForFirstTime;
		if(isPause == true)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }

        if(cc.levelStatus == 1)
        {
            targetProfile = level.targetProfile[0].GetComponent<TargetProfile>();
            timer += Time.deltaTime;
            DelayTime = 2.7f;

            // When Reach the 1st place, if Dialogue 1 still on the screen , deactive it and call Dialogue 2 out (Tap to Shoot on Target)
            if(targetProfile.EnemyCount == 3)
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
            {
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
                }
            }

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
}
