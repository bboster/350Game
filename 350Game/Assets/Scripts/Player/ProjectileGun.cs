
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ProjectileGun : MonoBehaviour
{
    //public static ProjectileGun Instance;
    [Tooltip("The prefab spawned from the gun")]
    public GameObject bullet;
    [Tooltip("idk")]
    public float shootForce;
    [Tooltip("idk")]
    public float upwardForce;
    [Tooltip("The time between when the player shoots and can shoot again.")]
    public float timeBetweenShooting;
    [Tooltip("idk")]
    public float spread;
    public float currentSpread;
    [Tooltip("The time it takes to reload the gun")]
    public float reloadTime;
    [Tooltip("The time between bullets from the same button press")]
    public float timeBetweenShots;
    [Tooltip("How many bullets can be shot before needing to reload")]
    public int magazineSize;
    [Tooltip("How many bullets are shot per button press")]
    public float bulletsPerTap;
    [Tooltip("How many bullets the player currently has left")]
    int bulletsLeft;
    int bulletsRight;
    [Tooltip("If the player is shotting or not")]
    bool shooting;
    [Tooltip("Keeps the player from shooting again while the shot function is running")]
    bool readyToShoot;
    [Tooltip("Stops the player from shooting while reloading")]
    bool reloading;

    public Camera fpsCam;
    public Transform attackPoint;

    public bool allowInvoke = true;
    public bool allowHoldButton;
    private int bulletsShot;

    public GameObject muzzleFlash;
    //public TextMeshProUGUI ammunitionDisplay;

    //public int bulletCount;
    //public int maxBullets = 20;
    //public int bulletStandard = 20;
    public PlayerBulletController PlayerBulletController;

    private void Awake()
    {
        /*if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);*/
        // make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Start()
    {
        GameObject controllerObj = GameObject.FindGameObjectWithTag("PlayerBulletController");
        if(controllerObj != null )
        {
            PlayerBulletController = controllerObj.GetComponent<PlayerBulletController>();
        }
    }

    private void Update()
    {
        MyInput();
        //if(ammunitionDisplay != null)
        //{
        // ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        // }
        //PlayerBulletController = PlayerBulletController.Instance;
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
        
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < 100 && !reloading)
        {
            PlayerBulletController.maxBullets = PlayerBulletController.bulletStandard;
            Reload();
        }

        if(readyToShoot && shooting && !reloading && bulletsLeft > 0 && PlayerBulletController.bulletCount < PlayerBulletController.bulletStandard)
        {
            bulletsShot = 0;
            currentSpread = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
       
            readyToShoot = false;
            Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(75);
            }

            Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

            // calculate spread
            //float x = UnityEngine.Random.Range(-spread, spread);
            //float y = UnityEngine.Random.Range(-spread, spread);

            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(currentSpread, 0, 0);
            
            
            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
            currentBullet.transform.forward = directionWithSpread.normalized;
            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
            Destroy(currentBullet, 2f);

            if (muzzleFlash != null)
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
                if (bulletsShot == 2)
                {
                    currentSpread = spread;
                }
                else if (bulletsShot == 1)
                {
                    currentSpread = -spread;
                }
                Invoke("Shoot", timeBetweenShots);
            }
            else
            {
                currentSpread = 0;
            }
        // increase the bullet amount
        PlayerBulletController.bulletCount++;
        // decrease the bullet cap
        PlayerBulletController.maxBullets--;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        PlayerBulletController.bulletCount = 0;
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
