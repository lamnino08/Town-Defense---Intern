using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent( typeof(NavMeshAgent), typeof(NavMeshObstacle))]
public abstract class ArmyLeagueDynamicMovement : ArmyDynamicMovement, ICharacterUnit
{
    protected bool isOrder = false;
    protected Vector3 _orderTager;
    GameObject _selectCircle;
    protected override void Start()
    {
        base.Start();
        _selectCircle = transform.GetChild(0).gameObject;
        _selectCircle.SetActive(false);
    }
    protected override void Update()
    {
        if (isOrder)
        {
            DefineEnemy();
            if (target != null)
            {
                isOrder = false;
                return;
            }
            float distanceToTarget = Vector3.Distance(transform.position, _orderTager);
            if (distanceToTarget > 0.5f)
            {
                if (!_isMoving)
                {
                    _animatorDynamic.Run();
                    _isMoving = true;
                    StartCoroutine(EnableChase());
                }

                NavMeshPath path = new NavMeshPath();
                if (_navMeshAgent.enabled && NavMesh.CalculatePath(transform.position, _orderTager, NavMesh.AllAreas, path))
                {
                    _navMeshAgent.isStopped = false;
                    _navMeshAgent.SetDestination(_orderTager);
                }
            } else
            {
                isOrder = false;
                if (_isMoving)
                {
                    _animatorCharacter.Idle();
                    _isMoving = false;
                    // _navMeshAgent.enabled = false;
                    // StartCoroutine(DisEnableChase());
                }
            }
        } else
        {
            // _navMeshAgent.stoppingDistance = _distanceStoppingToStop;
            ChaseTarget();
        }
    }

    public void SetOrderPostion(Vector3 target)
    {
        if (!_isAttacking)
        {
            isOrder = true;
            _orderTager = new Vector3(target.x, 0, target.z);
        }
    }

    public void SetSelect(bool isSelect)
    {   
        if (!isSelect) 
        {
            _selectCircle.SetActive(isSelect);
            return;
        }
        if (!_isAttacking)
        {
            _selectCircle.SetActive(isSelect);
        }
    }
}

