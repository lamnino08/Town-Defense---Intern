using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierTownArcherMovement : ArmyStaticMovement
{
    public override void DefineEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, rangeFindEnemy, layerMaskOfEnemy);
        if (colliders.Length == 0)
        {
            return;
        }

        Collider highestCollider = null;
        float highestYPosition = float.MinValue;

        foreach (Collider collider in colliders)
        {
            if (collider.transform.position.y > highestYPosition)
            {
                highestYPosition = collider.transform.position.y;
                highestCollider = collider;
            }
        }

        if (highestCollider != null)
        {
            target = highestCollider.transform;
        }

        // if (highestCollider != null)
        // {
        //     List<Collider> highestColliders = new List<Collider>();

        //     foreach (Collider collider in colliders)
        //     {
        //         if (collider.transform.position.y == highestYPosition)
        //         {
        //             highestColliders.Add(collider);
        //         }
        //     }

        //     Collider closestCollider = null;
        //     float closestDistance = float.MaxValue;

        //     foreach (Collider collider in highestColliders)
        //     {
        //         float distance = Vector3.Distance(transform.position, collider.transform.position);
        //         if (distance < closestDistance)
        //         {
        //             closestDistance = distance;
        //             closestCollider = collider;
        //         }
        //     }

        //     if (closestCollider != null)
        //     {
        //         target = closestCollider.transform;
        //     }
        // }
    }

}