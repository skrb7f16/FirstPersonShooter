using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun: MonoBehaviour
    
{
    [SerializeField]private Transform cam;
    [SerializeField]private GameInput gameInput;
    [SerializeField] private ParticleSystem muzzleFlash;
    private float impactForceOfBullet = 150;
    private float range = 20;
    private int fireRate = 1;
    private float nextTimeToFire = 0;
    private int totalBulletsForNewWeapon = 10;
    private int currNoOfBullets = 10;
    private int magzineLimit = 5;
    
    void Update()
    {
        if (gameInput.GetShootDownButton() && Time.time>=nextTimeToFire)
        {
            nextTimeToFire = (Time.time + 1f) / fireRate;
            Fire();
        }
    }
    protected void Fire()
    {
        currNoOfBullets--;
        if (currNoOfBullets <= 0)
        {
            if (totalBulletsForNewWeapon > 0)
            {
                if (totalBulletsForNewWeapon > magzineLimit)
                {
                    currNoOfBullets = magzineLimit;
                    totalBulletsForNewWeapon -= magzineLimit;
                }
                else
                {
                    currNoOfBullets = totalBulletsForNewWeapon;
                    totalBulletsForNewWeapon = 0;
                }
            }
            return;
        }
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, range))
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForceOfBullet);
            }
        }
    }

    public void SetImpactOfBullet(float impact)
    {
        impactForceOfBullet = impact;
    }

    public float GetImpactOfBullet()
    {
        return impactForceOfBullet;
    }

    public void SetRangeOfGun(float range)
    {
        this.range = range;
    }

    public float GetRangeOfGun()
    {
        return range;
    }

    public void SetShootingPosition(Transform pos)
    {
        cam = pos;
    }

    public void SetFireRate(int fireRate)
    {
        this.fireRate = fireRate;
    }



    public void SetNoOfBulletsForGun(int bullets)
    {
        totalBulletsForNewWeapon = bullets;
       
    }

    public void SetMagzineLimit(int bullets)
    {
        magzineLimit = bullets;
        currNoOfBullets = magzineLimit;
        totalBulletsForNewWeapon -= magzineLimit;
    }

}
