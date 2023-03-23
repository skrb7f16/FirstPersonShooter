using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperGun : BaseGun
{
    // Start is called before the first frame update
    void Start()
    {
        
        SetFireRate(1f);
        SetNoOfBulletsForGun(25);
        SetMagzineLimit(5);
    }


    
}
