using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class ArmyEnemyDynamic : ArmyDynamicMovement 
{
    protected override void Start()
    {
        base.Start();
        target = FindFirstObjectByType<Penhouse>().transform;
    }
}
