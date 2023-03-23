using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameInput gameInput;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float gravity = -10f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    void Start()
    {
        controller = GetComponent < CharacterController> ();

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementDirectionNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        moveDir = transform.right * moveDir.x + transform.forward * moveDir.z;
        
        controller.Move(moveDir*Time.deltaTime*moveSpeed);
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundLayer);
        
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        if (isGrounded)
        {
            if (gameInput.GetJumpButtonPressed())
            {
                Jump();
            }
        }
        else
        {
            velocity.y += gravity*Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }
    private void Jump()
    {
        
        velocity.y = jumpForce;
    }
}
