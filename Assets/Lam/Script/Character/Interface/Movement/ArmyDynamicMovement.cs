using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent( typeof(NavMeshAgent), typeof(NavMeshObstacle))]
public abstract class ArmyDynamicMovement : ArmyMovement, ICharacterMovement, ICharacterDynamicMovement
{
    [SerializeField] protected LayerMask _layerAttack;
    [SerializeField] protected float _distanceStoppingToStop;
    [SerializeField] protected float speedMove;
    protected NavMeshAgent _navMeshAgent;
    protected NavMeshObstacle _navMeshObstacle;
    protected IDynamicAnimator _animatorDynamic;
    protected bool _isMoving = false;
    protected bool _isAttacking = false;
    protected bool _isStartEnabelChase = false;
    protected bool _isStartDisEnabelChase = false;
    protected bool _isWaitForDefineEnegy = false;
    protected bool _isStartWaitForDefineEnegy = false;
    
    protected Transform _previouseTarget;

    protected override void Start() 
    {
        base.Start();
        _animatorDynamic = GetComponentInChildren<ArmyDynamicAnimator>();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();

        // StartCoroutine(EnableChase());   
        _navMeshAgent.speed = speedMove;
        _navMeshObstacle.carving = true;
    }   

    protected virtual void Update()
    {
            if (_navMeshAgent.enabled && _navMeshObstacle.enabled) _navMeshObstacle .enabled = false;
        ChaseTarget();
    }

    public virtual void ChaseTarget()
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

            

            Collider[] colliders = Physics.OverlapSphere(transform.position, _distanceStoppingToStop, _layerAttack);
            Transform enemyClose = IsTarget(colliders);
            if (colliders.Length != 0 && enemyClose)
            {
                DirectToTarget();
                if (_navMeshAgent.enabled)
                {
                    if (!_isStartDisEnabelChase)
                    {
                        StartCoroutine(DisEnableChase());
                    }
                    _isStartEnabelChase = false;
                } 
                if (!_isAttacking)
                {
                    _isAttacking = true;
                    _attack.Acttack(enemyClose.gameObject);
                }

                IdleState();
            }
            else
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
            _isStartWaitForDefineEnegy = true;
        }
        else
        {
            _navMeshObstacle.enabled = false;
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

    protected virtual Transform IsTarget(Collider[] list)
    {
        foreach(Collider element in list)
        {
            Transform eleTranf = element.transform;
            if (eleTranf == target)
            {
                return eleTranf;
            }
        }
        return null;
    }
   

    public override void DirectToTarget()
    {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    protected void IdleState()
    {
        if (_isMoving)
        {
            _isMoving = false;
            _animatorCharacter.Idle();
        }
    }

    protected void RunState()
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
    
    protected IEnumerator EnableChase()
    {
        _isStartEnabelChase = true;
        _isStartDisEnabelChase = false;
        _navMeshObstacle.enabled = false;
        yield return new WaitForSeconds(.07f);
        _navMeshAgent.enabled = true;
        _navMeshAgent.isStopped = false;
        _navMeshAgent.SetDestination(transform.position);
    }

    protected IEnumerator DisEnableChase()
    {
        _isStartDisEnabelChase = true;
        _isStartEnabelChase = false;
        _navMeshAgent.enabled = false;
        yield return new WaitForSeconds(.07f);
        _navMeshObstacle.enabled = true;
    }
}

