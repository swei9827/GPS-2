using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCastShoot : MonoBehaviour {

    public int gunDamage = 1;
    public float fireRate = .25f;
    public float weaponRange = 500f;
    public int gunBullet = 6;
    public int maxBullet = 6;
    public Transform gunEnd;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    //private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;
    public GameObject effect;

    public Text bulletLeft;

    private Vector3 firstPos = Vector3.zero; // First Position
    private Vector3 lastPos = Vector3.zero;  // Last Position
    private float dragDistance;  //minimum distance for a swipe to be registered

    void Start () {
        dragDistance = Screen.height * 0.15f;
        laserLine = GetComponent<LineRenderer>();
        //gunAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Debug.Log(Input.acceleration.y);
        //Debug.Log(Input.acceleration.x);

        if (new Vector2(Input.acceleration.x, Input.acceleration.z).magnitude > 1.5) // reload
        {
            gunBullet = maxBullet;
            bulletLeft.text = gunBullet.ToString();
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

                    if (Mathf.Abs(lastPos.x - firstPos.x) > dragDistance || Mathf.Abs(lastPos.y - firstPos.y) > dragDistance)
                    {
                        // perform melee
                    }
                    else
                    {
                        if (Time.time > nextFire && gunBullet > 0)
                        {
                            nextFire = Time.time + fireRate;
                            StartCoroutine(ShotEffect());
                            Vector3 posFar = new Vector3(touch.position.x, touch.position.y, Camera.main.farClipPlane);
                            Vector3 posNear = new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane);
                            Vector3 posF = Camera.main.ScreenToWorldPoint(posFar);
                            Vector3 posN = Camera.main.ScreenToWorldPoint(posNear);
                            RaycastHit hit;
                            laserLine.SetPosition(0, gunEnd.position);
                            gunBullet--;
                            bulletLeft.text = gunBullet.ToString();
                            if (Physics.Raycast(posN, posF - posN, out hit, weaponRange))
                            {
                                laserLine.SetPosition(1, hit.point);
                                Instantiate(effect, hit.point, transform.rotation);
                                if (hit.collider.CompareTag("Enemy"))
                                {
                                    int targetHP = hit.collider.gameObject.GetComponent<EnemyHP>().hp;
                                    targetHP = targetHP - gunDamage;
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
        //gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
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
