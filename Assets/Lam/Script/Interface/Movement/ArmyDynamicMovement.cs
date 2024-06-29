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
    [SerializeField] protected float speedMove;
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
        _navMeshAgent.speed = speedMove;
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

            Collider[] colliders = Physics.OverlapSphere(transform.position, _distanceStoppingToStop, layerMaskOfEnemy);
            if (colliders.Length != 0)
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
                    _attack.Acttack(target.gameObject);
                }

                IdleState();
            }
            else
            {
                if (_navMeshAgent.enabled)
                {
                    _navMeshAgent.isStopped = false;
                    _navMeshAgent.SetDestination(target.position);
                    // Debug.Log(this.gameObject.name+"    "+target.gameObject.name);

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
    
    protected IEnumerator EnableChase()
    {
        _isStartEnabelChase = true;
        _isStartDisEnabelChase = false;
        _navMeshObstacle.enabled = false;
        yield return new WaitForSeconds(1f);
        _navMeshAgent.enabled = true;
        _navMeshAgent.isStopped = false;
    }

    protected IEnumerator DisEnableChase()
    {
        _isStartDisEnabelChase = true;
        _isStartEnabelChase = false;
        _navMeshAgent.enabled = false;
        yield return new WaitForSeconds(0.3f);
        _navMeshObstacle.enabled = true;
    }
}

