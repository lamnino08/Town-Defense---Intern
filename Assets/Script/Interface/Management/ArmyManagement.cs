using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ArmyManagement : MonoBehaviour
{
    protected ArmyAnimator _animator;
    protected ArmyMovement _movement;
    protected ArmyHealth _health;
    protected Attack _attack;

    protected virtual void Awake()
    {
        _health = GetComponent<ArmyHealth>();
        _attack = GetComponent<Attack>();
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }
}
