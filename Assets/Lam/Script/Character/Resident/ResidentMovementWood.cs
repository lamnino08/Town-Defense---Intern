using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ResidentMovementWood : ResidentMovement
{
    

    protected override void CheckDetectTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f, _natureMask);

                foreach (var collider in hitColliders)
                {
                    if (collider.transform == _natureTarget && collider.transform.CompareTag("tree"))
                    {
                        _navMeshAgent.isStopped = true;
                        _animator.Idle();
                        _Work.Manufacture(_natureTarget);
                        _previousNatureTarget = _natureTarget;

                        if (listTarget.Count ==0)
                        {
                            DefineNatureArea();
                        }
                    }
                }
    }

    protected override void DefineNatureArea()
    {
        Collider[] additionalTargets = Physics.OverlapSphere(_natureTarget.position, 10f, _natureMask);
            
        foreach (var additionalCollider in additionalTargets)
        {
            if (!listTarget.Contains(additionalCollider.transform) && additionalCollider.CompareTag("tree"))
            {
                listTarget.Add(additionalCollider.transform);
            }
        }

        listTarget.Sort((a, b) => 
        Vector3.Distance(transform.position, a.position).CompareTo(
        Vector3.Distance(transform.position, b.position)));
    }
}