using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastShoot : MonoBehaviour
{ 
    public Weapon weapon;
    public GameObject reloadIndicator;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;
    public GameObject effect;
    private bool reloading;
    public List<Image> bulletList; // test purpose
    public int meleeDamage; 

    public Text bulletLeft;

    private Vector3 firstPos = Vector3.zero; // First Position
    private Vector3 lastPos = Vector3.zero;  // Last Position
    private float dragDistance;  //minimum distance for a swipe to be registered

    void Start()
    {
        dragDistance = Screen.height * 0.15f;
        laserLine = GetComponent<LineRenderer>();
        bulletLeft.text = weapon.currentAmmo.ToString() + " / " + weapon.maxAmmo.ToString();
        reloading = false;
        gunAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Debug.Log(Input.acceleration.y);
        //Debug.Log(Input.acceleration.x);

        if (/*(new Vector2(Input.acceleration.x, Input.acceleration.z).magnitude > 2 */reloading == false && Input.GetButtonDown("Jump")) // reload
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
        for (int i = 0; i < bulletList.Count; i++)
        {
            bulletList[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < weapon.currentAmmo; i++)
        {
            bulletList[i].gameObject.SetActive(true);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
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
                    //if(firstPos.x >= (Screen.Width-200) && firstPos.y <= (Screen.Height - 800))
                    //{
                    //    swap weapon //!
                    //}
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
                            closest.GetComponent<EnemyHP>().hp -= meleeDamage;
                        }
                    }
                    else
                    {
                        if (Time.time > nextFire && weapon.currentAmmo > 0)
                        {
                            nextFire = Time.time + weapon.fireRate;
                            StartCoroutine(ShotEffect());
                            Vector3 posFar = new Vector3(touch.position.x, touch.position.y, Camera.main.farClipPlane);
                            Vector3 posNear = new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane);
                            Vector3 posF = Camera.main.ScreenToWorldPoint(posFar);
                            Vector3 posN = Camera.main.ScreenToWorldPoint(posNear);
                            RaycastHit hit;
                            laserLine.SetPosition(0,posN);
                            weapon.currentAmmo--;
                            bulletLeft.text = weapon.currentAmmo.ToString() + " / " + weapon.maxAmmo.ToString(); ;
                            if (Physics.Raycast(posN, posF - posN, out hit, weapon.weaponRange))
                            {
                                laserLine.SetPosition(1, hit.point);
                                Instantiate(effect, hit.point, transform.rotation);
                                if (hit.collider.CompareTag("Enemy"))
                                {
                                    int targetHP = hit.collider.gameObject.GetComponent<EnemyHP>().hp;
                                    targetHP -= weapon.gunDamage;
                                }
                                else
                                {

                                }
                            }
                            else
                            {
                                laserLine.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                            }
                        }
                    }
                    break;
            }
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
