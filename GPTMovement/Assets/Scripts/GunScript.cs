using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 200f;
    public float fireRate = 5f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject impactEffectWall;
    public GameObject impactBH;

    public AudioClip shotSFX;
    public AudioSource _audioSource;

    private float nextTimeToFire = 0;
    public int maxAmmo = 30;
    public int currentAmmo;
    public float reloadTime = 2.5f;

    private bool isReloading = false;

    public AmmoUI ammoUI;

    //private GameObject[] bulletList;
    //public int number;

    //bool fixedSize;

    void Start()
    {
        currentAmmo = maxAmmo;
        ammoUI = GameObject.FindGameObjectWithTag("AmmoUI").GetComponent<AmmoUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (Input.GetKeyDown("r") || currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            StartCoroutine(ammoUI.setToMax());
            return;
        }

        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            currentAmmo--;
            ammoUI.showAmmo(1);
        }

        //bulletList = GameObject.FindGameObjectsWithTag("Bullet Hole");

        //fixedSize = bulletList.IsFixedSize; // checking if the problem is in the array itself

        //int length = bulletList.Length; // getting the array length

        //if (fixedSize)
        //{
        //    Debug.Log("YEs");
        //    Debug.Log(length);
        //}

        //else
        //{
        //    Debug.Log("No");
        //}

        //for (int i = 0; i < length; i++)
        //{
        //    Debug.Log(bulletList[i]); // add this part and ones above to the shooting function
        //}

    }

    void Shoot()
    {
        _audioSource.PlayOneShot(shotSFX);

        muzzleFlash.Play();

        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            EnemyTarget target = hit.transform.GetComponent<EnemyTarget>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            if (hit.transform.tag == "Enemy")
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 2f);
            }

            else
            {
                GameObject impactWallGO = Instantiate(impactEffectWall, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactWallGO, 2f);

                GameObject impactBHGO = Instantiate(impactBH, hit.point, Quaternion.LookRotation(hit.normal));
                impactBHGO.transform.position += impactBHGO.transform.forward / 1000;
                Destroy(impactBHGO, 10f);
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Reloading");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;

        isReloading = false;
    }
}