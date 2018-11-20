using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Level lvlController;
    public TargetProfile tp;

    public bool move;

    public float moveSpeed;
    private float tempX;
    private float tempY;

    private Rigidbody rb;
    private Vector3 endPosition;
    private Vector3 startPosition;

    public float endPosX;
    public float endPosY;
    public float endPosZ;
    private ControlCenter CC;

    // When Reaching CheckPoint
    public Transform player;
    public bool EnemyStartAppear = false;
    public bool ThrowBarrel = false;
    public bool ThrowGranade = false;
    public bool StabSlash = false;
    public bool Running = false;

    public int EnemyArea = 0;
    int playerArea = 0;

    [SerializeField]
    public static bool StartShooting = false;

    public float timer = 0;
    public int hideDelayTimer;

    public bool enemyHide = false;
    public int hp;

    // Animation
    public Animator animator;
    public bool SlothWolf_DamageAnim;
    public bool SlothWolf_AimingAnim;
    public bool SlothWolf_ShootingAnim;

    public bool Bison_DamageAnim;
    public bool Bison_AimingAnim;
    public bool Bison_ShootingAnim;
    public bool Bison_DeadAnim;
    public bool Bison_ThrowBarrelAnim;

    public bool Soldier_DamageAnim;
    public bool Soldier_AimingAnim;
    public bool Soldier_ShootingAnim;
    public bool Soldier_DeadAnim;
    public bool Soldier_StabSlashAnim;
    public bool Soldier_RunningAnim;
    public bool Soldier_GranadeAnim;

    public List<TargetProfile> targetProfile;

    public static bool EnemyHPreducing = false;

    void Start()
    {
        tempX = this.transform.position.x;
        tempY = this.transform.position.y;
        animator = this.GetComponent<Animator>();
        startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rb = GetComponent<Rigidbody>();
        CC = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<ControlCenter>();
        lvlController = GameObject.FindGameObjectWithTag("ControlCenter").GetComponent<Level>();

        //animator.enabled = false;
        //SLOTH
        SlothWolf_DamageAnim = false;
        SlothWolf_AimingAnim = false;
        SlothWolf_ShootingAnim = false;

        //BISON
        Bison_DamageAnim = false;
        Bison_AimingAnim = false;
        Bison_ShootingAnim = false;
        Bison_DeadAnim = false;
        Bison_ThrowBarrelAnim = false;

        //SOLDIER
        Soldier_DamageAnim = false;
        Soldier_AimingAnim = false;
        Soldier_ShootingAnim = false;
        Soldier_DeadAnim = false;
        Soldier_StabSlashAnim = false;
        Soldier_RunningAnim = false;
        Soldier_GranadeAnim = false;
    }

    public void DamageCalculation(int damage, int damagePart)
    {
        if(damagePart == 1)
        {
            hp -= damage * 2;
        }
        else if(damagePart == 2)
        {
            hp -= damage;
        }
        else if(damagePart == 3)
        {
            hp -= damage / 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerArea = CC.levelStatus;
        if (this.hp <= 0)
        {
            tp.EnemyCount -= 1;

            Destroy(this.gameObject);
            lvlController.GetComponent<Level>().setScore(lvlController.GetComponent<Level>().getScore()+1000);
        }

        if (EnemyArea == playerArea )
        {
            EnemyStartAppear = true;  
        }

        if(move == true && EnemyStartAppear == true)
        {
            endPosition = new Vector3(endPosX, endPosY, endPosZ);
            if (transform.position != endPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
            }
        }

        if (transform.position == endPosition)
        {
            move = false;
            EnemyStartAppear = false;
            StartShooting = true;

            timer += Time.deltaTime;
            //Debug.Log(timer);
        }

        if (timer >= hideDelayTimer)
        {
            StartShooting = false;
            enemyHide = true;
            if (enemyHide == true && StartShooting == false)
            {
                //back last position;
                if (transform.position != startPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
                }
            }
        }

        if(enemyHide == true && StartShooting == false)
        {
            if (transform.position == startPosition && enemyHide == true)
            {
                timer = 0;
                enemyHide = false;
                EnemyStartAppear = true;
                move = true;
            }
        }

        //Enemy1 Sloth Animation ========================================================= Enemy1 Sloth Animation =========================================================
        //if (Input.GetKeyDown(KeyCode.E))    //ANIM1
        if (EnemyHPreducing == true) //Damage Anim
        {
            animator.enabled = true;
            SlothWolf_DamageAnim = true;
            SlothWolf_AimingAnim = false;
            SlothWolf_ShootingAnim = false;
        }

        // if (Input.GetKeyDown(KeyCode.R))//ANIM2 
        if (EnemyStartAppear == true) //Aimaing Anim
        {
            animator.enabled = true;
            SlothWolf_AimingAnim = true;
            SlothWolf_DamageAnim = false;
            SlothWolf_ShootingAnim = false;
        }

        //if (Input.GetKeyDown(KeyCode.T)) //ANIM3
        if (StartShooting == true) //Shoot Anim
        {
            animator.enabled = true;
            SlothWolf_ShootingAnim = true;
            SlothWolf_DamageAnim = false;
            SlothWolf_AimingAnim = false;
        }


        //----------------------------------------------------
        if (SlothWolf_DamageAnim == true)
        {
            animator.SetBool("SlothWolf_DamageAnim", true);
            animator.SetBool("SlothWolf_AimingAnim", false);
            animator.SetBool("SlothWolf_ShootingAnim", false);
        }

        if (SlothWolf_AimingAnim == true)
        {
            animator.SetBool("SlothWolf_AimingAnim", true);
            animator.SetBool("SlothWolf_DamageAnim", false);
            animator.SetBool("SlothWolf_ShootingAnim", false);
        }

        if (SlothWolf_ShootingAnim == true)
        {
            animator.SetBool("SlothWolf_ShootingAnim", true);
            animator.SetBool("SlothWolf_AimingAnim", false);
            animator.SetBool("SlothWolf_DamageAnim", false);
        }

        //Enemy2 Bison Animation ========================================================= Enemy2 Bison Animation =========================================================

        //if (Input.GetKeyDown(KeyCode.E))    //ANIM2
        if (EnemyHPreducing == true) //Damage Anim
        {
            animator.enabled = true;
            Bison_DamageAnim = true;
            Bison_AimingAnim = false;
            Bison_ShootingAnim = false;
            Bison_DeadAnim = false;
            Bison_ThrowBarrelAnim = false;
        }

        // if (Input.GetKeyDown(KeyCode.R))//ANIM2 
        if (EnemyStartAppear == true) //Aimaing Anim
        {
            animator.enabled = true;
            Bison_AimingAnim = true;
            Bison_DamageAnim = false;
            Bison_ShootingAnim = false;
            Bison_DeadAnim = false;
            Bison_ThrowBarrelAnim = false;
        }

        //if (Input.GetKeyDown(KeyCode.T)) //ANIM2
        if (StartShooting == true) //Shoot Anim
        {
            animator.enabled = true;
            Bison_ShootingAnim = true;
            Bison_DamageAnim = false;
            Bison_AimingAnim = false;
            Bison_DeadAnim = false;
            Bison_ThrowBarrelAnim = false;
        }

        //if (Input.GetKeyDown(KeyCode.D)) //ANIM2
        if (this.hp <= 0) //death Anim
        {
            animator.enabled = true;
            Bison_DeadAnim = true;
            Bison_ShootingAnim = false;
            Bison_DamageAnim = false;
            Bison_AimingAnim = false;
            Bison_ThrowBarrelAnim = false;
        }

        //if (Input.GetKeyDown(KeyCode.T)) //ANIM3
        if (ThrowBarrel == true) //death Anim
        {
            animator.enabled = true;
            Bison_ThrowBarrelAnim = true;
            Bison_DeadAnim = false;
            Bison_ShootingAnim = false;
            Bison_DamageAnim = false;
            Bison_AimingAnim = false;
        }

        //----------------------------------------------------

        if (Bison_DamageAnim == true)
        {
            animator.SetBool("Bison_DamageAnim", true);
            animator.SetBool("Bison_AimingAnim", false);
            animator.SetBool("Bison_ShootingAnim", false);
            animator.SetBool("Bison_DamageAnim", false);
            animator.SetBool("Bison_DeadAnim", false);
        }

        if (Bison_AimingAnim == true)
        {
            animator.SetBool("Bison_AimingAnim", true);
            animator.SetBool("Bison_DamageAnim", false);
            animator.SetBool("Bison_ShootingAnim", false);
            animator.SetBool("Bison_DamageAnim", false);
            animator.SetBool("Bison_DeadAnim", false);
        }

        if (Bison_ShootingAnim == true)
        {
            animator.SetBool("Bison_ShootingAnim", true);
            animator.SetBool("Bison_AimingAnim", false);
            animator.SetBool("Bison_DamageAnim", false);
            animator.SetBool("Bison_DamageAnim", false);
            animator.SetBool("Bison_DeadAnim", false);
        }

        if (Bison_DeadAnim == true)
        {
            animator.SetBool("Bison_DeadAnim", true);
            animator.SetBool("Bison_ShootingAnim", false);
            animator.SetBool("Bison_AimingAnim", false);
            animator.SetBool("Bison_DamageAnim", false);
            animator.SetBool("Bison_ThrowBarrelAnim", true);

        }

        if (Bison_ThrowBarrelAnim == true)
        {
            animator.SetBool("Bison_ThrowBarrelAnim", true);
            animator.SetBool("Bison_ShootingAnim", false);
            animator.SetBool("Bison_AimingAnim", false);
            animator.SetBool("Bison_DamageAnim", false);
            animator.SetBool("Bison_DeadAnim", false);
        }


        //Enemy3 Soldier  Animation ========================================================= Enemy3 Soldier Animation =========================================================

        //if (Input.GetKeyDown(KeyCode.P))    //ANIM3
        if (EnemyHPreducing == true) //Damage Anim
        {
            animator.enabled = true;
            Soldier_DamageAnim = true;
            Soldier_AimingAnim = false;
            Soldier_ShootingAnim = false;
            Soldier_DeadAnim = false;
            Soldier_StabSlashAnim = false;
            Soldier_RunningAnim = false;
            Soldier_GranadeAnim = false;
        }

        // if (Input.GetKeyDown(KeyCode.R))//ANIM2 
        if (EnemyStartAppear == true) //Aimaing Anim
        {
            animator.enabled = true;
            Soldier_AimingAnim = true;
            Soldier_DamageAnim = false;
            Soldier_ShootingAnim = false;
            Soldier_DeadAnim = false;
            Soldier_StabSlashAnim = false;
            Soldier_RunningAnim = false;
            Soldier_GranadeAnim = false;
        }

        //if (Input.GetKeyDown(KeyCode.T)) //ANIM2
        if (StartShooting == true) //Shoot Anim
        {
            animator.enabled = true;
            Soldier_ShootingAnim = true;
            Soldier_AimingAnim = false;
            Soldier_DamageAnim = false;
            Soldier_DeadAnim = false;
            Soldier_StabSlashAnim = false;
            Soldier_RunningAnim = false;
            Soldier_GranadeAnim = false;
        }

        //if (Input.GetKeyDown(KeyCode.D)) //ANIM2
        if (this.hp <= 0) //death Anim
        {
            animator.enabled = true;
            Soldier_DeadAnim = true;
            Soldier_ShootingAnim = false;
            Soldier_AimingAnim = false;
            Soldier_DamageAnim = false;
            Soldier_StabSlashAnim = false;
            Soldier_RunningAnim = false;
            Soldier_GranadeAnim = false;
        }

        //if (Input.GetKeyDown(KeyCode.T)) //ANIM3
        if (ThrowGranade == true) //granade Anim
        {
            animator.enabled = true;
            Soldier_GranadeAnim = true;
            Soldier_DeadAnim = false;
            Soldier_ShootingAnim = false;
            Soldier_AimingAnim = false;
            Soldier_DamageAnim = false;
            Soldier_StabSlashAnim = false;
            Soldier_RunningAnim = false;
        }

        //if (Input.GetKeyDown(KeyCode.O)) //ANIM3
        if (StabSlash == true) //stabslash Anim
        {
            animator.enabled = true;
            Soldier_StabSlashAnim = true;
            Soldier_GranadeAnim = false;
            Soldier_DeadAnim = false;
            Soldier_ShootingAnim = false;
            Soldier_AimingAnim = false;
            Soldier_DamageAnim = false;
            Soldier_RunningAnim = false;
        }

        //if (Input.GetKeyDown(KeyCode.O)) //ANIM3
        if (Running == true) //running Anim
        {
            animator.enabled = true;
            Soldier_RunningAnim = true;
            Soldier_StabSlashAnim = false;
            Soldier_GranadeAnim = false;
            Soldier_DeadAnim = false;
            Soldier_ShootingAnim = false;
            Soldier_AimingAnim = false;
            Soldier_DamageAnim = false;
        }

        //----------------------------------------------------

        if (Soldier_DamageAnim == true)
        {
            animator.SetBool("Soldier_DamageAnim", true);
            animator.SetBool("Soldier_AimingAnim", false);
            animator.SetBool("Soldier_ShootingAnim", false);
            animator.SetBool("Soldier_DeadAnim", false);
            animator.SetBool("Soldier_RunningAnim", false);
            animator.SetBool("Soldier_GranadeAnim", false);
            animator.SetBool("Soldier_StabSlashAnim", false);
        }

        if (Soldier_AimingAnim == true)
        {
            animator.SetBool("Soldier_DamageAnim", false);
            animator.SetBool("Soldier_AimingAnim", true);
            animator.SetBool("Soldier_ShootingAnim", false);
            animator.SetBool("Soldier_DeadAnim", false);
            animator.SetBool("Soldier_RunningAnim", false);
            animator.SetBool("Soldier_GranadeAnim", false);
            animator.SetBool("Soldier_StabSlashAnim", false);
        }

        if (Soldier_ShootingAnim == true)
        {
            animator.SetBool("Soldier_DamageAnim", false);
            animator.SetBool("Soldier_AimingAnim", false);
            animator.SetBool("Soldier_ShootingAnim", true);
            animator.SetBool("Soldier_DeadAnim", false);
            animator.SetBool("Soldier_RunningAnim", false);
            animator.SetBool("Soldier_GranadeAnim", false);
            animator.SetBool("Soldier_StabSlashAnim", false);
        }

        if (Soldier_DeadAnim == true)
        {
            animator.SetBool("Soldier_DamageAnim", false);
            animator.SetBool("Soldier_AimingAnim", false);
            animator.SetBool("Soldier_ShootingAnim", false);
            animator.SetBool("Soldier_DeadAnim", true);
            animator.SetBool("Soldier_RunningAnim", false);
            animator.SetBool("Soldier_GranadeAnim", false);
            animator.SetBool("Soldier_StabSlashAnim", false);

        }

        if (Soldier_RunningAnim == true)
        {
            animator.SetBool("Soldier_DamageAnim", false);
            animator.SetBool("Soldier_AimingAnim", false);
            animator.SetBool("Soldier_ShootingAnim", false);
            animator.SetBool("Soldier_DeadAnim", false);
            animator.SetBool("Soldier_RunningAnim", true);
            animator.SetBool("Soldier_GranadeAnim", false);
            animator.SetBool("Soldier_StabSlashAnim", false);

        }

        if (Soldier_GranadeAnim == true)
        {
            animator.SetBool("Soldier_DamageAnim", false);
            animator.SetBool("Soldier_AimingAnim", false);
            animator.SetBool("Soldier_ShootingAnim", false);
            animator.SetBool("Soldier_DeadAnim", false);
            animator.SetBool("Soldier_RunningAnim", false);
            animator.SetBool("Soldier_GranadeAnim", true);
            animator.SetBool("Soldier_StabSlashAnim", false);

        }

        if (Soldier_StabSlashAnim == true)
        {
            animator.SetBool("Soldier_DamageAnim", false);
            animator.SetBool("Soldier_AimingAnim", false);
            animator.SetBool("Soldier_ShootingAnim", false);
            animator.SetBool("Soldier_DeadAnim", false);
            animator.SetBool("Soldier_RunningAnim", false);
            animator.SetBool("Soldier_GranadeAnim", false);
            animator.SetBool("Soldier_StabSlashAnim", true);

        }

        /*
        if (left == true && right == false && down == false && top == false && EnemyStartAppear == true)
        {
            endPosition = new Vector3(-endPosX, this.transform.position.y, this.transform.position.z);
            if (transform.position != endPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
            }
        }
        if (left == false && right == true && down == false && top == false && EnemyStartAppear == true)
        {
            endPosition = new Vector3(endPosX, this.transform.position.y, this.transform.position.z);
            if (transform.position != endPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
                if (transform.position == endPosition)
                {
                    left = false;
                    right = false;
                    down = false;
                    top = false;
                    EnemyStartAppear = false;
                    StartShooting = true;

                    timer += Time.deltaTime;

                    if (timer >= hideDelayTimer)
                    {
                        StartShooting = false;
                        enemyHide = true;
                        timer = 0;
                        if (enemyHide == true && StartShooting == false)
                        {
                            //back last position;
                            if (transform.position != startPosition)
                            {
                                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
                                if (transform.position == startPosition)
                                {
                                    timer++;
                                    enemyHide = false;
                                    EnemyStartAppear = true;
                                    right = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        if (left == false && right == false && down == true && top == false && EnemyStartAppear == true)
        {
            endPosition = new Vector3(this.transform.position.x, -endPosY, this.transform.position.z);
            if (transform.position != endPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
                if (transform.position == endPosition)
                {
                    left = false;
                    right = false;
                    down = false;
                    top = false;
                    EnemyStartAppear = false;
                    StartShooting = true;

                    timer += Time.deltaTime;

                    if (timer >= hideDelayTimer)
                    {
                        StartShooting = false;
                        enemyHide = true;
                        timer = 0;
                        if (enemyHide == true && StartShooting == false)
                        {
                            //back last position;
                            if (transform.position != startPosition)
                            {
                                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
                                if (transform.position == startPosition)
                                {
                                    timer++;
                                    enemyHide = false;
                                    EnemyStartAppear = true;
                                    down = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        if (left == false && right == false && down == false && top == true && EnemyStartAppear == true)
        {
            endPosition = new Vector3(this.transform.position.x, endPosY, this.transform.position.z);
            if (transform.position != endPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
                if (transform.position == endPosition)
                {
                    left = false;
                    right = false;
                    down = false;
                    top = false;
                    EnemyStartAppear = false;
                    StartShooting = true;

                    timer += Time.deltaTime;

                    if (timer >= hideDelayTimer)
                    {
                        StartShooting = false;
                        enemyHide = true;
                        timer = 0;
                        if (enemyHide == true && StartShooting == false)
                        {
                            //back last position;
                            if (transform.position != startPosition)
                            {
                                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
                                if (transform.position == startPosition)
                                {
                                    timer++;
                                    enemyHide = false;
                                    EnemyStartAppear = true;
                                    top = true;
                                }
                            }
                        }
                    }
                }
            }
        }*/


    }
}

