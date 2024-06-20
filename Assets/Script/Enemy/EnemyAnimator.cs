using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : IAnimator
{
     [SerializeField] private Animator animator;

    [SerializeField] private float _walkVelociy;
    [SerializeField] private float _runVelociy;
    [SerializeField] private float _velocity = 0;
    [SerializeField] private float _acceletion;  
    [SerializeField] private float _deceletion;
    private int velocityHash;

    private void Start() 
    {
        velocityHash = Animator.StringToHash("Velocity");
    }

    public void Walk()
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
        animator.SetFloat(velocityHash, _velocity);
    }

    public void Run()
    {
        if (_velocity < _walkVelociy)
        {
            _velocity += (_acceletion + 0.3f) * Time.deltaTime;
        } else
        {
            if (_velocity < _runVelociy)
            {
                _velocity += _acceletion * Time.deltaTime;
            }
        }
        animator.SetFloat(velocityHash, _velocity);
    }

    public void Idle()
    {
        if (_velocity > 0)
        {
            _velocity -= (_deceletion);
        }  
        if (_velocity < 0)
        {
            _velocity = 0;
        }
        animator.SetFloat(velocityHash, _velocity);
    }
}
