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
    
    [SerializeField] private LayerMask _EnemyMask;
    [SerializeField] private LayerMask _natureMask;
    [SerializeField] private LayerMask _pointMask;
    private Vector3 _destination;
    [SerializeField] private Transform _natureTarget;
    private bool isMoving = false;
    private PlayerWork _playerWork;
    private PlayerAnimator _playerAnimator;
    private NavMeshAgent _navmeshAgent;
    private PlayerAudio _playerAudio;
    private float rotationDuration = 0.6f;
    private float rotationTime = 0f;

    private void Awake() 
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _navmeshAgent = GetComponent<NavMeshAgent>();
        _playerWork = GetComponent<PlayerWork>();
        _playerAudio = GetComponent<PlayerAudio>();
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
                if (hitInfo.transform != _natureTarget)
                {
                    if (_natureTarget != null)
                    {
                        _playerWork.StopManufacture();
                    }
                    _natureTarget = hitInfo.transform;
                } 
            } else
            {
                if (_natureTarget != null)
                {
                    _playerWork.StopManufacture();
                    _natureTarget = null;
                }
            }
        }

        float vertical = _inputManager.verticalInput;
        float horizontal = _inputManager.horizontalInput;

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        if (movement != Vector3.zero)
        {
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
        }



        if (_natureTarget == null)
        {
            _playerWork.StopManufacture();
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, 1, _natureMask);
        foreach(Collider e in colliders)
        {
            if (e.transform == _natureTarget && isMoving)
            {
                isMoving = false;
                _playerAnimator.Idle();
                _playerAudio.Move(false);

                _navmeshAgent.isStopped = true;
                _playerWork.Manufacture(_natureTarget);

                Vector3 targetPosition = _natureTarget.position;
                targetPosition.y = transform.position.y; 
                transform.LookAt(targetPosition);

                break;
            }
        }

        if (isMoving && Vector3.Distance(transform.position, _destination) < 0.1f)
        {
            _playerAnimator.Idle();
            _playerAudio.Move(false);
            isMoving = false;
        }
            
    }

    // private void MoveToTarget()
    // {
    //     _navmeshAgent.isStopped = false;
    //     _destination = PlacementSystem.instance.GetPositionGrid();
    //     _navmeshAgent.SetDestination(_destination);

    //     if (!isMoving)
    //     {
    //         _playerAnimator.Run();
    //         _playerAudio.Move(true);
    //         isMoving = true;
        
    //     }
    // }

    private void Move(Vector3 movement)
    {
        if (!isMoving)
        {
            _playerAnimator.Run();
            _playerAudio.Move(true);
            isMoving = true;
        };

        // Di chuyển nhân vật
        // _navmeshAgent.isStopped = false;
        // _navmeshAgent.velocity = movement * _moveSpeed;
        transform.position += movement * _moveSpeed * Time.deltaTime;
        // Xoay nhân vật theo hướng di chuyển
        Quaternion newRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationDuration);
    }
}
