using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManagement inputManager;

    private Vector3 _moveDirection;
    [SerializeField] private Transform _cameraObject;
    private Rigidbody _playerRigidbody;
    [SerializeField] private float _walkSpeed = 3;
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _runSpeed = 5;

    [SerializeField] private float _rotateSpeed = 150;
    [SerializeField] private PlayerAnimator playerAnimator;

    private void Awake() 
    {
        inputManager = GetComponent<InputManagement>();
        _playerRigidbody = GetComponent<Rigidbody>();
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
            _moveSpeed = 0;
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
        moveDirection.y = 0;
       if (inputManager.isRunning)
        {
            if (_moveSpeed < _runSpeed)
            {
                _moveSpeed += 10 * Time.deltaTime;
                if (_moveSpeed > _runSpeed)
                {
                    _moveSpeed = _runSpeed;
                }
            }
        }
        else
        {
            if (_moveSpeed < _walkSpeed)
            {
                _moveSpeed += 5 * Time.deltaTime;
                if (_moveSpeed > _walkSpeed)
                {
                    _moveSpeed = _walkSpeed;
                }
            }
            else if (_moveSpeed > _walkSpeed)
            {
                _moveSpeed -= 5 * Time.deltaTime;
                if (_moveSpeed < _walkSpeed)
                {
                    _moveSpeed = _walkSpeed;
                }
            }
        }
        moveDirection *= _moveSpeed * Time.deltaTime;
        transform.position += moveDirection;
    }
}
