using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private Transform player;
    
    private float xRotation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleCameraMovement();
    }

    private void HandleCameraMovement()
    {
        Vector2 inputVector = gameInput.GetLookAroundDirectionNormalized();
        float mouseX = inputVector.x*sensitivity;
        float mouseY = inputVector.y*sensitivity;
        xRotation -= mouseY * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80, 80);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        player.Rotate(Vector3.up*mouseX * Time.deltaTime);
    }
}
