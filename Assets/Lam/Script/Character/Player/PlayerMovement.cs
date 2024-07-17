using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputManagement inputManager;
    [SerializeField] private float _moveSpeed = 3;
    [SerializeField] private float _rotateSpeed = 150;
    [SerializeField] private PlayerAnimator playerAnimator;
     [SerializeField] private NavMeshAgent _navmeshAgent;
    [SerializeField] private LayerMask _EnemyMask;
    [SerializeField] private LayerMask _natureMask;
    private Vector3 _destination;
    private Transform _natureTarget;
    [SerializeField] private bool isMoving = false;

    private void Awake() 
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        _navmeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() 
    {
        HandleMovement();
    }

    public void HandleMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _navmeshAgent.isStopped = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _natureMask))
            {
                _natureTarget = hitInfo.transform;
            } else
            {
                _natureTarget = null;
            }

            _destination = PlacementSystem.instance.GetPositionGrid();
            Debug.Log(_destination);
            _navmeshAgent.SetDestination(_destination);

            if (!isMoving)
            {
                playerAnimator.Run();
                isMoving = true;
            }
        }

        float distanceDestination = Vector3.Distance(transform.position, _destination);
        if (distanceDestination < 0.2f && isMoving)
        {
            isMoving = false;
            playerAnimator.Idle();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_natureTarget != null && other.transform == _natureTarget)
        {
            _navmeshAgent.isStopped = true;
            playerAnimator.Attack();
            isMoving = false;
        }
    }
}
