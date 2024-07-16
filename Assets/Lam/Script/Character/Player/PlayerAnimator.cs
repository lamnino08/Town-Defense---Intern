using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : ArmyDynamicAnimator
{
    [SerializeField] private float _acceletion;  
    [SerializeField] private float _deceletion;

    public override void Walk()
    {
        if (_velocity > _walkVelociy)
        {
            _velocity -= _deceletion * Time.deltaTime;
        }
        else
            if (_velocity < _walkVelociy)
            {
                _velocity += _acceletion * Time.deltaTime;
            }
        _animator.SetFloat(_velocityHash, _velocity);
    }

    public override void Run()
    {
        // if (_velocity < _walkVelociy)
        // {
        //     _velocity += (_acceletion + 0.3f) * Time.deltaTime;
        // } else
        // {
        //     if (_velocity < _runVelociy)
        //     {
        //         _velocity += _acceletion * Time.deltaTime;
        //     }
        // }
        _velocity = 5;
        _animator.SetFloat(_velocityHash, _velocity);
    }

    public override void Idle()
    {

        // if (_velocity > 0)
        // {
        //     _velocity -= (_deceletion);
        // }  
        // if (_velocity < 0)
        // {
            _velocity = 0;
        // }
        _animator.SetFloat(_velocityHash, _velocity);
    }

}
