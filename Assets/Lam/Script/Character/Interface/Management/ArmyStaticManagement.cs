using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class ArmyStaticManagement : ArmyManagement
{
    protected Transform previousTarget = null;
    private bool isAttacking;
    protected override void Awake() 
    {
        base.Awake();
        _animator = GetComponent<ArmyStaticAnimator>();
        _movement = GetComponent<ArmyStaticMovement>();
    }

    private void Update() 
    {
        // if (_movement.isHadEnemy && _movement.target)
        // {
        //     _movement.DirectToTarget();
        //     if (_movement.target == null)
        //     {
        //         _animator.Idle();
        //         _movement.isHadEnemy = false;
        //     } else
        //     {
        //         if (_movement.target != previousTarget)
        //         {
        //             previousTarget = _movement.target;
        //             isAttacking = true;
        //         }
        //     }
        // } else 
        // {
        //     _movement.DefineEnemy();
        //     if (!isAttacking)
        //     {
        //         if (isAttackProcess != null)
        //         {
        //             StopCoroutine(isAttackProcess);
        //             _animator.Attack(false);
        //             _animator.Idle();
        //         }
        //         isAttacking = false;
        //     }
        // }
    }
}