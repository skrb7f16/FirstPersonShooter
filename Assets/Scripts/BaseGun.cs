using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BaseGun: MonoBehaviour
    
{
    [SerializeField]private Transform cam;
    [SerializeField]private GameInput gameInput;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactFromGunGameObject;
    [SerializeField] private TextMeshProUGUI ammo;
    private float impactForceOfBullet = 150;
    private float range = 100;
    private float fireRate = 1;
    
    private float nextTimeToFire = 0;
    private int totalBulletsForNewWeapon = 10;
    private int currNoOfBullets = 10;
    private int magzineLimit = 5;
    


    void Update()
    {
        
        if (gameInput.GetShootDownButton() && Time.time>=nextTimeToFire)
        {
            
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();

        }
        
    }

    private void LateUpdate()
    {
        ammo.text = currNoOfBullets.ToString() + "/" + totalBulletsForNewWeapon.ToString();
    }
    protected void Fire()
    {

  
        
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit))
        {
            
                if (CanFire())
                {
                    
                    AudioManager.instance.PlaySound("Shoot");
                    if (muzzleFlash != null)
                    {
                        muzzleFlash.Play();
                    }
                    if (hit.rigidbody != null)
                    {

                        hit.rigidbody.AddForce(-hit.normal * impactForceOfBullet);
                    }
                    if (hit.collider != null)
                    {
                        Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
                        GameObject tempImpact = Instantiate(impactFromGunGameObject, hit.point, impactRotation);
                        tempImpact.transform.parent = hit.transform;
                        Destroy(tempImpact, 5f);
                    }
                    

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

    public void SetFireRate(float fireRate)
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


    private bool CanFire()
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
            else
            {
                currNoOfBullets = 0;
                totalBulletsForNewWeapon = 0;
                return false;
            }
            
            
        }
        return true;
    }

    private void Reload()
    {
        
    }
}
