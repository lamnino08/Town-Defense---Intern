using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent( typeof(NavMeshAgent), typeof(NavMeshObstacle))]
public abstract class ArmyDynamicMovement : ArmyMovement, IArmyMovement, ICharacterMovement, ICharacterDynamicMovement
{
    protected NavMeshAgent _navMeshAgent;
    protected NavMeshObstacle _navMeshObstacle;
    protected IDynamicAnimator _animatorDynamic;
    [SerializeField] protected float _distanceStoppingToStop;
    [SerializeField] protected bool _isMoving = false;
    [SerializeField] protected bool _isAttacking = false;
    protected bool _isStartEnabelChase = false;
    protected bool _isStartDisEnabelChase = false;
    protected bool _isWaitForDefineEnegy = false;
    protected bool _isStartWaitForDefineEnegy = false;
    [SerializeField] protected Transform _previouseTarget;

    protected override void Start() 
    {
        base.Start();
        _animatorDynamic = GetComponentInChildren<ArmyDynamicAnimator>();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();

        StartCoroutine(EnableChase());   
        _navMeshAgent.stoppingDistance = _distanceStoppingToStop;
        _navMeshObstacle.carving = true;
    }   

    protected virtual void Update()
    {
        ChaseTarget();
    }

    public void ChaseTarget()
    {
        if (target != null)
        {
            DefineEnemy();
            if (target != _previouseTarget)
            {
                if (_isAttacking)
                {
                    _attack.StopActtack();
                    _isAttacking = false;
                    _animatorCharacter.Idle();
                    _isAttacking = false;
                }
                _previouseTarget = target;
            }

            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget > _navMeshAgent.stoppingDistance)
            {
                if (_navMeshAgent.enabled)
                {
                    _navMeshAgent.isStopped = false;
                    _navMeshAgent.SetDestination(target.position);

                    if (_isAttacking)
                    {
                        _isAttacking = false;
                        _attack.StopActtack();
                    } 

                    RunState();

                    _isStartEnabelChase = false;
                } else 
                {
                    _isStartDisEnabelChase = false;
                    if (!_isStartEnabelChase)
                    {
                        StartCoroutine(EnableChase());
                    }
                }      
            }
            else
            {
                DirectToTarget();
                if (_navMeshAgent.enabled)
                {
                    if (!_isStartDisEnabelChase)
                    {
                        StartCoroutine(DisEnableChase());
                    }
                } 
                if (!_isAttacking)
                {
                    _isAttacking = true;
                    _attack.Acttack(target.gameObject);
                }

                IdleState();
            }
            _isStartWaitForDefineEnegy = true;
        }
        else
        {
            if (_isAttacking)
            {
                _isAttacking = false;
                _animatorCharacter.Idle();
                _attack.StopActtack();
            }

            IdleState();

            if (!_isStartEnabelChase)
            {
                StartCoroutine(EnableChase());
            }
            
            DefineEnemy();
        }
    }

   

    public override void DirectToTarget()
    {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void IdleState()
    {
        if (_isMoving)
        {
            _isMoving = false;
            _animatorCharacter.Idle();
        }
    }

    private void RunState()
    {
        if (!_isMoving)
        {
            _animatorDynamic.Run();
            _isMoving = true;
        }
    }

    public IEnumerator WaitForDefineEnemy()
    {
        yield return new WaitForSeconds(1f);
        _isWaitForDefineEnegy = false;
    }
    
    public IEnumerator EnableChase()
    {
        _isStartEnabelChase = true;
        _navMeshObstacle.enabled = false;
        yield return new WaitForSeconds(0.05f);
        _navMeshAgent.enabled = true;
        _navMeshAgent.isStopped = false;
    }

    public IEnumerator DisEnableChase()
    {
        _isStartDisEnabelChase = true;
        _navMeshAgent.enabled = false;
        yield return new WaitForSeconds(0.05f);
        _navMeshObstacle.enabled = true;
    }
}

