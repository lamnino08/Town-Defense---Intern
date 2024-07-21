using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputManagement inputManager;
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _rotateSpeed = 150;
    [SerializeField] private PlayerWork _playerWork;
    [SerializeField] private PlayerAnimator _playerAnimator;
     [SerializeField] private NavMeshAgent _navmeshAgent;
    [SerializeField] private LayerMask _EnemyMask;
    [SerializeField] private LayerMask _natureMask;
    private Vector3 _destination;
    [SerializeField] private Transform _natureTarget;
    [SerializeField] private bool isMoving = false;

    private void Awake() 
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _navmeshAgent = GetComponent<NavMeshAgent>();
        _playerWork = GetComponent<PlayerWork>();
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
                if (hitInfo.transform != _natureTarget || hitInfo.transform.CompareTag("plane"))
                {
                    if (_natureTarget != null)
                    {
                        _playerWork.StopManufacture(_natureTarget);
                    }
                    _natureTarget = hitInfo.transform;
                    MoveToTarget();
                } 
            } else
            {
                if (_natureTarget != null)
                {
                    _playerWork.StopManufacture(_natureTarget);
                    _natureTarget = null;
                }
            }
        }

        float distanceDestination = Vector3.Distance(transform.position, _destination);
        if (distanceDestination < 0.2f && isMoving)
        {
            isMoving = false;
            _playerAnimator.Idle();
        }
    }

    private void MoveToTarget()
    {
        _navmeshAgent.isStopped = false;
        _destination = PlacementSystem.instance.GetPositionGrid();
        _navmeshAgent.SetDestination(_destination);

        if (!isMoving)
        {
            _playerAnimator.Run();
            isMoving = true;
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_natureTarget != null && other.transform == _natureTarget)
        {
            _playerAnimator.Idle();
            _navmeshAgent.isStopped = true;
            isMoving = false;
            

            _playerWork.Manufacture(_natureTarget);

            Vector3 targetPosition = _natureTarget.position;
            targetPosition.y = transform.position.y; 
            transform.LookAt(targetPosition);
        }
    }
}
