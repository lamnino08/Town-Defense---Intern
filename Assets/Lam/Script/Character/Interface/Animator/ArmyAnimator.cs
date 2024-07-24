
using UnityEngine;

public abstract class ArmyAnimator : MonoBehaviour, ICharacterAnimator, IArmyAnimator
{
    protected Animator _animator;
    protected int _isDeadHash;
    protected int _isAttackHash;

    protected virtual void Start() 
    {
        _animator = GetComponentInChildren<Animator>();
        _isAttackHash = Animator.StringToHash("isAttack");
        _isDeadHash = Animator.StringToHash("isDead");
    }

    public abstract void Idle();

    public void Attack()
    {
        if (_animator)
        {
            _animator.SetTrigger(_isAttackHash);
        }
    }
    
    public void Dead()
    {
        _animator.SetTrigger(_isDeadHash);
    }
}
