using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolder : MonoBehaviour
{
    [SerializeField] private BaseGun[] guns;
    [SerializeField] private GameInput gameInput;
    private int selectedGun = 0;
    
    void Start()
    {
        foreach(Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }

        guns[selectedGun].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
  
        int tempRotate = gameInput.GetWeaponChange();
        if (tempRotate == 0) return;
        else
        {
            selectedGun += tempRotate;
            if (selectedGun == guns.Length) selectedGun = 0;
            if (selectedGun == -1) selectedGun = guns.Length-1;
        }
        Changegun();

    }

    private void Changegun()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }

        guns[selectedGun].gameObject.SetActive(true);
    }
}
