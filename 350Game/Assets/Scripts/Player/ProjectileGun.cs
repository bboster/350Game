
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ProjectileGun : MonoBehaviour
{
    public GameObject bullet;
    public float shootForce;
    public float upwardForce;
    public float timeBetweenShooting;
    public float spread;
    public float reloadTime;
    public float timeBetweenShots;
    public int magazineSize;
    public float bulletsPerTap;
    int bulletsLeft;
    int bulletsRight;
    bool shooting;
    bool readyToShoot;
    bool reloading;

    public Camera fpsCam;
    public Transform attackPoint;

    public bool allowInvoke = true;
    public bool allowHoldButton;
    private int bulletsShot;

    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    private void Awake()
    {
        // make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
        if(ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        }
    }

    private void MyInput()
    {
        // check if allowed to hold down button
        if (allowHoldButton)
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        // calculate spread
        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
        Destroy(currentBullet, 2f);

        if(muzzleFlash != null)
        {
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);    
        }

        bulletsLeft--;
        bulletsShot++;

        /// invoke reset shot function
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        // if more than one bullets per tap, make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    IEnumerator DeleteBulletTimer()
    {
        yield return new WaitForSeconds(3f);
    }
}
