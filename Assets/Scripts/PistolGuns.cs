using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolGuns : BaseGun
{
    // Start is called before the first frame update
    void Start()
    {
        SetFireRate(1);
        SetNoOfBulletsForGun(30);
        SetMagzineLimit(15);
        
    }

   
}
