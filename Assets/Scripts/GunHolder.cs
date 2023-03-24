using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GunHolder : MonoBehaviour
{
    private const string IS_SCOPED = "scoped";

    [SerializeField] private BaseGun[] guns;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Image crossHair;
    [SerializeField] private Camera cam;
    private int selectedGun = 0;
    private bool scoped;
    
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

        if (gameInput.GetScopeButtonDown())
        {
            ScopeUnScopeFunction();
        }
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

    private void ScopeUnScopeFunction()
    {
        scoped = !scoped;
        GetComponent<Animator>().SetBool(IS_SCOPED, scoped);
        if (scoped)
        {
            cam.fieldOfView = 40;
        }
        else
        {
            cam.fieldOfView = 60;
        }
    }

    
}
