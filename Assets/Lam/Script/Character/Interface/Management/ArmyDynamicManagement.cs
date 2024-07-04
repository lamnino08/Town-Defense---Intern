using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class ArmyDynamicManagement : ArmyManagement
{
    // private bool isAttacking = false;
    private ArmyDynamicMovement _moveDynamic;
    protected override void Awake() 
    {
        base.Awake();
        _animator = GetComponent<ArmyDynamicAnimator>();
        _movement = GetComponent<ArmyDynamicMovement>();
        _moveDynamic = GetComponent<ArmyDynamicMovement>();
    }

    private void Update() 
    {
        // _moveDynamic.ChaseTarget();
    }
}