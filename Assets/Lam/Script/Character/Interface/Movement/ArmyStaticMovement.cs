using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ArmyAnimator))]
public abstract class ArmyStaticMovement : ArmyMovement, IArmyMovement, ICharacterMovement
{
    [SerializeField] protected Transform previousTarget = null;
    [SerializeField] protected bool _isAttacking = true;
    protected virtual void Update()
    {
        DirectToTarget();
        DefineEnemy();
        if (target != null)
        {
            if (target != previousTarget)
            {
                if (_isAttacking)
                {
                    _attack.StopActtack();  
                    _isAttacking = false;
                }
                previousTarget = target;
            } else 
            {
                if (!_isAttacking)
                {
                    // Debug.Log("here");
                    _attack.Acttack(target.gameObject);
                    _isAttacking = true;
                }
            }
        } else
        {

            if (_isAttacking)
            {
                _isAttacking = false;
                _animatorCharacter.Idle();
                _attack.StopActtack();
            }
        }
            
    }

    private IEnumerator WaitAndDefineEnemy()
    {
        yield return new WaitForSeconds(2f); 
        isHadEnemy = false;
    }

    public override void DirectToTarget()
    {
        if (target !=null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}