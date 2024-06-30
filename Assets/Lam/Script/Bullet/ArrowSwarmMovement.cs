using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSwarmMovement : AArrowMovement
{
    protected override void ArrowFlyCurve()
    {
        Vector3 direction = (endPos - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, endPos) < 0.1f)
        {
            Destroy(gameObject);
        }

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion adjustedRotation = targetRotation * Quaternion.Euler(0, 90, 0); 
            transform.rotation = adjustedRotation;
        }
    }
}
