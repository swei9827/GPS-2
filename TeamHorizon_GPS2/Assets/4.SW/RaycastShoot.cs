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

    void Start () {
        laserLine = GetComponent<LineRenderer>();
        //gunAudio = GetComponent<AudioSource>();
    }

	void Update () {
        //Debug.Log(Input.acceleration.y);
        //Debug.Log(Input.acceleration.x);

        if (new Vector2(Input.acceleration.x, Input.acceleration.z).magnitude > 1.5)
        {
            gunBullet = maxBullet;
            bulletLeft.text = gunBullet.ToString();
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
        if (Input.GetMouseButtonDown(0) && Time.time > nextFire && gunBullet>0)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
            Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
            Vector3 mousePosF = Camera.main.ScreenToWorldPoint(mousePosFar);
            Vector3 mousePosN = Camera.main.ScreenToWorldPoint(mousePosNear);
            RaycastHit hit;
            laserLine.SetPosition(0, gunEnd.position);
            gunBullet--;
            bulletLeft.text = gunBullet.ToString();
            if (Physics.Raycast(mousePosN,mousePosF-mousePosN,out hit,weaponRange))
            {
                laserLine.SetPosition(1, hit.point);
                Instantiate(effect,hit.point, transform.rotation);
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

    private IEnumerator ShotEffect()
    {
        //gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
