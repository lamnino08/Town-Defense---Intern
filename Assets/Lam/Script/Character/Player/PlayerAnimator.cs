using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : ArmyDynamicAnimator, IWorkerAnimator
{
    protected int _isWorkHash;

    protected override void Start() 
    {
        base.Start();
        _isWorkHash = Animator.StringToHash("isWork");
    }

    public void Work()
    {
        _animator.SetTrigger(_isWorkHash);
    }
}
