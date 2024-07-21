using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidentWork : AWork
{
    protected override IEnumerator DoManufacture(Transform nature)
    { 
        NatureHealth health = nature.GetComponent<NatureHealth>();
        float time = health.TakeDamage(_damage);
        _animatorWorker.Work(true);
        yield return new WaitForSeconds(time);
        
        ResidentMovement movement = GetComponent<ResidentMovement>();
        movement.DoneTarget(nature);
        _animatorWorker.Work(false);
        isManufactureProcess =null;
    }
}
