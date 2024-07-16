using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputManagement inputManager;

    [SerializeField] private Transform _cameraObject;
    [SerializeField] private float _moveSpeed = 3;
    // [SerializeField] private float _runSpeed = 5;

    [SerializeField] private float _rotateSpeed = 150;
    [SerializeField] private PlayerAnimator playerAnimator;

    private void Awake() 
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Update() 
    {
        HandleAllMovement();
    }

    public void HandleAllMovement()
    {
        HandleMoveMent();
        HandleRotation();
    }

    private void HandleMoveMent()
    {
        if (inputManager.verticalInput != 0)
        {
            if (inputManager.isRunning)
            {
                playerAnimator.Run();
            } else 
            {
                playerAnimator.Walk();
            }
            Move();
        } else 
        {
            playerAnimator.Idle();
        }
    }

    private void HandleRotation()
    {
        float rotationAmount = 0f;
        if (inputManager.horizontalInput < 0) // Nhấn phím A
        {
            rotationAmount = -_rotateSpeed * Time.deltaTime;
        }
        else 
        if (inputManager.horizontalInput > 0) // Nhấn phím D
        {
            rotationAmount = _rotateSpeed * Time.deltaTime;
        }

        transform.Rotate(0, rotationAmount, 0);
    }

    private void Move()
    {
        Vector3 moveDirection = transform.forward;
        // moveDirection.y = 0;
        moveDirection *= _moveSpeed * Time.deltaTime;
        transform.position += moveDirection;
    }
}
