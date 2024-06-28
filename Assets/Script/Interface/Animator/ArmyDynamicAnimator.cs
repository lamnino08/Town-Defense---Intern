using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmyDynamicAnimator : ArmyAnimator, ICharacterAnimator, IDynamicAnimator
{
    [SerializeField] protected float _walkVelociy;
    [SerializeField] protected float _runVelociy;
    [SerializeField] protected float _velocity = 0;
    protected int _velocityHash;
    protected override void Start() 
    {
        base.Start();
        _velocityHash = Animator.StringToHash("Velocity");
    }

    public virtual void Walk()
    {
        _velocity = _walkVelociy;
        _animator.SetFloat(_velocityHash, _velocity);
    }

    public virtual void Run()
    {
        _velocity = _runVelociy;
        _animator.SetFloat(_velocityHash, _velocity);
    }

    public override void Idle()
    {
        _velocity = 0;
        _animator.SetFloat(_velocityHash, _velocity);
    }
}
