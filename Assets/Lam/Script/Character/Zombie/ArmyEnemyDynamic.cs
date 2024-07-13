using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class ArmyEnemyDynamic : ArmyDynamicMovement 
{
    protected override void Start()
    {
        base.Start();
        target = FindFirstObjectByType<Penhouse>().transform;
    }

    public override void ChaseTarget()
    {
        if (target != null)
        {
            DefineEnemy();
            // NavMeshPath path = new NavMeshPath();
            // if (!NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path))
            // {
            //     Vector3 direction = (target.position - transform.position).normalized;
            //     Ray ray = new Ray(transform.position, direction);
            //     float distance = Vector3.Distance(transform.position, target.position);
            //     RaycastHit hit1;
            //     if (Physics.Raycast(ray, out hit1, distance, LayerMask.GetMask("Wall")))
            //     {
            //         Debug.Log("Found wall object: " + hit1.collider.gameObject.name);
            //         target = hit1.collider.transform;
            //     }
            // } 

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
                    Debug.Log(enemyClose.gameObject.name);
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
                    // NavMeshPath path = new NavMeshPath();
                    // if (NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path))
                    // {                   
                    //     if (path != null)
                    //     {
                    //         Debug.Log("here");
                    //     } else
                    //     {
                    //     }
                    // }

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

    protected override Transform IsTarget(Collider[] list)
    {
        foreach(Collider element in list)
        {
            Transform eleTranf = element.transform;
            if (eleTranf == target)
            {
                return eleTranf;
            }
        }

        foreach(Collider element in list)
        {
            if (element.CompareTag("Gate"))
            {
                return element.transform;
            }
        }
        return null;
    }
}

