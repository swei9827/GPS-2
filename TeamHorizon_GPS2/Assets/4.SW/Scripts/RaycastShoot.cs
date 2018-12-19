using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RaycastShoot : MonoBehaviour
{
    public static RaycastShoot instance;
    public Weapon weapon;
    public Camera playerCamera;
    public AudioSource fireAudio;
    public AudioSource missAudio;
    public bool isUItouch = false;
    public bool isReloading = false;
    public int currentAmmo;

    //! UI
    public List<Image> bulletList;
    public GameObject reloadNotice;
    public GameObject reloadIndicator;
    public Text bulletLeft;

    public bool isDisable;
    //for debugging use
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isDisable = false;
        currentAmmo = weapon.maxAmmo;
        bulletLeft.text = currentAmmo.ToString();
    }

    void Update()
    {
        // Reload
        if (((new Vector2(Input.acceleration.x, Input.acceleration.z).magnitude > 1.5 || Input.GetButtonDown("Jump"))&& isReloading == false))
        {
            isReloading = true;
            if (weapon.clipReload == true)
            {
                StartCoroutine(ReloadEffect(weapon.reloadTime));
            }
            else
            {
                StartCoroutine(ReloadEffect2(weapon.eachBulletRequire, (weapon.maxAmmo - currentAmmo)));
            }
            for (int i = 0; i < currentAmmo; i++)
            {
                Animator anim = bulletList[i].GetComponent<Animator>();
                anim.Play("bulletIdle");
            }
        }
        MouseShoot();
        TouchShoot();
    }


    public void MouseShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject() && !isDisable)
            {
                if (currentAmmo > 0 && !isReloading)
                {
                    fireAudio.Play();
                    Vector3 posFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, playerCamera.farClipPlane);
                    Vector3 posNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, playerCamera.nearClipPlane);
                    Vector3 posF = playerCamera.ScreenToWorldPoint(posFar);
                    Vector3 posN = playerCamera.ScreenToWorldPoint(posNear);
                    RaycastHit hit;
                    currentAmmo--;
                    if(currentAmmo < 10)
                    {
                        Animator anim = bulletList[currentAmmo].GetComponent<Animator>();
                        anim.Play("bulletAnim");
                    }
                    else if(currentAmmo == 0)
                    {
                        reloadNotice.SetActive(true);
                        reloadNotice.GetComponent<Animator>().Play("ReloadNotice");
                    }
                    Vector3 shootOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));                    
                    if (Physics.Raycast(shootOrigin, posF - posN, out hit, weapon.range))
                    {                       
                        GameObject bulletEffect = Instantiate(weapon.effect, hit.point, transform.rotation);
                        Destroy(bulletEffect, 1.0f);
                        if(hit.collider.CompareTag("Enemy"))
                        {                            
                            hit.collider.gameObject.GetComponent<EnemyMovement>().hp -= weapon.damage;
                            hit.collider.gameObject.GetComponentInParent<DamageFlash>().StartCoroutine("Flash");
                        }
                        else if (hit.collider.CompareTag("EnemyHead"))
                        {
                            Debug.Log("Hit Head");                            
                            hit.collider.gameObject.GetComponentInParent<EnemyMovement>().DamageCalculation(weapon.damage, 1);
                            hit.collider.gameObject.GetComponentInParent<DamageFlash>().StartCoroutine("Flash");
                        }
                        else if (hit.collider.CompareTag("EnemyBody"))
                        {
                            Debug.Log("Hit Body");
                            hit.collider.gameObject.GetComponentInParent<EnemyMovement>().DamageCalculation(weapon.damage, 2);
                            hit.collider.gameObject.GetComponentInParent<DamageFlash>().StartCoroutine("Flash");
                        }
                        else if (hit.collider.CompareTag("EnemyHand"))
                        {
                            Debug.Log("Hit Hand");
                            hit.collider.gameObject.GetComponentInParent<EnemyMovement>().DamageCalculation(weapon.damage, 3);
                            hit.collider.gameObject.GetComponentInParent<DamageFlash>().StartCoroutine("Flash");
                        }
                        else if (hit.collider.CompareTag("Enemy_Destroyable_Bullet"))
                        {
                            hit.collider.gameObject.GetComponent<Enemy_Destroyable_Bullet>().hp -= weapon.damage;
                        }
                        else if (hit.collider.CompareTag("Environment"))
                        {                            
                            hit.collider.gameObject.GetComponent<EnvironementTrigger>().ETDamage(); ;
                        }
                        else if (hit.collider.CompareTag("FallingTree"))
                        {
                            hit.collider.gameObject.GetComponent<TreeFallHazard>().TreeFallDamage();
                        }
                        else if (hit.collider.CompareTag("FallingTreeV2"))
                        {
                            hit.collider.gameObject.GetComponent<TreeFallHazardWithDistanceChecker>().TreeV2FallDamage();
                        }
                        else if (hit.collider.CompareTag("BlockingTree"))
                        {                            
                            hit.collider.gameObject.GetComponent<TreeBlockHazard>().TreeBlockDamage();
                        }
                        else if (hit.collider.CompareTag("Obstacle"))
                        {
                            hit.collider.gameObject.GetComponent<Obstacles>().ObstaclesDamage();
                        }
                        else if (hit.collider.CompareTag("Interactive"))
                        {
                            hit.collider.gameObject.GetComponent<IObstacles>().IObstaclesDamage();
                        }
                        else
                        {
                            missAudio.Play();
                        }
                    }
                    else
                    { 
                        GameObject bulletEffect = Instantiate(weapon.effect, shootOrigin + ((posF - posN) * weapon.range), transform.rotation);
                        Destroy(bulletEffect, 1.0f);
                    }
                    bulletLeft.text = currentAmmo.ToString();
                }
                else
                {
                    reloadNotice.SetActive(true);
                    reloadNotice.GetComponent<Animator>().Play("ReloadNotice");
                }
            }
        }
    }

    public void TouchShoot()
    {
        if (Input.touchCount > 0 && !isDisable)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    {
                        isUItouch = true;
                    }

                    if(currentAmmo <= 0)
                    {
                        reloadNotice.SetActive(true);
                        reloadNotice.GetComponent<Animator>().Play("ReloadNotice");
                    }
                    Debug.Log("Began");
                    break;
                //case TouchPhase.Moved:
                //case TouchPhase.Stationary:
                //    lastPos = touch.position;
                //    if (!isUItouch)
                //    {
                //        if (currentAmmo > 0 && !isReloading)
                //        {
                //            StartCoroutine(ShotEffect());
                //            Vector3 posFar = new Vector3(touch.position.x, touch.position.y, playerCamera.farClipPlane);
                //            Vector3 posNear = new Vector3(touch.position.x, touch.position.y, playerCamera.nearClipPlane);
                //            Vector3 posF = playerCamera.ScreenToWorldPoint(posFar);
                //            Vector3 posN = playerCamera.ScreenToWorldPoint(posNear);
                //            RaycastHit hit;
                //            currentAmmo--;
                //            //Animator anim = bulletList[weapon.currentAmmo].GetComponent<Animator>();
                //            //anim.Play("bulletAnim");
                //            Vector3 shootOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
                //            laserLine.SetPosition(0, shootOrigin);
                //            if (Physics.Raycast(shootOrigin, posF - posN, out hit, weapon.range))
                //            {
                //                laserLine.SetPosition(1, hit.point);
                //                GameObject bulletEffect = Instantiate(weapon.effect, hit.point, transform.rotation);
                //                Destroy(bulletEffect, 1.0f);
                //                if (hit.collider.CompareTag("Enemy"))
                //                {
                //                    hit.collider.gameObject.GetComponent<EnemyMovement>().hp -= weapon.damage;
                //                }
                //                else if (hit.collider.CompareTag("EnemyHead"))
                //                {
                //                    Debug.Log("Hit Head");
                //                    hit.collider.gameObject.GetComponentInParent<EnemyMovement>().DamageCalculation(weapon.damage, 1);
                //                }
                //                else if (hit.collider.CompareTag("EnemyBody"))
                //                {
                //                    Debug.Log ("Hit Body");
                //                    hit.collider.gameObject.GetComponentInParent<EnemyMovement>().DamageCalculation(weapon.damage, 2);
                //                }
                //                else if (hit.collider.CompareTag("EnemyHand"))
                //                {
                //                    Debug.Log("Hit Hand");
                //                    hit.collider.gameObject.GetComponentInParent<EnemyMovement>().DamageCalculation(weapon.damage, 3);
                //                }                               
                //                else if (hit.collider.CompareTag("Enemy_Destroyable_Bullet"))
                //                {
                //                    hit.collider.gameObject.GetComponent<Enemy_Destroyable_Bullet>().hp -= weapon.damage;
                //                }
                //                else if (hit.collider.CompareTag("Environment"))
                //                {
                //                    hit.collider.gameObject.GetComponent<EnvironementTrigger>().ETDamage(); ;
                //                }
                //                else if (hit.collider.CompareTag("FallingTree"))
                //                {
                //                    hit.collider.gameObject.GetComponent<TreeFallHazard>().TreeFallDamage();
                //                }
                //                else if (hit.collider.CompareTag("BlockingTree"))
                //                {
                //                    hit.collider.gameObject.GetComponent<TreeBlockHazard>().TreeBlockDamage();
                //                }
                //                else if (hit.collider.CompareTag("Obstacle"))
                //                {
                //                    hit.collider.gameObject.GetComponent<Obstacles>().ObstaclesDamage();
                //                }
                //                else if (hit.collider.CompareTag("Interactive"))
                //                {
                //                    hit.collider.gameObject.GetComponent<IObstacles>().IObstaclesDamage();
                //                }
                //            }
                //            bulletLeft.text = currentAmmo.ToString();
                //        }
                //    }
                //    Debug.Log("Moving/Stanionary");
                //    break;
                case TouchPhase.Ended:
                    if (!isUItouch)
                    {
                        if (currentAmmo > 0 && !isReloading)
                        {
                            fireAudio.Play();
                            Vector3 posFar = new Vector3(touch.position.x, touch.position.y, playerCamera.farClipPlane);
                            Vector3 posNear = new Vector3(touch.position.x, touch.position.y, playerCamera.nearClipPlane);
                            Vector3 posF = playerCamera.ScreenToWorldPoint(posFar);
                            Vector3 posN = playerCamera.ScreenToWorldPoint(posNear);
                            RaycastHit hit;
                            currentAmmo--;
                            if (currentAmmo < 10)
                            {
                                Animator anim = bulletList[currentAmmo].GetComponent<Animator>();
                                anim.Play("bulletAnim");
                            }
                            else if (currentAmmo == 0)
                            {
                                reloadNotice.SetActive(true);
                                reloadNotice.GetComponent<Animator>().Play("ReloadNotice");
                            }
                            Vector3 shootOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));                            
                            if (Physics.Raycast(shootOrigin, posF - posN, out hit, weapon.range))
                            {                                
                                GameObject bulletEffect = Instantiate(weapon.effect, hit.point, transform.rotation);
                                Destroy(bulletEffect, 1.0f);
                                if (hit.collider.CompareTag("Enemy"))
                                {                                    
                                    hit.collider.gameObject.GetComponent<EnemyMovement>().hp -= weapon.damage;
                                    hit.collider.gameObject.GetComponentInParent<DamageFlash>().StartCoroutine("Flash");
                                }
                                else if (hit.collider.CompareTag("EnemyHead"))
                                {
                                    Debug.Log("Hit Head");
                                    hit.collider.gameObject.GetComponentInParent<EnemyMovement>().DamageCalculation(weapon.damage, 1);
                                    hit.collider.gameObject.GetComponentInParent<DamageFlash>().StartCoroutine("Flash");
                                }
                                else if (hit.collider.CompareTag("EnemyBody"))
                                {
                                    Debug.Log("Hit Body");
                                    hit.collider.gameObject.GetComponentInParent<EnemyMovement>().DamageCalculation(weapon.damage, 2);
                                    hit.collider.gameObject.GetComponentInParent<DamageFlash>().StartCoroutine("Flash");
                                }
                                else if (hit.collider.CompareTag("EnemyHand"))
                                {
                                    Debug.Log("Hit Hand");
                                    hit.collider.gameObject.GetComponentInParent<EnemyMovement>().DamageCalculation(weapon.damage, 3);
                                    hit.collider.gameObject.GetComponentInParent<DamageFlash>().StartCoroutine("Flash");
                                }
                                else if (hit.collider.CompareTag("Enemy_Destroyable_Bullet"))
                                {
                                    hit.collider.gameObject.GetComponent<Enemy_Destroyable_Bullet>().hp -= weapon.damage;
                                }
                                else if (hit.collider.CompareTag("Environment"))
                                {
                                    hit.collider.gameObject.GetComponent<EnvironementTrigger>().ETDamage(); ;
                                }
                                else if (hit.collider.CompareTag("FallingTree"))
                                {
                                    hit.collider.gameObject.GetComponent<TreeFallHazard>().TreeFallDamage();
                                }
                                else if (hit.collider.CompareTag("FallingTreeV2"))
                                {
                                    hit.collider.gameObject.GetComponent<TreeFallHazardWithDistanceChecker>().TreeV2FallDamage();
                                }
                                else if (hit.collider.CompareTag("BlockingTree"))
                                {
                                    hit.collider.gameObject.GetComponent<TreeBlockHazard>().TreeBlockDamage();
                                }
                                else if (hit.collider.CompareTag("Obstacle"))
                                {
                                    hit.collider.gameObject.GetComponent<Obstacles>().ObstaclesDamage();
                                }
                                else if (hit.collider.CompareTag("Interactive"))
                                {
                                    hit.collider.gameObject.GetComponent<IObstacles>().IObstaclesDamage();
                                }
                                else
                                {
                                    missAudio.Play();
                                }
                            }
                            bulletLeft.text = currentAmmo.ToString();
                        }
                        Debug.Log("NotUI");
                    }
                    isUItouch = false;
                    Debug.Log("Ended");
                    break;
            }
        }
        bulletLeft.text = currentAmmo.ToString();

    }

    public void CrouchReload()
    {
        isReloading = true;
        if (weapon.clipReload == true)
        {
            StartCoroutine(ReloadEffect(weapon.reloadTime));
        }
        else
        {
            StartCoroutine(ReloadEffect2(weapon.eachBulletRequire, (weapon.maxAmmo - currentAmmo)));
        }
        for (int i = 0; i < currentAmmo; i++)
        {
            Animator anim = bulletList[i].GetComponent<Animator>();
            anim.Play("bulletIdle");
        }
    }

    private IEnumerator ReloadEffect(float rT)
    {
        reloadIndicator.SetActive(true);
        yield return new WaitForSeconds(rT);
        currentAmmo = weapon.maxAmmo;
        if(currentAmmo >= 10)
        {
            for(int i = 0; i<10;i++)
            {
                bulletList[i].GetComponent<Animator>().Play("bulletIdle");
            }
        }
        else
        {
            for (int i = 0; i < currentAmmo; i++)
            {
                bulletList[i].GetComponent<Animator>().Play("bulletIdle");
            }
        }
        bulletLeft.text = currentAmmo.ToString();
        reloadIndicator.SetActive(false);
        isReloading = false;
        reloadNotice.SetActive(false);
    }

    private IEnumerator ReloadEffect2(float perBullet, int bulletCount)
    {
        reloadIndicator.SetActive(true);
        for (int i = 0; i < bulletCount; i++)
        {
            yield return new WaitForSeconds(perBullet);
            currentAmmo++;
            bulletLeft.text = currentAmmo.ToString();
        }
        reloadIndicator.SetActive(false);
        isReloading = false;
        reloadNotice.SetActive(false);
    }
}
