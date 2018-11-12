using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RaycastShoot : MonoBehaviour
{
    public Weapon weapon;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;
    public Camera camera;

    public int meleeDamage;

    private Vector3 firstPos = Vector3.zero; // First Position
    private Vector3 lastPos = Vector3.zero;  // Last Position
    private float dragDistance;  //minimum distance for a swipe to be registered
    //! UI
    public List<Image> bulletList; // test purpose
    public GameObject reloadIndicator;
    public Text bulletLeft;

    public GameObject meleeBlade;

    public bool isUItouch = false;
    public bool isSwipe = false;
    public float maxSwipeTime = 0.5f;
    public float currentSwipeTime = 0f;

    void Start()
    {
        dragDistance = Screen.height * 0.05f;
        laserLine = GetComponent<LineRenderer>();
        bulletLeft.text = weapon.currentAmmo.ToString();
        weapon.reloading = false;
        gunAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Debug.Log(Input.acceleration.y);
        //Debug.Log(Input.acceleration.x);

        // Reload
        if ((new Vector2(Input.acceleration.x, Input.acceleration.z).magnitude > 2 && weapon.reloading == false)) //&& Input.GetButtonDown("Jump")) 
        {
            weapon.reloading = true;
            if (weapon.clipReload == true)
            {
                StartCoroutine(ReloadEffect(weapon.reloadTime));
            }
            else
            {
                StartCoroutine(ReloadEffect2(weapon.eachBulletRequire, (weapon.maxAmmo - weapon.currentAmmo)));
            }
            for (int i = 0; i < weapon.currentAmmo; i++)
            {
                Animator anim = bulletList[i].GetComponent<Animator>();
                anim.Play("bulletIdle");
            }
        }
        /*if(weapon.currentAmmo <= 10)
        {
            for (int i = 0; i < bulletList.Count; i++)
            {
                Animator anim = bulletList[i].GetComponent<Animator>();
                anim
            }
            for (int i = 0; i < weapon.currentAmmo; i++)
            {
                Animation anim = bulletList[i].GetComponent<Animation>();
                anim.Play("bulletIdle");
                //bulletList[i].gameObject.SetActive(true);
            }
        }*/
        
        //MouseShoot();
        TouchShoot();
    }

    public void MouseShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Time.time > nextFire && weapon.currentAmmo > 0)
                {
                    nextFire = Time.time + weapon.fireRate;
                    StartCoroutine(ShotEffect());
                    Vector3 posFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.farClipPlane);
                    Vector3 posNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane);
                    Vector3 posF = camera.ScreenToWorldPoint(posFar);
                    Vector3 posN = camera.ScreenToWorldPoint(posNear);
                    RaycastHit hit;
                    weapon.currentAmmo--;
                    Vector3 shootOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
                    laserLine.SetPosition(0, shootOrigin);
                    if (Physics.Raycast(shootOrigin, posF - posN, out hit, weapon.weaponRange))
                    {
                        laserLine.SetPosition(1, hit.point);
                        GameObject bulletEffect = Instantiate(weapon.effect, hit.point, transform.rotation);
                        Destroy(bulletEffect, 1.0f);
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            hit.collider.gameObject.GetComponent<EnemyMovement>().hp -= weapon.gunDamage;
                        }
                        else if (hit.collider.CompareTag("Environment"))
                        {

                        }
                        else if (hit.collider.CompareTag("Enemy_Destroyable_Bullet"))
                        {
                            hit.collider.gameObject.GetComponent<Enemy_Destroyable_Bullet>().hp -= weapon.gunDamage;
                        }
                    }
                    else
                    {
                        laserLine.SetPosition(1, camera.ScreenToWorldPoint(Input.mousePosition));
                        GameObject bulletEffect = Instantiate(weapon.effect, shootOrigin + ((posF - posN) * weapon.weaponRange), transform.rotation);
                        Destroy(bulletEffect, 1.0f);
                    }
                    bulletLeft.text = weapon.currentAmmo.ToString();
                }
            }
        }
    }

    public void TouchShoot()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);            
            switch(touch.phase)
            {
                case TouchPhase.Began:
                    firstPos = touch.position;
                    lastPos = touch.position;
                    if(EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    {
                        isUItouch = true;
                    }
                    Debug.Log("Began");
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    lastPos = touch.position;
                    currentSwipeTime += Time.deltaTime;
                    if (currentSwipeTime > maxSwipeTime)
                    {
                        if (Mathf.Abs(lastPos.x - firstPos.x) > dragDistance || Mathf.Abs(lastPos.y - firstPos.y) > dragDistance)
                        {
                            meleeBlade.SetActive(true);
                            isSwipe = true;
                            Vector3 mouse_pos = Input.mousePosition;
                            mouse_pos.z = 5;
                            Vector3 worldPos = camera.ScreenToWorldPoint(mouse_pos);
                            meleeBlade.transform.LookAt(worldPos);
                        }
                        else
                        {
                            if (!isUItouch)
                            {
                                if (!isSwipe)
                                {
                                    if (Time.time > nextFire && weapon.currentAmmo > 0)
                                    {
                                        nextFire = Time.time + weapon.fireRate;
                                        StartCoroutine(ShotEffect());
                                        Vector3 posFar = new Vector3(touch.position.x, touch.position.y, camera.farClipPlane);
                                        Vector3 posNear = new Vector3(touch.position.x, touch.position.y, camera.nearClipPlane);
                                        Vector3 posF = camera.ScreenToWorldPoint(posFar);
                                        Vector3 posN = camera.ScreenToWorldPoint(posNear);
                                        RaycastHit hit;
                                        weapon.currentAmmo--;
                                        //Animator anim = bulletList[weapon.currentAmmo].GetComponent<Animator>();
                                        //anim.Play("bulletAnim");
                                        Vector3 shootOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
                                        laserLine.SetPosition(0, shootOrigin);
                                        if (Physics.Raycast(shootOrigin, posF - posN, out hit, weapon.weaponRange))
                                        {
                                            laserLine.SetPosition(1, hit.point);
                                            GameObject bulletEffect = Instantiate(weapon.effect, hit.point, transform.rotation);
                                            Destroy(bulletEffect, 1.0f);
                                            if (hit.collider.CompareTag("Enemy"))
                                            {
                                                hit.collider.gameObject.GetComponent<EnemyMovement>().hp -= weapon.gunDamage;
                                            }
                                            else if (hit.collider.CompareTag("Enemy_Destroyable_Bullet"))
                                            {
                                                hit.collider.gameObject.GetComponent<Enemy_Destroyable_Bullet>().hp -= weapon.gunDamage;
                                            }
                                        }
                                        bulletLeft.text = weapon.currentAmmo.ToString();
                                    }
                                }
                            }
                        }
                    }
                    Debug.Log("Moving/Stanionary");
                    break;
                case TouchPhase.Ended:
                    lastPos = touch.position;
                    if(!isUItouch)
                    {
                        if (!isSwipe)
                        {
                            if (Time.time > nextFire && weapon.currentAmmo > 0)
                            {
                                nextFire = Time.time + weapon.fireRate;
                                StartCoroutine(ShotEffect());
                                Vector3 posFar = new Vector3(touch.position.x, touch.position.y, camera.farClipPlane);
                                Vector3 posNear = new Vector3(touch.position.x, touch.position.y, camera.nearClipPlane);
                                Vector3 posF = camera.ScreenToWorldPoint(posFar);
                                Vector3 posN = camera.ScreenToWorldPoint(posNear);
                                RaycastHit hit;
                                weapon.currentAmmo--;
                                //Animator anim = bulletList[weapon.currentAmmo].GetComponent<Animator>();
                                //anim.Play("bulletAnim");
                                Vector3 shootOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
                                laserLine.SetPosition(0, shootOrigin);
                                if (Physics.Raycast(shootOrigin, posF - posN, out hit, weapon.weaponRange))
                                {
                                    laserLine.SetPosition(1, hit.point);
                                    GameObject bulletEffect = Instantiate(weapon.effect, hit.point, transform.rotation);
                                    Destroy(bulletEffect, 1.0f);
                                    if (hit.collider.CompareTag("Enemy"))
                                    {
                                        hit.collider.gameObject.GetComponent<EnemyMovement>().hp -= weapon.gunDamage;
                                    }
                                    else if (hit.collider.CompareTag("Environment"))
                                    {

                                    }
                                    else if (hit.collider.CompareTag("Enemy_Destroyable_Bullet"))
                                    {
                                        hit.collider.gameObject.GetComponent<Enemy_Destroyable_Bullet>().hp -= weapon.gunDamage;
                                    }
                                }
                                bulletLeft.text = weapon.currentAmmo.ToString();
                            }
                        }
                        else  // if is a swipe, perform melee
                        {
                            Debug.Log("Melee");
                            Vector3 mouse_pos = Input.mousePosition;
                            mouse_pos.z = 5;
                            Vector3 worldPos = camera.ScreenToWorldPoint(mouse_pos);
                            meleeBlade.transform.LookAt(worldPos);
                        }
                        Debug.Log("NotUI");
                    }
                    currentSwipeTime = 0.0f;
                    isUItouch = false;
                    isSwipe = false;
                    meleeBlade.SetActive(false);
                    Debug.Log("Ended");
                    break;
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        gunAudio.Play();
        //laserLine.enabled = true;
        yield return shotDuration;
        //laserLine.enabled = false;
    }

    private IEnumerator ReloadEffect(float rT)
    {
        reloadIndicator.SetActive(true);
        yield return new WaitForSeconds(rT);
        weapon.currentAmmo = weapon.maxAmmo;
        bulletLeft.text = weapon.currentAmmo.ToString();// + " / " + weapon.maxAmmo.ToString();
        reloadIndicator.SetActive(false);
        weapon.reloading = false;
    }

    private IEnumerator ReloadEffect2(float perBullet, int bulletCount)
    {
        reloadIndicator.SetActive(true);
        Debug.Log(bulletCount);
        for (int i = 0; i < bulletCount; i++)
        {
            yield return new WaitForSeconds(perBullet);
            weapon.currentAmmo++;
            bulletLeft.text = weapon.currentAmmo.ToString();// + " / " + weapon.maxAmmo.ToString();
            Debug.Log("+1");
        }
        reloadIndicator.SetActive(false);
        weapon.reloading = false;
    }
}
//[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
[System.Serializable]
public class Weapon //: ScriptableObject
{
    public int gunDamage = 1;
    public float fireRate = .25f;
    public float weaponRange = 500f;
    public int currentAmmo = 6;
    public int maxAmmo = 6;
    public float reloadTime;
    public bool clipReload;
    public float eachBulletRequire;
    public GameObject effect;
    public bool reloading;
}
