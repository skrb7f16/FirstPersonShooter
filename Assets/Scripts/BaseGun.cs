using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BaseGun : MonoBehaviour
{
    private const string RELOAD = "reload";
    private const string IS_SHOOTING = "shooting";
    
    [SerializeField] private Transform cam;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactFromGunGameObject;
    [SerializeField] private TextMeshProUGUI ammo;
    private float impactForceOfBullet = 150;
    private float range = 100;
    private float fireRate = 1;
    private float nextTimeToFire = 0;
    private float reloadTime = 2f;

    private int totalBulletsForNewWeapon = 10;
    private int currNoOfBullets = 10;
    private int magzineLimit = 5;

    private bool canShoot = true;
    private bool isShooting;

    private string shootSound = "Shoot";

    private void OnEnable()
    {
        if (currNoOfBullets > 0)
        {
            canShoot = true;
            transform.parent.GetComponent<Animator>().SetBool(RELOAD, !canShoot);
        }
    }
    void Update()
    {
        if (!canShoot)
        {
            isShooting = false;
            return;
        }
        transform.parent.GetComponent<Animator>().SetBool(IS_SHOOTING, isShooting);
        if (gameInput.GetShootDownButton() && Time.time >= nextTimeToFire)
        {
            isShooting = true;
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();

        }
        else
        {
            isShooting = false;
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

                AudioManager.instance.PlaySound(shootSound);
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


    public void SetReloadTime(float time)
    {
        reloadTime = time;
    }

    private bool CanFire()
    {
        
        currNoOfBullets--;
        if (currNoOfBullets <= 0)
        {
            if (totalBulletsForNewWeapon > 0)
            {
                StartCoroutine(Reload());
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

    protected void SetShootSound(string sound)
    {
        shootSound = sound;
    }
    
    IEnumerator Reload()
    {
        AudioManager.instance.PlaySound("Reload");
        canShoot = false;
        transform.parent.GetComponent<Animator>().SetBool(RELOAD, !canShoot);
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
        transform.parent.GetComponent<Animator>().SetBool(RELOAD, !canShoot);

    }
}
