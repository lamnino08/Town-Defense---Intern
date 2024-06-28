using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent( typeof(NavMeshAgent), typeof(NavMeshObstacle))]
public abstract class ArmyLeagueDynamicMovement : ArmyDynamicMovement
{
    [SerializeField] protected bool isOrder = false;
    [SerializeField] protected Vector3 _orderTager;
    [SerializeField] GameObject _selectCircle;
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
            _navMeshAgent.stoppingDistance = 0;
            DefineEnemy();
            if (target != null)
            {
                _navMeshAgent.stoppingDistance = _distanceStoppingToStop;
                isOrder = false;
                return;
            }
            float distanceToTarget = Vector3.Distance(transform.position, _orderTager);
            if (distanceToTarget > 0.5f)
            {
                if (!_isMoving)
                {
                    _animatorDynamic.Walk();
                    _isMoving = true;
                    StartCoroutine(EnableChase());
                }
                if (_navMeshAgent.enabled)
                {
                    _navMeshAgent.isStopped = false;
                    _navMeshAgent.SetDestination(_orderTager);
                }
            } else
            {
                _navMeshAgent.stoppingDistance = _distanceStoppingToStop;
                isOrder = false;
                if (_isMoving)
                {
                    _animatorCharacter.Idle();
                    _isMoving = false;
                    StartCoroutine(DisEnableChase());
                }
            }
        } else
        {
            _navMeshAgent.stoppingDistance = _distanceStoppingToStop;
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

