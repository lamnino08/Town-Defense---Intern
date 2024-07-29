using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputManagement _inputManager;
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _rotateSpeed = 1000;
    
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private LayerMask _natureMask;
    [SerializeField] private Transform _natureTarget;
    [SerializeField] private GameObject _enemyTarget;
    private bool isMoving = false;
    private PlayerWork _playerWork;
    private PlayerAnimator _playerAnimator;
    private PlayerAudio _playerAudio;
    private PlayerAttack _playerAttack;
    private float rotationDuration = 0.6f;
    private bool _isAttacking = false;
    private bool _isWorking = false;

    private void Awake() 
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerWork = GetComponent<PlayerWork>();
        _playerAudio = GetComponent<PlayerAudio>();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update() 
    {
        HandleMovement();
    }

    public void HandleMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() || Input.GetKey(KeyCode.LeftShift))
            {
                return; // Exit if the click is on UI
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo,  Mathf.Infinity, _natureMask))
            {
                if (hitInfo.transform == _natureTarget) return;
                Collider[] colliders = Physics.OverlapSphere(transform.position, 2f, _natureMask);
                if (colliders.Length > 0)
                {
                    foreach(Collider e in colliders)
                    {
                        if (e.transform == hitInfo.transform)
                        {
                            _natureTarget = hitInfo.transform;
                            _isWorking = true;
                            _playerWork.Manufacture(hitInfo.transform);
                            break;
                        }
                    }
                }
            } else
            {
                _playerWork.StopManufacture();
                DefineEnemy();
                
            }
        }

        if (_enemyTarget != null)
        {
            if (!_isAttacking) 
            {
                _isAttacking = true;
                _playerAttack.Acttack(_enemyTarget);
            } else
            {
                if (Vector3.Distance(transform.position, _enemyTarget.transform.position) > 20)
                {
                     _isAttacking = false;
                    _playerAttack.StopActtack();
                    DefineEnemy();   
                }
            }     
        } else
        {
                _isAttacking = false;
            _playerAttack.StopActtack();
        }

        float vertical = _inputManager.verticalInput;
        float horizontal = _inputManager.horizontalInput;

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        if (movement != Vector3.zero)
        {
            _isWorking = false;
            _playerWork.StopManufacture();
            Move(movement);
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                _playerAnimator.Idle();
                _playerAudio.Move(false);
            }

            if (_natureTarget == null)
            {
                _isWorking = false;
                _playerWork.StopManufacture();
            }
        }
    }
    private void Move(Vector3 movement)
    {
        if (!isMoving)
        {
            _playerAnimator.Run();
            _playerAudio.Move(true);
            isMoving = true;
        }

        // Rotate movement vector by 45 degrees
        Quaternion rotationOffset = Quaternion.Euler(0, -45, 0);
        Vector3 adjustedMovement = rotationOffset * movement;

        // Move the character
        transform.position += adjustedMovement * _moveSpeed * Time.deltaTime;

        // Rotate character towards the new movement direction
        Quaternion newRotation = Quaternion.LookRotation(adjustedMovement);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationDuration);
    }

    private void DefineEnemy()
    {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 20f, _enemyMask);

                if (hitColliders.Length > 0)
                {
                    Collider nearestCollider = hitColliders[0];
                    float minDistance = Vector3.Distance(transform.position, nearestCollider.transform.position);

                    foreach (var collider in hitColliders)
                    {
                        if (collider.transform == _enemyTarget) break;
                        float distance = Vector3.Distance(transform.position, collider.transform.position);
                        if (distance < minDistance)
                        {
                            nearestCollider = collider;
                            minDistance = distance;
                        }
                    }

                    _enemyTarget = nearestCollider.gameObject;
                }
        
    }

}
