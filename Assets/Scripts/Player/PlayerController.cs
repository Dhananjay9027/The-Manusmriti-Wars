using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed; // Serialized to show in Unity inspector
    [SerializeField] private GameInput gameInput; // Input system reference
    [SerializeField] Transform Target;
    private bool IsWalking;

    [SerializeField] private float sprintSpeed = 5f;
    [SerializeField] private float baseSpeed = 2f;
    [SerializeField] private float jumpHeight = 2f; // Height of the jump
    [SerializeField] private float gravity = -9.81f; // Gravity value
    private Vector3 moveDirection; // Movement direction
    private CharacterController characterController; // Reference to CharacterController component
    private float verticalVelocity; // Vertical velocity for gravity and jumping
    private bool isGrounded; // Check if the player is grounded
    [SerializeField] private CameraFocus mainCamera;
    private void Start()
    {
        characterController = GetComponent<CharacterController>(); // Get the CharacterController component
        mainCamera.SetTarget(transform,false);
    }
    private void Update()
    {
        HandleMovement();
        HandleGravity();
    }

    public void SetCamera(bool IsZoom)
    {
         mainCamera.SetTarget(IsZoom ? Target: transform, IsZoom); 
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementNormalized();
        Vector3 moveDir = mainCamera.transform.right * inputVector.x + mainCamera.transform.forward * inputVector.y;
       // Vector3 moveDir = transform.right * inputVector.x + transform.forward* inputVector.y;
        moveDir.y = 0;
        if (gameInput.IsRunning())
        {
            moveDirection = moveDir * sprintSpeed;
        }
        else
        {
            moveDirection = moveDir * baseSpeed;
        }

        IsWalking = moveDir != Vector3.zero;
        
        float rotateSpeed = 30f;
        if (IsWalking)
        {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
     
        if (characterController.isGrounded)
        {
            if (gameInput.IsJumping()) // Check for jump input
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculate the jump velocity
            }
        }
        moveDirection.y = verticalVelocity;
        characterController.Move(moveDirection * Time.deltaTime); // Move the character
    }

    private void HandleGravity()
    {
        if (characterController.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // Small negative value to ensure the player sticks to the ground
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; // Apply gravity
        }
    }

    public bool Is_Walking()
    {
        return IsWalking;
    }

    public bool Is_Running()
    {
        return gameInput.IsRunning() && IsWalking;
    }
    public bool Is_Jumping()
    {
        return gameInput.IsJumping();
    }

}
