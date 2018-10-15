using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RaycastShoot : MonoBehaviour//,IPointerDownHandler
{
    public CURRENT_SELECTED_WEAPON CSW;
    public Weapon weapon;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;
    public Camera camera;
    private bool reloading;

    public int meleeDamage;

    private Vector3 firstPos = Vector3.zero; // First Position
    private Vector3 lastPos = Vector3.zero;  // Last Position
    private float dragDistance;  //minimum distance for a swipe to be registered
    //! UI
    public List<Image> bulletList; // test purpose
    public GameObject reloadIndicator;
    public Text bulletLeft;

    void Start()
    {
        dragDistance = Screen.height * 0.15f;
        laserLine = GetComponent<LineRenderer>();
        bulletLeft.text = weapon.currentAmmo.ToString() + " / " + weapon.maxAmmo.ToString();
        reloading = false;
        gunAudio = GetComponent<AudioSource>();
        CSW = CURRENT_SELECTED_WEAPON.RANGE;
    }

    void Update()
    {
        //Debug.Log(Input.acceleration.y);
        //Debug.Log(Input.acceleration.x);

        // Reload
        if ((new Vector2(Input.acceleration.x, Input.acceleration.z).magnitude > 2 && reloading == false)) //&& Input.GetButtonDown("Jump")) 
        {
            reloading = true;
            if (weapon.clipReload == true)
            {
                StartCoroutine(ReloadEffect(weapon.reloadTime));
            }
            else
            {
                StartCoroutine(ReloadEffect2(weapon.eachBulletRequire, (weapon.maxAmmo - weapon.currentAmmo)));
            }
        }
        /*for (int i = 0; i < bulletList.Count; i++)
        {
            bulletList[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < weapon.currentAmmo; i++)
        {
            //bulletList[i].gameObject.SetActive(true);
        }*/
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                if (CSW == CURRENT_SELECTED_WEAPON.RANGE)
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
                        }
                        else
                        {
                            laserLine.SetPosition(1, camera.ScreenToWorldPoint(touch.position));
                            GameObject bulletEffect = Instantiate(weapon.effect, shootOrigin + ((posF - posN) * weapon.weaponRange), transform.rotation);
                            Destroy(bulletEffect, 1.0f);
                        }
                        bulletLeft.text = weapon.currentAmmo.ToString() + " / " + weapon.maxAmmo.ToString();
                    }
                }
                else if (CSW == CURRENT_SELECTED_WEAPON.MELEE)
                {
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            firstPos = touch.position;
                            lastPos = touch.position;
                            break;
                        case TouchPhase.Moved:
                            lastPos = touch.position;
                            break;
                        case TouchPhase.Ended:
                            lastPos = touch.position;
                            if (Mathf.Abs(lastPos.x - firstPos.x) > dragDistance || Mathf.Abs(lastPos.y - firstPos.y) > dragDistance)
                            {
                                Debug.Log("Melee");
                                GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
                                GameObject closest = null;
                                float distance = 100;
                                Vector3 myPos = transform.position;
                                foreach (GameObject go in enemy)
                                {
                                    Vector3 diff = go.transform.position - myPos;
                                    float curDistance = diff.sqrMagnitude;
                                    if (curDistance < distance)
                                    {
                                        closest = go;
                                        distance = curDistance;
                                    }
                                }
                                if (closest != null)
                                {
                                    if (closest.tag == "Enemy")
                                    {
                                        closest.GetComponent<EnemyHP>().hp -= meleeDamage;
                                    }
                                }
                            }
                            break;
                    }
                }
            }
        }
        
    }
    
    public void SwitchWeapon()
    {
        if(CSW == CURRENT_SELECTED_WEAPON.MELEE)
        {
            Debug.Log("Switch To Range");
            CSW = CURRENT_SELECTED_WEAPON.RANGE;
        }
        else
        {
            CSW = CURRENT_SELECTED_WEAPON.MELEE;
            Debug.Log("Switch To Melee");
        }
    }

    private IEnumerator ShotEffect()
    {
        gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }

    private IEnumerator ReloadEffect(float rT)
    {
        reloadIndicator.SetActive(true);
        yield return new WaitForSeconds(rT);
        weapon.currentAmmo = weapon.maxAmmo;
        bulletLeft.text = weapon.currentAmmo.ToString() + " / " + weapon.maxAmmo.ToString();
        reloadIndicator.SetActive(false);
        reloading = false;
    }

    private IEnumerator ReloadEffect2(float perBullet, int bulletCount)
    {
        reloadIndicator.SetActive(true);
        Debug.Log(bulletCount);
        for (int i = 0; i < bulletCount; i++)
        {
            yield return new WaitForSeconds(perBullet);
            weapon.currentAmmo++;
            bulletLeft.text = weapon.currentAmmo.ToString() + " / " + weapon.maxAmmo.ToString();
            Debug.Log("+1");
        }
        reloadIndicator.SetActive(false);
        reloading = false;
    }

    public void OnPointerDown(PointerEventData touchData)
    {/*
        Touch touch = Input.GetTouch(0);
        if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            if (CSW == CURRENT_SELECTED_WEAPON.RANGE)
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
                    Vector3 shootOrigin = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
                    laserLine.SetPosition(0, shootOrigin);
                    if (Physics.Raycast(shootOrigin, posF - posN, out hit, weapon.weaponRange))
                    {
                        laserLine.SetPosition(1, hit.point);
                        GameObject bulletEffect = Instantiate(weapon.effect, hit.point, transform.rotation);
                        Destroy(bulletEffect, 1.0f);
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            hit.collider.gameObject.GetComponent<EnemyHP>().hp -= weapon.gunDamage;
                        }
                        else if(hit.collider.CompareTag("Environment"))
                        {

                        }
                    }
                    else
                    {
                        laserLine.SetPosition(1, camera.ScreenToWorldPoint(touchData.position));
                        GameObject bulletEffect = Instantiate(weapon.effect, shootOrigin + ((posF - posN) * weapon.weaponRange), transform.rotation);
                        Destroy(bulletEffect, 1.0f);
                    }
                    bulletLeft.text = weapon.currentAmmo.ToString() + " / " + weapon.maxAmmo.ToString();
                }
            }
            else if (CSW == CURRENT_SELECTED_WEAPON.MELEE)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        firstPos = touch.position;
                        lastPos = touch.position;
                        break;
                    case TouchPhase.Moved:
                        lastPos = touch.position;
                        break;
                    case TouchPhase.Ended:
                        lastPos = touch.position;
                        if (Mathf.Abs(lastPos.x - firstPos.x) > dragDistance || Mathf.Abs(lastPos.y - firstPos.y) > dragDistance)
                        {
                            Debug.Log("Melee");
                            GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
                            GameObject closest = null;
                            float distance = 100;
                            Vector3 myPos = transform.position;
                            foreach (GameObject go in enemy)
                            {
                                Vector3 diff = go.transform.position - myPos;
                                float curDistance = diff.sqrMagnitude;
                                if (curDistance < distance)
                                {
                                    closest = go;
                                    distance = curDistance;
                                }
                            }
                            if (closest != null)
                            {
                                if (closest.tag == "Enemy")
                                {
                                    closest.GetComponent<EnemyHP>().hp -= meleeDamage;
                                }
                            }
                        }
                        break;
                }
            }
        } */
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
}

public enum CURRENT_SELECTED_WEAPON
{
    RANGE = 0,
    MELEE
}

/*if (Input.GetMouseButtonDown(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            laserLine.SetPosition(0, gunEnd.position);
            if (Physics.Raycast(rayOrigin, fpsCam.ScreenToWorldPoint(Input.mousePosition), out hit, weaponRange))
            { 
                laserLine.SetPosition(1, hit.point);
                ShootableBox health = hit.collider.GetComponent<ShootableBox>();
                if (health != null)
                {
                    health.Damage(gunDamage);
                }
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }*/
