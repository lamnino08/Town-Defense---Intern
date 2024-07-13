using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// [RequireComponent( typeof(NavMeshAgent), typeof(NavMeshObstacle))]
public abstract class ArmyBirdDynamicMovement : ArmyMovement, IArmyMovement, ICharacterMovement, ICharacterDynamicMovement
{
    protected Transform _previouseTarget;
    [SerializeField] protected float _distanceStoppingToStop;
    [SerializeField] protected float speedMove;
    protected bool _isMoving = false;
    protected bool _isAttacking = false;
    protected IDynamicAnimator _animatorDynamic;
    protected override void Start()
    {
        base.Start();
        _animatorDynamic = GetComponentInChildren<ArmyDynamicAnimator>();
    }
    protected void Update()
    {
            ChaseTarget();
    }

    public void ChaseTarget()
    {
        DefineEnemy();

        if (target != null)
        {
            DirectToTarget();
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

            Vector2 birdPosition2D = new Vector2(transform.position.x, transform.position.z);
            Vector2 targetPosition2D = new Vector2(target.position.x, target.position.z);

            float distanceToTarget = Vector2.Distance(birdPosition2D, targetPosition2D);
            if (distanceToTarget < _distanceStoppingToStop)
            {
                
                if (!_isAttacking)
                {
                    _isAttacking = true;
                    _attack.Acttack(target.gameObject);
                }

                IdleState();
            }
            else
            {

                Vector3 direction = (target.position - transform.position).normalized;
                Vector3 targetMove = transform.position + direction * speedMove * Time.deltaTime;
                transform.position = new UnityEngine.Vector3(targetMove.x, 5, targetMove.z);

                if (_isAttacking)
                {
                    _isAttacking = false;
                    _attack.StopActtack();
                } 

                RunState();
            }
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

        }
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
}

