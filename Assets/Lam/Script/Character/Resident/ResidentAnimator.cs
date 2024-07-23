using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidentAnimator : MonoBehaviour, IDynamicAnimator, IWorkerAnimator, ICharacterAnimator
{
    private int _isWorkHash;
    private int _VelocityHash;
    private int _isDeadkHash;
    private Animator _animator;
    

    private void Start() 
    {
        _isWorkHash = Animator.StringToHash("isWork");
        _VelocityHash = Animator.StringToHash("Velocity");
        _isDeadkHash = Animator.StringToHash("isDead");
        _animator = GetComponentInChildren<Animator>();
    }
    public void Work()
    {
        _animator.SetTrigger(_isWorkHash);
    }

    public void Walk()
    {
        _animator.SetFloat(_VelocityHash, 1);
    }

    public void Run()
    {
        _animator.SetFloat(_VelocityHash, 5);
    }

    public void Idle()
    {
        _animator.SetFloat(_VelocityHash, 0);
    }

    public void Dead()
    {
        _animator.SetTrigger(_isDeadkHash);
    }
}
