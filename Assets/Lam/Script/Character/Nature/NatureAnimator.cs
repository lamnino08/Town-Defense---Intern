
using UnityEngine;

public class NatureAnimator : MonoBehaviour, ICharacterAnimator
{
    protected Animator _animator;
    protected int _isDeadHash;
    protected virtual void Start() 
    {
        _animator = GetComponentInChildren<Animator>();
        _isDeadHash = Animator.StringToHash("isDead");
    }

    public void Dead()
    {
        _animator.SetTrigger(_isDeadHash);
    }

    public void Idle()
    {
        
    }
}
