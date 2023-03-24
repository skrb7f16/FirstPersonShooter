using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubAutomaticGun : BaseGun
{
   
    void Start()
    {
        SetFireRate(10);
        SetNoOfBulletsForGun(120);
        SetMagzineLimit(30);
        SetImpactOfBullet(200);
        SetReloadTime(3);
    }

    
   


    
}
