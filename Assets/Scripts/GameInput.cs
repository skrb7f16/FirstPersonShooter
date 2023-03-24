using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInput inputActions;
    void Start()
    {
        inputActions = new PlayerInput();
        inputActions.OnFoot.Enable();
     
    }

  

    void Update()
    {
        
    }
    public Vector2 GetMovementDirectionNormalized()
    {
        Vector2 inputVector = inputActions.OnFoot.Movement.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
    public Vector2 GetLookAroundDirectionNormalized()
    {
        Vector2 inputVector = inputActions.OnFoot.LookAround.ReadValue<Vector2>();
        //inputVector = inputVector.normalized;
        return inputVector;
    }

    public bool GetJumpButtonPressed()
    {
        
        float pressed = inputActions.OnFoot.Jump.ReadValue<float>();
        return pressed==1;
    }
    public bool GetShootDownButton()
    {
        float pressed = inputActions.OnFoot.Shoot.ReadValue<float>();

        return pressed == 1;
    }
    
    public bool GetScopeButtonDown()
    {
        return inputActions.OnFoot.Scope.triggered;
    }
    public int GetWeaponChange()
    {
        return (int)inputActions.OnFoot.WeaponChange.ReadValue<float>()/120;

   
    }
}
