using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSwarmMovement : ArmyBirdDynamicMovement 
{
    protected override void Start()
    {
        base.Start();
        target = FindFirstObjectByType<Penhouse>().transform;
    }
    public override void DirectToTarget()
    {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
